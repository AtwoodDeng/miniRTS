using UnityEngine;
using System.Collections;

public class MagicBall : Bullet {

	public float forceIntense;
	public Rigidbody rigidbody;

	Transform myTarget;
	override public void Shoot(Transform target , float _speed)
	{
		if (rigidbody == null)
			rigidbody = GetComponent<Rigidbody>();


		rigidbody.velocity = Global.GetRandomDirection() * _speed * 10f;
		myTarget = target;

		// transform.LookAt(transform.position+toward);
	}

	public override void UpdateBullet () {
		if (myTarget != null)
			rigidbody.AddForce(forceIntense * (myTarget.position - transform.position));
		else
			SelfDestory();
		Vector3 toward = rigidbody.velocity.normalized;
        transform.LookAt(transform.position+toward);
    }
}
