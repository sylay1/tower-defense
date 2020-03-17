using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class AEffect : ScriptableObject
{
    public enum EffectType
    {
        Poison,//Enemy takes damage, for some time in some time interval, can be percentage or fixed value
        Slow,//Enemy is slowed for some time, its current speed is lower than its base speed
        Stun,//Enemy is stunned and effect is blocking its behaviour for some time of course it cant move
        Burn,//Enemy gets damage but it also becomes a little more faster
        Freeze,//Enemy gets a short stun, and after that gets damage over time along with a slow(its value is lower over time)//It will be hard to balance tho
        Drunk,//Enemy speed varies, and is pretty random, but also is in some range like (0.5f,2f) and is changed in some time interval
        Deadly_Heal//Enemy Gets -50% current HP but gets a heal for 40% current HP, great for opening and fast kill build
        //here is space for other shit I will 
    };
    public EffectType type;
    public bool isInfinite;
    public float duration;
    public float duration_timer;
    public float effect_interval;
    public AEnemy target;
    public virtual  void EffectInit()
    {
    }
    public virtual void EffectUpdate()
    {
        Debug.Log("buuuug");
    }

    public virtual  void EffectExit()
    {
        
    }
}
