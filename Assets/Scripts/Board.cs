using PuzzledTiles.Misc;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzledTiles
{
	/// <summary>
	/// This class represents the game board and contains the main logic of the game.
	/// </summary>
	public class Board : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("Linker scriptable object.")]
		private LinkerObject linker;
		[SerializeField]
		[Tooltip("Score scriptable object.")]
		private ScoreObject score;
		[SerializeField]
		[Tooltip("Sound scriptable object.")]
		private SoundPlayer sound;

		/// <summary>
		/// Number of slots per row in the grid.
		/// </summary>
		public int width;
		/// <summary>
		/// Number of slots per column in the grid.
		/// </summary>
		public int height;

		/// <summary>
		/// A list of all the slots in the board.
		/// </summary>
		private List<Slot> grid;
		/// <summary>
		/// A list of the slots organized by rows.
		/// </summary>
		private List<List<Slot>> rows;
		/// <summary>
		/// A list of the slots organized by columns.
		/// </summary>
		private List<List<Slot>> columns;
		/// <summary>
		/// A list of the pieces the player can move at the moment.
		/// </summary>
		private List<Piece> activePieces;

		public List<Piece> ActivePieces => activePieces;

		private void Awake()
		{
			linker.board = this;

			sound.PlayStart();

			//Check if the board dimensions are correct

			Slot[] slots = GetComponentsInChildren<Slot>();

			if (width * height != slots.Length)
			{
				Debug.LogError("Board mismatch: (width x height) not equal to number of slots.");
				return;
			}

			//Populate lists of slots

			grid = new List<Slot>(slots);

			grid.Sort(new SlotComparerRows());
			rows = new List<List<Slot>>();
			for (int i = 0; i < height; i++) rows.Add(grid.GetRange(i * width, width));

			grid.Sort(new SlotComparerColumns());
			columns = new List<List<Slot>>();
			for (int j = 0; j < width; j++) columns.Add(grid.GetRange(j * height, height));

			activePieces = new List<Piece>(3);
		}

		private void Start()
		{
			//Initialize score and high score
			score.Init();
		}

		/// <summary>
		/// Check if the active pieces are placeable one by one.
		/// If none of them is, the game ends.
		/// </summary>
		public void CheckActivePieces()
		{
			int placeable = 0;

			//Check if the active pieces are placeable one by one.
			foreach (Piece piece in activePieces)
			{
				if (CheckFakePiece(piece.Fake))
				{
					piece.SetPlaceable(true);
					placeable++;
				}
				else
				{
					piece.SetPlaceable(false);
				}
			}

			//If none of them is, the game ends.
			if (placeable == 0)
			{
				linker.GameOver();
				sound.PlayGameOver();
				score.SaveHighScore();
			}
		}

		/// <summary>
		/// Use a fake piece to check if its associated piece is placeable.
		/// </summary>
		/// <param name="fake"></param>
		/// <returns></returns>
		private bool CheckFakePiece(FakePiece fake)
		{
			foreach (Slot slot in grid)
			{
				if (slot.HasSquare()) continue;

				//Move the fake piece's position to a slot position
				fake.Position = slot.transform.position;

				bool validSlot = true;

				//Check if all fake squares are on empty slots
				foreach(Vector2 squareLocalPos in fake.FakeSquares)
				{
					//Find the closest slot
					Slot closeSlot = grid.Find(s =>
					{
						Vector2 slotPos = s.transform.position;
						return Vector2.SqrMagnitude(slotPos - (fake.Position + squareLocalPos)) < 0.3f;
					});

					//If the slot is not valid, move to the next empty slot
					if (closeSlot == null || closeSlot.HasSquare())
					{
						validSlot = false;
						break;
					}
				}

				//If the slot is valid, the piece is placeable
				if (validSlot) return true;
			}


			return false;
		}

		/// <summary>
		/// Check if all the squares of a piece are on an empty slot
		/// when the player releases the piece on the board.
		/// </summary>
		/// <param name="squares">List of squares.</param>
		/// <param name="assigned">Assigned slots for each square.</param>
		/// <returns></returns>
		public bool CheckSquares(List<Square> squares, out Dictionary<Square, Slot> assigned)
		{
			assigned = new Dictionary<Square, Slot>();

			foreach (Slot slot in grid)
			{
				//If a slot is empty, check if some square is close to it

				if (slot.HasSquare()) continue;
				Vector2 slotPos = slot.transform.position;

				foreach (Square square in squares)
				{
					//Avoid checking squares that have been already assigned to another slot
					if (assigned.ContainsKey(square)) continue;

					//Check if the slot is close enough to be assigned
					Vector2 squarePos = square.transform.position;
					if (Vector2.SqrMagnitude(slotPos - squarePos) < 0.3f)
						assigned.Add(square, slot);
				}
			}

			//If the number of assigned squares is the same as the number of squares, return true
			return squares.Count == assigned.Count;
		}

		/// <summary>
		/// Use a list of assigned squares and slots to place the squares on the slots.
		/// </summary>
		/// <param name="assigned"></param>
		public void PlaceSquares(Dictionary<Square, Slot> assigned)
		{
			sound.PlayPiece();

			foreach (Square square in assigned.Keys)
				square.Place(assigned[square]);
		}

		/// <summary>
		/// Remove all the squares that are marked to be removed.
		/// </summary>
		/// <param name="slots"></param>
		private void RemoveMarkedSquares(List<Slot> slots)
		{
			slots.ForEach(slot => slot.RemoveSquareIfMarked());
		}

		/// <summary>
		/// Check a list of slots and return true if all of them contain a square.
		/// </summary>
		/// <param name="lines">A list of slots (e.g. a row or a column).</param>
		/// <returns></returns>
		private bool CheckLines(List<List<Slot>> lines)
		{
			bool removeSomething = false;

			lines.ForEach(line =>
			{
				if (line.TrueForAll(slot => slot.HasSquare()))
				{
					removeSomething = true;
					line.ForEach(slot => slot.MarkToRemove());
				}
			});

			return removeSomething;
		}

		/// <summary>
		/// Check all the rows and columns and remove the complete ones.
		/// </summary>
		public void RemoveCompleteLines()
		{
			if(CheckLines(rows) | CheckLines(columns))
				sound.PlaySquaresRemoved();
			RemoveMarkedSquares(grid);
		}
	}
}