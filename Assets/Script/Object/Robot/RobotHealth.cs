using UnityEngine;
using System.Collections;

public class RobotHealth  {

	public float health{
		get{return _health;}
	}
	float _health;

	public float power{
		get{return _power;}
	}
	float _power;

	Robot robot;

	public virtual void Init(Robot _robot, float initHeal , float initPower)
	{
		robot = _robot;
		_health = initHeal;
		_power = initPower;
	}

	public virtual bool RecieveDamage(Damage dmg)
	{
		_health -= dmg.damage;
		if ( _health < 0 )
			return true;
		return false;
	}

	public virtual bool UsePower( float p )
	{
		Debug.Log("use power " + p.ToString() + "/" + _power.ToString());
		if ( _power > p )
		{
			_power -= p;
			return true;
		}

		return false;
	}
}
