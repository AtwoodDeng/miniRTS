using UnityEngine;
using System.Collections;

public class Pusher : Equipment {

	[System.Serializable]
	public struct PusherParameter 
	{
		public string Name;
		public float CreateTime;
		public float MoveSpeed;
		public float TurnAngleSpeed;

		//parameter
		public int PNumber;
		public float PPercentage;
		public float PTime;
		public float PDamage;
		public float PRange;
		public string Parameter1;
		public string Parameter2;

		public string Info;
	}

	PusherParameter parameter;
	// Robot robot;

	public override void Init (DataRow data, Robot _robot) {
		type = Type.Pusher;
		parameter =  DataManager.Instance.GetPusherParameter( data );
		base.Init(data, _robot);
    }

	public override void InitParameterI (ref Robot.BodyParameter _parameter) {
       	_parameter.MoveSpeed = parameter.MoveSpeed;
      	_parameter.TurnAngleSpeed = parameter.TurnAngleSpeed;


    }

    public override void InitParameterII (ref Robot.BodyParameter _parameter) {
       
    }
}
