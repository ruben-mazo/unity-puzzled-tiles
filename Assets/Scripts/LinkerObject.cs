using PuzzledTiles.Misc;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzledTiles
{
	/// <summary>
	/// A scriptable object use as a linker between unrelated scene objects.
	/// It contains methods to communicate objects and lists of prefabs, colors or
	/// sprites to get random elements for each piece and square.
	/// </summary>
	[CreateAssetMenu(fileName = "Linker", menuName = "Puzzle/Linker")]
	public class LinkerObject : ScriptableObject
	{
		[HideInInspector]
		public Board board;

		[HideInInspector]
		public SpawnQueue queue;

		[HideInInspector]
		public GameOver gameOver;

		[SerializeField]
		private List<GameObject> piecePrefabs = new List<GameObject>();

		[SerializeField]
		private List<Color> colors = new List<Color>();

		[SerializeField]
		private List<Sprite> squareSprites = new List<Sprite>();

		/// <summary>
		/// Get a random piece from the list.
		/// </summary>
		/// <returns></returns>
		public GameObject GetRandomPiece()
		{
			int index = Random.Range(0, piecePrefabs.Count);
			return piecePrefabs[index];
		}

		/// <summary>
		/// Get a random color from the list.
		/// </summary>
		/// <returns></returns>
		public Color GetRandomColor()
		{
			int index = Random.Range(0, colors.Count);
			return colors[index];
		}

		/// <summary>
		/// Get a random square sprite from the list.
		/// </summary>
		/// <returns></returns>
		public Sprite GetRandomSquare()
		{
			int index = Random.Range(0, squareSprites.Count);
			return squareSprites[index];
		}

		/// <summary>
		/// Create a new piece at the end of the queue.
		/// </summary>
		public void NewQueuedPiece()
		{
			queue.InstantiateNewPiece();
		}

		/// <summary>
		/// Get the first piece in the queue.
		/// </summary>
		/// <returns></returns>
		public GameObject GetQueuedPiece()
		{
			return queue.GetNextPiece();
		}

		/// <summary>
		/// Show the "Game Over" panel.
		/// </summary>
		public void GameOver()
		{
			gameOver.SetVisible(true);
		}
	}
}