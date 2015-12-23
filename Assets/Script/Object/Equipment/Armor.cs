using UnityEngine;
using System.Collections;

public class Armor : Equipment {

	[System.Serializable]
	public struct ArmorParameter 
	{
		public string Name;
		public float CreateTime;
		public float Armor;

		public int PNumber;
		public float PPercentage;
		public float PTime;
		public float PDamage;
		public float PRange;
		public string Parameter1;
		public string Parameter2;

		public string Info;
	}

	ArmorParameter parameter;
	// Robot robot;

	public override void Init (DataRow data, Robot _robot) {
		type = Type.Armor;
		parameter =  DataManager.Instance.GetArmorParameter( data );
		base.Init(data, _robot);
    }


	public override void InitParameterI (ref Robot.BodyParameter _parameter) {
       _parameter.Armor = parameter.Armor;
    }

    public override void InitParameterII (ref Robot.BodyParameter _parameter) {
    }
}
