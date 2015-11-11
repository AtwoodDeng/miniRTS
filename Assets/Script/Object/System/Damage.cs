using UnityEngine;
using System.Collections;

public struct Damage  {

	public Damage(Robot _sender , float _damage = 0 , DamageType _type = DamageType.None )
	{
		damage = _damage;
		type = _type;
		sender = _sender;
	}

	public Robot sender;
	public float damage;
	public DamageType type;

	static public Damage ZERO = new Damage(null,0,DamageType.None);
}
