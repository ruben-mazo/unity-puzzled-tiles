using System.Collections.Generic;
using UnityEngine;

namespace PuzzledTiles
{
	/// <summary>
	/// This class represents the spawn queue that provides new pieces for the spawn points.
	/// This is used to show the player what pieces will come next.
	/// </summary>
	public class SpawnQueue : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("Linker scriptable object.")]
		private LinkerObject linker;

		[SerializeField]
		[Tooltip("Positions of the pieces in the queue.")]
		private List<Transform> queuePositions = new List<Transform>();

		private List<GameObject> queue;

		private void Awake()
		{
			linker.queue = this;

			//Fill the queue with new pieces.
			queue = new List<GameObject>();
			for (int i = 0; i < queuePositions.Count; i++) InstantiateNewPiece();
		}

		/// <summary>
		/// Create a new random piece and place it at the end of the queue.
		/// </summary>
		public void InstantiateNewPiece()
		{
			GameObject piece = Instantiate(linker.GetRandomPiece(), queuePositions[queue.Count]);
			piece.transform.localPosition = Vector2.right * 5;
			queue.Add(piece);
		}

		/// <summary>
		/// Return the first piece in the queue.
		/// </summary>
		public GameObject GetNextPiece()
		{
			for (int i = 1; i < queue.Count; i++)
				queue[i].transform.SetParent(queuePositions[i - 1]);

			GameObject nextPiece = queue[0];
			queue.RemoveAt(0);
			return nextPiece;
		}
	}
}