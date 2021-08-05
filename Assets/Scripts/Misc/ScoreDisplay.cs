using UnityEngine;
using UnityEngine.UI;

namespace PuzzledTiles.Misc
{
	/// <summary>
	/// Score displayer behaviour.
	/// </summary>
	public class ScoreDisplay : MonoBehaviour, IScoreListener
	{
		[SerializeField]
		[Tooltip("Score scriptable object.")]
		private ScoreObject score;

		[SerializeField]
		private Text scoreText;
		[SerializeField]
		private Text highScoreText;

		private void Start()
		{
			//Subscribe as a listener of score change events.
			score.AddListener(this);

			//Get current score and highscore.
			OnScoreUpdated();
			OnHighScoreUpdated();
		}

		/// <summary>
		/// Callback method for the score change event.
		/// </summary>
		public void OnScoreUpdated()
		{
			if (scoreText != null) scoreText.text = score.GetScore().ToString();
		}

		/// <summary>
		/// Callback method for the high score change event.
		/// </summary>
		public void OnHighScoreUpdated()
		{
			if (highScoreText != null) highScoreText.text = score.GetHighScore().ToString();
		}
	}
}