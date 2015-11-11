using UnityEngine;
using System.Collections;

public class RobotBehavior : StateMachineBehaviour {

	protected Robot _robot;

	public Robot Rob {
		set {
			_robot = value;
		}
	}
}
