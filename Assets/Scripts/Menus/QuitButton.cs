using UnityEngine;
using UnityEngine.SceneManagement;

namespace PuzzledTiles.Menus
{
	/// <summary>
	/// Quit button behaviour.
	/// </summary>
	public class QuitButton : MonoBehaviour
	{
		[SerializeField]
		private SceneNames sceneNames;

		/// <summary>
		/// Return to the start menu.
		/// </summary>
		public void Quit()
		{
			SceneManager.LoadScene(sceneNames.startMenu);
		}
	}
}