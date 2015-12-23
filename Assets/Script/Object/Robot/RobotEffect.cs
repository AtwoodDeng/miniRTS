using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotEffect  {

	Robot _robot;
	List<Effect> effectList = new List<Effect>();

	public void Init(Robot robot)
	{
		_robot = robot;
	}

	public void RecieveEffect(Effect e)
	{
		// if ( e != null )
		// Debug.Log("recieve Effect" + e.type.ToString());
		// if ( e != null )
		if (e.coverType == Effect.EffectCoverType.Cover)
		{
			foreach(Effect my_e in effectList)
			{
				if ( my_e.type == e.type)
				{
					effectList.Remove(my_e);
					break;
				}
			}
		}
		if (e.coverType == Effect.EffectCoverType.Merge)
		{
			foreach(Effect my_e in effectList)
			{
				if ( my_e.type == e.type)
				{
					my_e.Merge(e);
					return;
				}
			}
		}
		
		// type == Extend or other 
		effectList.Add(e);
	}

	public void UpdateEffect()
	{
		for(int i = effectList.Count -1 ; i >= 0 ; --i )
		{
			if ( effectList[i].Update(_robot) )
			{
				effectList.RemoveAt(i);
			}
		}
	}

	public float GetCoolDown(float baseCoolDown)
	{
		float res = baseCoolDown;
		foreach(Effect e in effectList )
			if ( e.numbericType == Effect.EffectNumbericType.Number)
				res = e.GetCoolDown(res);
		foreach(Effect e in effectList )
			if ( e.numbericType == Effect.EffectNumbericType.Percentage)
				res = e.GetCoolDown(res);
		return res;
	}

	public float GetSpeed(float speed)
	{
		float res = speed;
		foreach(Effect e in effectList )
			if ( e.numbericType == Effect.EffectNumbericType.Number)
				res = e.GetSpeed(res);
		foreach(Effect e in effectList )
			if ( e.numbericType == Effect.EffectNumbericType.Percentage)
				res = e.GetSpeed(res);
		return res;
	}
}
