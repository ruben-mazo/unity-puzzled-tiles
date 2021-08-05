using UnityEngine;

namespace PuzzledTiles
{
	/// <summary>
	/// This class represents a square that can be part of a piece, or be placed on a slot of the board.
	/// </summary>
	public class Square : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("Linker scriptable object.")]
		private LinkerObject linker;

		private SpriteRenderer spriteRenderer;
		private Animator animator;

		private void Awake()
		{
			spriteRenderer = GetComponentInChildren<SpriteRenderer>();
			animator = GetComponentInChildren<Animator>();

			//Get a random sprite for the square from a list.
			if (spriteRenderer != null) spriteRenderer.sprite = linker.GetRandomSquare();
		}

		/// <summary>
		/// Place the square in a board slot.
		/// </summary>
		public void Place(Slot slot)
		{
			transform.parent = slot.transform;
			transform.position = slot.transform.position;
			slot.SetSquare(this);
		}

		public void SetSortingOrder(int order)
		{
			if(spriteRenderer != null) spriteRenderer.sortingOrder = order;
		}

		/// <summary>
		/// Change the square color.
		/// </summary>
		public void SetColor(Color color)
		{
			if (spriteRenderer != null) spriteRenderer.color = color;
		}

		/// <summary>
		/// Destroy the square.
		/// </summary>
		public void Destroy()
		{
			if (animator != null) animator.SetTrigger("destroy");
		}
	}
}