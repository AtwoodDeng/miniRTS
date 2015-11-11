using UnityEngine;
using System.Collections;

public class BasicGun : Weapon {


	float coolDown = 0;

	public override void AttackUpdate (Robot _robot) {
		coolDown -= Time.deltaTime;
        if ( _robot.GetEnemy() != null )
        {
            if ( _robot.RotateTo( _robot.GetEnemy().transform.position ) )
            {
            	if ( coolDown < 0 )
            	{
            		Shoot(_robot);
            		coolDown = weaponParameter.ShootCoolDown;
            	}
            }
        }

    }

    public void Shoot(Robot _robot)
    {
        Debug.Log("Shoot");
    	var obj = Instantiate( weaponParameter.BulletPrefab ) as GameObject;
        obj.transform.parent = transform;
        obj.transform.position = transform.position;
    	Bullet bullet = obj.GetComponent<Bullet>();
    	var dmg = new Damage(_robot, weaponParameter.Damage,weaponParameter.DamageType);
        Vector3 toward = _robot.GetFirstEnemy().transform.position - transform.position;
    	bullet.Init(_robot.teamColor,dmg,toward,weaponParameter.ShootSpeed);
    }

}
