using UnityEngine;
using System.Collections;

public class RobotAttack : RobotBehavior {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		// Debug.Log(_robot.name + " attacking" );
		_robot.AttackUpdate();
		if ( _robot.isDestory)
		{
			animator.SetBool(UnityAnimationConstants.Robot.Parameters.isDead, true);
		}
		if ( !_robot.isMeetEnemy )
		{
			animator.SetBool(UnityAnimationConstants.Robot.Parameters.isAttack, false);
		}
		if ( !_robot.canAttack )
			animator.SetBool(UnityAnimationConstants.Robot.Parameters.canAttack, false);
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		_robot.AttackEnd();
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
