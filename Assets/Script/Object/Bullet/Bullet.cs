using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class Bullet : MonoBehaviour {

	public Damage damage{
		get {return _damage;}
	}
	Damage _damage;

	public TeamColor teamColor{
		get {return _teamColor;}
	}
	TeamColor _teamColor = TeamColor.None;

	Rigidbody _rigidbody;

	void Awake()
	{	
		Init();
	}

	virtual public void Init(TeamColor teamColor = TeamColor.None , Damage dmg = new Damage() , Vector3 toward = new Vector3() , float speed = 0 )
	{
		gameObject.tag = Global.BULLET_TAG;
		_damage = dmg;

		if ( _rigidbody == null )
			_rigidbody = GetComponent<Rigidbody>();

		Shoot(toward,speed);
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
		robot.CauseDamage(_damage);

		SelfDestory();
	}

	public virtual void SelfDestory()
	{
		Destroy(gameObject);
	}

	Vector3 toward;
	float speed;
	public void Shoot(Vector3 _toward , float _speed)
	{
		toward = _toward.normalized;
		speed = _speed;

		transform.LookAt(transform.position+_toward);
	}

	void Update()
	{
		transform.position += toward.normalized * speed;
	}
}
