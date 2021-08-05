using UnityEngine;

namespace PuzzledTiles.Misc
{
	/// <summary>
	/// Game Over panel behaviour
	/// </summary>
	public class GameOver : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("Linker scriptable object.")]
		private LinkerObject linker;

		private void Awake()
		{
			//Hide on awake
			SetVisible(false);

			linker.gameOver = this;
		}

		/// <summary>
		/// Set visibility of the panel.
		/// </summary>
		/// <param name="visible"></param>
		public void SetVisible(bool visible)
		{
			gameObject.SetActive(visible);
		}
	}
}