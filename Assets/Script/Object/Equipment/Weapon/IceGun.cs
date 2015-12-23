using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IceGun : BasicGun {


	public override Damage GetDamage (Robot _robot) {
        IceEffect e = new IceEffect();
        e.Init(weaponParameter.PPercentage, weaponParameter.Parameter1, weaponParameter.PTime);
        Damage dmg = new Damage(_robot, weaponParameter.Damage,weaponParameter.DamageType);

        dmg.AddEffect(e);
        return dmg;
    }
}
