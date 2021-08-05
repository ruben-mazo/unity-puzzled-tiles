using UnityEngine;
using UnityEngine.UI;

namespace PuzzledTiles.Menus
{
	/// <summary>
	/// Options menu behaviour.
	/// </summary>
	public class OptionsMenu : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("The volume slider of the sound effects in the game.")]
		private Slider volumeSlider;

		private void Awake()
		{
			//Hide on awake
			gameObject.SetActive(false);

			//Get volume from player prefs
			if (!PlayerPrefs.HasKey("volume"))
				PlayerPrefs.SetFloat("volume", 1);
			volumeSlider.value = PlayerPrefs.GetFloat("volume");
		}

		/// <summary>
		/// Set volume on player prefs.
		/// </summary>
		public void SetVolume()
		{
			PlayerPrefs.SetFloat("volume", volumeSlider.value);
		}

		/// <summary>
		/// Save player prefs.
		/// </summary>
		public void SavePrefs()
		{
			PlayerPrefs.Save();
		}
	}
}