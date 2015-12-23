using UnityEngine;
using System.Collections;

public class MagicBallGun : BasicGun {

	public override void Shoot (Robot _robot) {
        if ( _robot.UsePower(_robot.GetWeaponPowerCost(weaponParameter.PowerCost)) )
        	MagicShoot(_robot);
        else
        	NormalShoot(_robot);
    }

    virtual public void NormalShoot(Robot _robot) {
        //set bullet
    	var obj = Instantiate( weaponParameter.BulletPrefab ) as GameObject;
        obj.transform.parent = LogicManager.Instance.BulletField;
        obj.transform.position = transform.position;
    	Bullet bullet = obj.GetComponent<Bullet>();

        //get damage
    	var dmg = GetDamage(_robot);

        //set bullet's damage
    	bullet.Init(this,dmg,_robot.GetFirstEnemy().transform);
    }

	virtual public void MagicShoot(Robot _robot) {
        Debug.Log("Magic Shoot!");
		for( int i = 0 ; i < weaponParameter.PNumber + 1 ; ++ i)
            NormalShoot(_robot);
	}
}
