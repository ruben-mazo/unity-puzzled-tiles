namespace PuzzledTiles.Misc
{
	/// <summary>
	/// An interface for the sound listeners to implement.
	/// </summary>
	public interface IScoreListener
	{
		public void OnScoreUpdated();
		public void OnHighScoreUpdated();
	}
}