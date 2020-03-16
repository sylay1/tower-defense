using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AEnemy
{

    public float _distanceToNext
    {
        get
        {
            if (Nextpoint != null)
            {
                return Vector3.Distance(Nextpoint.transform.position, transform.position);
            }
            else return 0;
        }
    }

    void Update()
    {
        if (Nextpoint!=null)
        {
            _moveDirection = (Nextpoint.transform.position - transform.position);
            transform.Translate(_moveDirection.normalized*speed*Time.deltaTime);
            if (Vector3.Distance(transform.position, Nextpoint.transform.position) < epsilon)
            {
                Nextpoint = Nextpoint.next;
                if (Nextpoint == null) Destroy(gameObject);//here also do the damage to the player base
            }
        }
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        if (health < 0) Die();
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
