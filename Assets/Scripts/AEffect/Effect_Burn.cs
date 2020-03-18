using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Burn", menuName = "Effect/Burn", order = 0)]
public class Effect_Burn : AEffect
{        
    public float damage;
    public float speed_addition;
    public ImportantThings.DamageType damagetype = ImportantThings.DamageType.Value;
    public override void EffectInit()
    {
        target.speed += target.base_speed * speed_addition;
    }

    public override void EffectExit()
    {
        target.speed -= target.base_speed * speed_addition;
    }

    public override void EffectUpdate()
    {
        if(damagetype == ImportantThings.DamageType.Value)target.health -= damage;
        else if (damagetype == ImportantThings.DamageType.pHP) target.health -= target.health * damage;
        else if (damagetype == ImportantThings.DamageType.pMHP) target.health -= target.maxhealth * damage;
    }

}
