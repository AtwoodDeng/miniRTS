using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.SerializableAttribute]
public class Damage  {

	public Damage(Robot _sender , float _damage = 0 , DamageType _type = DamageType.None
				  )
	{
		damage = _damage;
		type = _type;
		sender = _sender;
		effectList = new List<Effect>();
	}

	public Robot sender;
	public float damage;
	public DamageType type;

	public List<Effect> effectList = new List<Effect>();

	static public Damage ZERO = new Damage(null,0,DamageType.None);


	public void AddEffect(Effect e )
	{
		// if (e != null)
		effectList.Add(e);
	}
}
