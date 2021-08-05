using UnityEngine;

namespace PuzzledTiles
{
	/// <summary>
	/// This class represents a spawn point where the placeable pieces will go from the queue.
	/// The player can drag them to the board only from this point.
	/// </summary>
	public class Spawn : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("Linker scriptable object.")]
		private LinkerObject linker;

		[SerializeField]
		[Tooltip("A filter to fade the pieces when they're not placeable.")]
		private SpriteRenderer filter;

		private void Awake()
		{
			//All pieces are placeable at the beginning
			SetFilter(false);
		}

		private void Start()
		{
			//Get first piece from the queue
			NextPiece();
		}

		/// <summary>
		/// Get a piece from the queue and move it to the spawn position.
		/// </summary>
		public void NextPiece()
		{
			Piece piece = linker.GetQueuedPiece().GetComponent<Piece>();
			piece.MoveToSpawn(this);
			linker.NewQueuedPiece();
		}

		/// <summary>
		/// Set the filter's visibility.
		/// </summary>
		public void SetFilter(bool enabled)
		{
			filter.enabled = enabled;
		}
	}
}