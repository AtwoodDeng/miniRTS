using UnityEngine;
using System.Collections;

[System.SerializableAttribute]
public class Effect : Object {

	public enum EffectNumbericType
	{
		None,
		Number,
		Percentage,
	}

	public EffectNumbericType numbericType;

	public enum EffectType
	{
		None,
		Ice,
	}
	public EffectType type;

	public enum EffectCoverType
	{
		Cover,
		NotCover,
		Merge,
	}
	public EffectCoverType coverType;

	public struct EffectParameter
	{
		public float PDamage;
		public float PTime;
		public float PPercentage;
		public string Parameter1;
		public string Parameter2; 

		//info
		public string Info;
	}

	protected EffectParameter parameter;

	float duration;

	virtual public void Init( EffectParameter _p )
	{
		parameter = _p;
	}

	//Update timer and return if end effect
	public bool Update(Robot _robot)
	{	
		duration += Time.deltaTime;
		if (duration > parameter.PTime)
			return true;
		return false;
	}

	virtual public float GetCoolDown(float _base)
	{
		return _base;
	}

	virtual public float GetSpeed(float _base)
	{
		return _base;
	}

	virtual public void Merge(Effect other_e )
	{
		duration += other_e.parameter.PTime;
	}
}
