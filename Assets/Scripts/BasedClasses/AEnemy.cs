using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public abstract class AEnemy : MonoBehaviour
{
     public float health;
     public float maxhealth;
     public float speed,base_speed;
     public float damage;
     public Waypoint Nextpoint = null;
     public static float epsilon = 0.2f;
     public Vector2 _moveDirection = new Vector2();
     public AEffect effect;
     public List<AEffect.EffectType> immunityList = new List<AEffect.EffectType>();//I would also recommend to make a resistance list
     void GetDamage(float damage) {     
          health -= damage;
          if (health < 0) Die();
     }

     void Die()
     {
          Destroy(gameObject);
     }
     public IEnumerator Tick()
     {
          effect.EffectInit();
          while (effect.duration_timer <= effect.duration||effect.isInfinite)
          {
               Debug.Log("poison");
               effect.EffectUpdate();
               effect.duration_timer += effect.effect_interval;
               yield return new WaitForSeconds(effect.effect_interval);
          }
          effect.EffectExit();
          Destroy(effect);
          effect = null;
     }

     public void AddEffect(AEffect e)
     {
          if (!e) return;
          if (!effect)
          {
               foreach (var VARIABLE in immunityList)
               {
                    if (VARIABLE == e.type) return;
               }
               effect = Instantiate(e);
               effect.target = GetComponent<AEnemy>();
               StartCoroutine(Tick());
          }
          else if (e.type == effect.type) {effect.duration_timer = 0;
               return;
          }
     }
}
