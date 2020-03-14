using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEnemy : MonoBehaviour
{
     public float health;
     public float maxhealth;
     public float speed;
     public float damage;
     public Waypoint Nextpoint = null;
     public static float epsilon = 0.2f;
     public Vector2 _moveDirection = new Vector2();

     void GetDamage(float damage) {     
          health -= damage;
          if (health < 0) Die();
     }

     void Die()
     {
          Destroy(gameObject);
     }
}
