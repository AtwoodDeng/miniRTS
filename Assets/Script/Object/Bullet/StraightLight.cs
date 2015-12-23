using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;


public class StraightLight : Bullet {

	[SerializeField] tk2dSprite bulletSprite;

	[SerializeField] float fadeOutTime = 2f;

	public override void Init (Weapon weapon, Damage dmg = default(Damage), Transform target = null ) {
		base.BasicInit(weapon, dmg , target );

		Vector3 toward = weapon.robot.transform.forward;

		transform.LookAt(transform.position+toward);

		if (bulletSprite == null )
			bulletSprite = GetComponent<tk2dSprite>();
		if (bulletSprite != null)
			StartCoroutine(FadeOut(fadeOutTime));
    }

    IEnumerator FadeOut(float time)
    {
    	float timer = 0;
    	while(true)
    	{
    		timer += Time.deltaTime;
    		bulletSprite.color = Global.GetColorByAlpha(bulletSprite.color,1f - timer/time);
    		if ( timer > time )
    		{
    			SelfDestory();
    			yield break;
    		}
    		yield return null;
    	}
    }



	public override void UpdateBullet () {
    }

    override public void Hit(Robot robot)
	{
		Debug.Log("StraightLight hit " + robot.name);
		Assert.IsNotNull<Robot>(robot);
		Assert.IsTrue(robot.teamColor != teamColor);
		robot.RecieveDamage(damage);
	}
}
