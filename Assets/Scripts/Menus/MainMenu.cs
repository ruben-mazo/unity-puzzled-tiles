using UnityEngine;
using UnityEngine.SceneManagement;

namespace PuzzledTiles.Menus
{
	/// <summary>
	/// Main menu behaviour.
	/// </summary>
	public class MainMenu : MonoBehaviour
	{
		[SerializeField]
		private SceneNames sceneNames;

		/// <summary>
		/// Start a new game.
		/// </summary>
		public void Play()
		{
			SceneManager.LoadScene(sceneNames.game);
		}

		/// <summary>
		/// Close the game.
		/// </summary>
		public static void Quit()
		{
			Application.Quit();
		}
	}
}