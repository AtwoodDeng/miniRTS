 using UnityEngine;
using System.Collections;

public class Weapon : Equipment {

	[System.Serializable]
	public struct WeaponParameter 
	{
		public string Name;
		public float ShootCoolDown;
		public float Damage;
		public DamageType DamageType;
		public float ShootSpeed;
		public GameObject BulletPrefab;
		public string BulletName;
		public float CreateTime;
		public float Range;
		public float PowerCost;

		//parameter
		public int PNumber;
		public float PPercentage;
		public float PTime;
		public float PDamage;
		public float PRange;
		public string Parameter1;
		public string Parameter2;

		//Information
		public string Info;
	}

	public WeaponParameter weaponParameter;
	// public Robot robot;
	public Explorer explorer;

	// bool isInit = false;

	// virtual public void Init(WeaponParameter _parameter, Robot _robot)
	// {
	// 	weaponParameter = _parameter;
	// 	robot = _robot;
	// 	if ( explorer != null )
	// 		explorer.Init(_robot);

	// 	isInit = true;
	// }

	public override void Init (DataRow data, Robot _robot) {
		type = Type.Weapon;
		weaponParameter =  DataManager.Instance.GetWeaponParameter( data );
		
		if (explorer == null )
		{
			explorer =  transform.GetComponentInChildren<Explorer>();
		}
		if ( explorer != null )
			explorer.Init(_robot,weaponParameter);

		base.Init(data, _robot);
    }
}
