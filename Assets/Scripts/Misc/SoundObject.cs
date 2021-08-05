using UnityEngine;

namespace PuzzledTiles.Misc
{
	/// <summary>
	/// A scriptable object to contain references to all the sound effects in the game.
	/// </summary>
	[CreateAssetMenu(fileName = "Sound", menuName = "Puzzle/Sound")]
	public class SoundObject : ScriptableObject
	{
		public AudioClip start;
		public AudioClip piece;
		public AudioClip gameOver;
		public AudioClip squaresRemoved;
	}
}