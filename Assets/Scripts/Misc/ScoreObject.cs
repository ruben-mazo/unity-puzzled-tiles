using System;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzledTiles.Misc
{
	/// <summary>
	/// A scriptable object that contains the current score and high score values in the game.
	/// </summary>
	[CreateAssetMenu(fileName = "Score", menuName = "Puzzle/Score")]
	public class ScoreObject : ScriptableObject
	{
		[SerializeField]
		private int score;
		[SerializeField]
		private int highScore;

		/// <summary>
		/// A list of all listeners subscribed to this object's events.
		/// </summary>
		private List<IScoreListener> listeners = new List<IScoreListener>();

		/// <summary>
		/// Initialize the list of listeners and the score values.
		/// </summary>
		public void Init()
		{
			listeners.Clear();
			score = 0;
			highScore = LoadHighScore();
		}

		/// <summary>
		/// Add a listener to the list.
		/// </summary>
		/// <param name="listener">The new listener.</param>
		public void AddListener(IScoreListener listener)
		{
			listeners.Add(listener);
		}

		/// <summary>
		/// Add a certain amount of points to the current score.
		/// If the new score is higher than the high score, it replaces the high score.
		/// </summary>
		/// <param name="newPoints"></param>
		public void Add(int newPoints)
		{
			score += newPoints;
			listeners.ForEach(listener => listener.OnScoreUpdated());
			if (score > highScore)
			{
				highScore = score;
				listeners.ForEach(listener => listener.OnHighScoreUpdated());
				SaveHighScore();
			}
		}

		/// <summary>
		/// Get the current score.
		/// </summary>
		public int GetScore()
		{
			return score;
		}

		/// <summary>
		/// Get the high score.
		/// </summary>
		public int GetHighScore()
		{
			return highScore;
		}

		/// <summary>
		/// A data structure to save and load the high score.
		/// </summary>
		[Serializable]
		private class ScoreData
		{
			public int score;
			public ScoreData(int score) => this.score = score;
		}

		/// <summary>
		/// Save high score into a file in the game directory.
		/// </summary>
		public void SaveHighScore()
		{
			SaveAndLoad.SaveData<ScoreData>(new ScoreData(highScore), "save", "highScore.pzl");
		}

		/// <summary>
		/// Load high score from a file in the game directory, or 0 if the file doesn't exist.
		/// </summary>
		public int LoadHighScore()
		{
			ScoreData data = SaveAndLoad.LoadData<ScoreData>("save", "highScore.pzl");
			return data != null ? data.score : 0;
		}
	}
}