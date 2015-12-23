using UnityEngine;
using System.Collections;

public class Explorer : MonoBehaviour {

	[SerializeField] Robot _robot;
	[SerializeField] SphereCollider _collider;


	virtual public void Init(Robot _p_robot, Weapon.WeaponParameter parameter )
	{
		_robot = _p_robot;
		if(_collider == null )
			_collider = GetComponent<SphereCollider>();
		SetExploreRange(parameter.Range);
	}

	void OnTriggerEnter ( Collider col )
	{
		if ( _robot == null )
			return;
		Robot col_rob = col.GetComponent<Robot>();
		if (col_rob != null )
		{
			// Debug.Log(transform.parent.name + "Explorer get rob");
			if ( !col_rob.teamColor.Equals(_robot.teamColor) )
			{
					_robot.EnterEnemy(col_rob);
			}
		}
	}

	void OnTriggerExit ( Collider col )
	{
		if ( _robot == null )
			return;
		Robot col_rob = col.GetComponent<Robot>();
		if (col_rob != null )
		{
			if ( !col_rob.teamColor.Equals(_robot.teamColor) )
			{
					_robot.ExitEnemy(col_rob);
			}
		}
	}

	public void SetExploreRange(float range)
	{
		if (_collider == null )
			return;
		_collider.radius = range;
	}
}
