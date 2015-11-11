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
	}

	public WeaponParameter weaponParameter;
	public Robot robot;
	public Explorer explorer;

	bool isInit = false;

	// virtual public void Init(WeaponParameter _parameter, Robot _robot)
	// {
	// 	weaponParameter = _parameter;
	// 	robot = _robot;
	// 	if ( explorer != null )
	// 		explorer.Init(_robot);

	// 	isInit = true;
	// }

	public override void Init (System.Data.DataRow data, Robot _robot) {
		weaponParameter =  DataManager.Instance.GetWeaponParameter( data );
		robot = _robot;
		if ( explorer != null )
			explorer.Init(_robot);

		isInit = true;
    }
}
