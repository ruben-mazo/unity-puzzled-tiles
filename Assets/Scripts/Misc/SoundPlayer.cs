using UnityEngine;

namespace PuzzledTiles.Misc
{
	/// <summary>
	/// The source of the sound effects.
	/// </summary>
	public class SoundPlayer : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("Sound scriptable object.")]
		private SoundObject sound;

		private float volume;

		private void Awake()
		{
			//Get volume from player prefs
			if (!PlayerPrefs.HasKey("volume"))
				PlayerPrefs.SetFloat("volume", 1);
			volume = PlayerPrefs.GetFloat("volume");
		}

		/// <summary>
		/// Play the "start game" sound effect.
		/// </summary>
		public void PlayStart()
		{
			AudioSource.PlayClipAtPoint(sound.start, transform.position, volume);
		}

		/// <summary>
		/// Play the "piece placed" sound effect.
		/// </summary>
		public void PlayPiece()
		{
			AudioSource.PlayClipAtPoint(sound.piece, transform.position, volume);
		}

		/// <summary>
		/// Play the "game over" sound effect.
		/// </summary>
		public void PlayGameOver()
		{
			AudioSource.PlayClipAtPoint(sound.gameOver, transform.position, volume);
		}

		/// <summary>
		/// Play the "squares removed" sound effect.
		/// </summary>
		public void PlaySquaresRemoved()
		{
			AudioSource.PlayClipAtPoint(sound.squaresRemoved, transform.position, volume);
		}
	}
}