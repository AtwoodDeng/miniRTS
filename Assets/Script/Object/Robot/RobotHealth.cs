using UnityEngine;
using System.Collections;

public class RobotHealth {

	public float health{
		get{return _health;}
	}
	float _health;

	Robot robot;

	public virtual void Init(Robot _robot, float initHeal)
	{
		robot = _robot;
		_health = initHeal;
	}

	public virtual bool RecieveDamage(Damage dmg)
	{
		_health -= dmg.damage;
		if ( _health < 0 )
			return true;
		return false;
	}
}
