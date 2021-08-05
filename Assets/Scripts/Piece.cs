using System.Collections.Generic;
using UnityEngine;

namespace PuzzledTiles
{
	/// <summary>
	/// This class represents a piece, composed by a bunch of squares
	/// and placeable by the player on the board.
	/// </summary>
	public class Piece : MonoBehaviour
	{
		private const float SCALE_QUEUE = 0.4f;
		private const float SCALE_SPAWN = 0.7f;

		[SerializeField]
		[Tooltip("Linker scriptable object.")]
		private LinkerObject linker;

		/// <summary>
		/// The piece collider, usually a box collider 2d.
		/// </summary>
		private Collider2D coll;

		/// <summary>
		/// A reference of the game board.
		/// </summary>
		private Board board;

		/// <summary>
		/// The spawn this piece is on.
		/// </summary>
		private Spawn spawn;

		/// <summary>
		/// True when the player is moving the piece.
		/// </summary>
		private bool moving;

		/// <summary>
		/// The speed of the piece when the player is not moving it.
		/// </summary>
		private float speed;

		/// <summary>
		/// A helper vector to keep the piece's position relative to the
		/// cursor position when dragging the piece.
		/// </summary>
		private Vector2 offset;

		/// <summary>
		/// A list of the squares in the piece.
		/// </summary>
		private List<Square> squares;

		/// <summary>
		/// A fake piece similar to this, used to check if the piece is placeable
		/// without moving it.
		/// </summary>
		public FakePiece Fake { get; private set; }

		private void Awake()
		{
			coll = GetComponent<Collider2D>();
			moving = false;
			speed = 50;
			offset = Vector2.zero;

			//Randomize the piece and squares rotation to make the piece more interesting and unique
			squares = new List<Square>(GetComponentsInChildren<Square>());
			transform.Rotate(Vector3.forward, Random.Range(0, 4) * 90f);
			squares.ForEach(square => square.transform.Rotate(Vector3.forward, Random.Range(0, 4) * 90f));

			//Create the fake piece BEFORE scaling it to the queue size
			CreateFakePiece();
			transform.localScale = new Vector3(SCALE_QUEUE, SCALE_QUEUE, 1);
		}

		private void Start()
		{
			board = linker.board;

			//Randomize the piece color
			Color color = linker.GetRandomColor();
			squares.ForEach(square => square.SetColor(color));
		}

		private void Update()
		{
			if (moving) //If the player is moving the piece...
			{
				//...maintain the piece under the cursor
				Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				transform.position = new Vector2(mousePosition.x, mousePosition.y) + offset;
			}
			else //Else...
			{
				//...move the piece to it's parent position (a queue position or a spawn position)
				transform.localPosition = Vector2.MoveTowards(transform.localPosition, Vector2.zero, speed * Time.deltaTime);
			}
		}

		/// <summary>
		/// Create a fake piece similar to this piece.
		/// </summary>
		private void CreateFakePiece()
		{
			Vector2 position = squares[0].transform.position;
			List<Vector2> fakeSquares = new List<Vector2>();
			squares.ForEach(square => fakeSquares.Add(square.transform.position - squares[0].transform.position));
			Fake = new FakePiece(position, fakeSquares);
		}

		/// <summary>
		/// When in a spawn, change the piece state to "placeable"
		/// (draggable by the player and fully visible)
		/// or "not placeable" (not draggable and partially faded).
		/// </summary>
		/// <param name="placeable"></param>
		public void SetPlaceable(bool placeable)
		{
			if (placeable)
			{
				coll.enabled = true;
				spawn.SetFilter(false);
			}
			else
			{
				coll.enabled = false;
				spawn.SetFilter(true);
			}
		}

		/// <summary>
		/// Move the piece from the queue to a specific spawn point.
		/// </summary>
		public void MoveToSpawn(Spawn spawn)
		{
			this.spawn = spawn;
			transform.SetParent(spawn.transform);
			coll.enabled = true;
			transform.localScale = new Vector3(SCALE_SPAWN, SCALE_SPAWN, 1);
			board.ActivePieces.Add(this);
		}

		public void OnMouseDown()
		{
			//Start moving the piece
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			float offset_x = transform.position.x - mousePosition.x;
			float offset_y = transform.position.y - mousePosition.y;
			offset = new Vector2(offset_x, offset_y);
			moving = true;
			transform.localScale = Vector3.one;
			squares.ForEach(square => square.SetSortingOrder(200));
		}

		public void OnMouseUp()
		{
			//Stop moving the piece
			moving = false;
			offset = Vector2.zero;
			squares.ForEach(square => square.SetSortingOrder(100));

			//Check if the piece is in a good place

			if (board.CheckSquares(squares, out Dictionary<Square, Slot> assigned))
			{
				//Place the piece's squares, destroy the piece, remove complete lines in the
				//board, get a new piece from the queue and check if the pieces in the spawns
				//are placeable.
				board.ActivePieces.Remove(this);
				board.PlaceSquares(assigned);
				Destroy(gameObject);
				board.RemoveCompleteLines();
				spawn.NextPiece();
				board.CheckActivePieces();
			}
			else
			{
				//The piece comes back to its spawn
				transform.localScale = new Vector3(SCALE_SPAWN, SCALE_SPAWN, 1);
			}
		}
	}
}