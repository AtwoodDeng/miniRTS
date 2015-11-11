using UnityEngine;
using System.Collections;

public class Pusher : Equipment {

	[System.Serializable]
	public struct PusherParameter
	{
		public string Name;
		public float CreateTime;
	}

	PusherParameter parameter;
	Robot robot;

	virtual public void Init(PusherParameter _parameter, Robot _robot)
	{
		parameter = _parameter;
		robot = _robot;
	}
}
