using UnityEngine;
using System.Collections;

public class BasicGun : Weapon {


	float coolDown = 0;

	public override void AttackUpdate (Robot _robot) {
		coolDown -= Time.deltaTime;
        _robot.canAttack = false;
        if ( _robot.GetEnemy() != null )
        {
            if ( _robot.RotateTo( _robot.GetEnemy().transform.position ) )
            {
                _robot.canAttack = true;
            	if ( coolDown < 0 )
            	{
            		Shoot(_robot);
            		coolDown = _robot.robotEffect.GetCoolDown( weaponParameter.ShootCoolDown) ;
            	}
            }
        }

    }

    virtual public void Shoot(Robot _robot)
    {
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

    virtual public Damage GetDamage(Robot _robot)
    {
        return new Damage(_robot, weaponParameter.Damage,weaponParameter.DamageType);
    }

}
