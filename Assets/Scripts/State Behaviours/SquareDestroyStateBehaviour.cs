using UnityEngine;

namespace PuzzledTiles
{
	/// <summary>
	/// A state behaviour to destroy squares after the destroy animation.
	/// </summary>
	public class SquareDestroyStateBehaviour : StateMachineBehaviour
	{
		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			Destroy(animator.gameObject);
		}
	}
}