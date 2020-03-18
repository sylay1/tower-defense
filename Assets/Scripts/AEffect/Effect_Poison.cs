using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Poison", menuName = "Effect/Poison", order = 0)]
    public class Effect_Poison : AEffect
    {
        public float damage;
        public ImportantThings.DamageType damagetype = ImportantThings.DamageType.Value;
        public override void EffectUpdate()
        {
            if(damagetype == ImportantThings.DamageType.Value)target.health -= damage;
            else if (damagetype == ImportantThings.DamageType.pHP) target.health -= target.health * damage;
            else if (damagetype == ImportantThings.DamageType.pMHP) target.health -= target.maxhealth * damage;
            Debug.Log("Poison tick");
        }
    }