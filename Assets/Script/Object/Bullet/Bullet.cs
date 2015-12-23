using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class Bullet : MonoBehaviour {

	public Damage damage{
		get {return _damage;}
	}
	[SerializeField]Damage _damage;

	public TeamColor teamColor{
		get {return _teamColor;}
	}
	[SerializeField]TeamColor _teamColor = TeamColor.None;

	Rigidbody _rigidbody;

	virtual public void Init(Weapon weapon, Damage dmg  , Transform target = null)
	{
		BasicInit(weapon,dmg,target);
		Shoot(target,weapon.weaponParameter.ShootSpeed);
	}

	protected void BasicInit(Weapon weapon = null , Damage dmg = null, Transform target = null )
	{
		gameObject.tag = Global.BULLET_TAG;
		_damage = dmg;
		_teamColor = weapon.robot.teamColor;

		if ( _rigidbody == null )
			_rigidbody = GetComponent<Rigidbody>();
	}


	void OnTriggerEnter(Collider col)
	{
		Robot robot = col.GetComponent<Robot>();
		if ( robot != null )
		{
			if ( robot.teamColor != teamColor)
			{
				Hit(robot);
			}
		}
	}

	public virtual void Hit(Robot robot)
	{
		Assert.IsNotNull<Robot>(robot);
		Assert.IsTrue(robot.teamColor != teamColor);
		robot.RecieveDamage(damage);

		SelfDestory();
	}

	public virtual void SelfDestory()
	{
		Destroy(gameObject);
	}

	Vector3 toward;
	float speed;
	virtual public void Shoot(Transform target , float _speed)
	{
		if ( target == null)
			toward = Vector3.left;
		else
			toward = (target.position - transform.position).normalized;

		speed = _speed;

		transform.LookAt(transform.position+toward);
	}

	virtual public void UpdateBullet()
	{
		transform.position += toward.normalized * speed;
	}

	void Update()
	{
		UpdateBullet();
	}
}
