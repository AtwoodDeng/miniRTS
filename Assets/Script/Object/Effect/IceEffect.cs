using UnityEngine;
using System.Collections;

public class IceEffect : Effect {


	public void Init(float CDDown, string SpeedDwon , float Time )
	{
        Effect.EffectParameter ep = new Effect.EffectParameter();
        ep.PPercentage = CDDown;
        ep.Parameter1 = SpeedDwon;
        ep.PTime = Time;

        this.Init(ep);
	}
	public override void Init (EffectParameter _p) {
        base.Init(_p);
        numbericType = EffectNumbericType.Percentage;
        type = EffectType.Ice;
        coverType = EffectCoverType.Cover; 
   }

	public override float GetCoolDown (float _base) {
        return _base / ( 1 - parameter.PPercentage);
    }

	public override float GetSpeed (float _base) {
        float speedDown = float.Parse(parameter.Parameter1);
        return _base * (1 - speedDown);
    }
}
