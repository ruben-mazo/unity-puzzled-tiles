using UnityEngine;

namespace PuzzledTiles.Menus
{
	/// <summary>
	/// A database that contains the names of the scenes in the project.
	/// </summary>
	[CreateAssetMenu(fileName = "Scene Names", menuName = "Scene Management/Scene Names")]
	public class SceneNames : ScriptableObject
	{
		public string startMenu = "Start Menu";
		public string game = "Game";
	}
}