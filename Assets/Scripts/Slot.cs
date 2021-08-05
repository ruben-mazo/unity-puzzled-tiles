using PuzzledTiles.Misc;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzledTiles
{
	/// <summary>
	/// This class represents a slot of the board where the squares can be placed.
	/// The slot is part of a grid and belongs to a row and a column.
	/// </summary>
	public class Slot : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("Score scriptable object.")]
		private ScoreObject score;

		/// <summary>
		/// The square placed on this slot.
		/// </summary>
		private Square square;

		/// <summary>
		/// A flag to determine if the square in this slot must be destroy or not.
		/// </summary>
		private bool removeFlag;

		/// <summary>
		/// True if the slot contains a square; false otherwise.
		/// </summary>
		public bool HasSquare()
		{
			return square != null;
		}

		/// <summary>
		/// Set a square to this slot and add 1 point to the current score.
		/// </summary>
		/// <param name="square"></param>
		public void SetSquare(Square square)
		{
			this.square = square;
			score.Add(1);
		}

		/// <summary>
		/// Mark this slot to be emptied by removing its square.
		/// </summary>
		public void MarkToRemove()
		{
			if (HasSquare()) removeFlag = true;
		}

		/// <summary>
		/// Remove the slot's square if it's not empty and add 1 point to the score.
		/// </summary>
		public void RemoveSquareIfMarked()
		{
			if (HasSquare() && removeFlag)
			{
				square.Destroy();
				square = null;
				score.Add(1);
			}
			removeFlag = false;
		}
	}

	/// <summary>
	/// A helper class to compare two slots by their rows.
	/// </summary>
	public class SlotComparerRows : IComparer<Slot>
	{
		public int Compare(Slot x, Slot y)
		{
			if (x == null) return -1;
			else if (y == null) return 1;

			Vector2 posA = x.transform.position;
			Vector2 posB = y.transform.position;

			if (posA.y < posB.y) return -1;
			if (posA.y > posB.y) return 1;

			if (posA.x < posB.x) return -1;
			if (posA.x > posB.x) return 1;
			return 0;
		}
	}

	/// <summary>
	/// A helper class to compare two slots by their columns.
	/// </summary>
	public class SlotComparerColumns : IComparer<Slot>
	{
		public int Compare(Slot x, Slot y)
		{
			if (x == null) return -1;
			else if (y == null) return 1;

			Vector2 posA = x.transform.position;
			Vector2 posB = y.transform.position;

			if (posA.x < posB.x) return -1;
			if (posA.x > posB.x) return 1;

			if (posA.y < posB.y) return -1;
			if (posA.y > posB.y) return 1;
			return 0;
		}
	}
}

