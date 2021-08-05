using System.Collections.Generic;
using UnityEngine;

namespace PuzzledTiles
{
	/// <summary>
	/// This class is used to simulate a piece moving over the board,
	/// but it's just a list of relative positions.
	/// </summary>
	public class FakePiece
	{
		/// <summary>
		/// Absolute position of the first square in the list.
		/// </summary>
		public Vector2 Position { get; set; }

		/// <summary>
		/// Relative position of all the squares to the first one.
		/// </summary>
		public List<Vector2> FakeSquares { get; private set; }

		public FakePiece(Vector2 position, List<Vector2> fakeSquares)
		{
			Position = position;
			FakeSquares = fakeSquares;
		}
	}
}