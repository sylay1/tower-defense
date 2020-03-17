using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : ABullet
{
    void Update()
    {
        Vector3 dis = targetPos - transform.position;
        transform.Translate((dis).normalized * speed * Time.deltaTime);
        if (dis.magnitude<0.2f) //here paste the check is it is the enemy 
        {
            if (Target)
            {
                Target.SendMessage("GetDamage", damage);
                Target.GetComponent<AEnemy>().AddEffect(effect);
            }
            Die();
        }
        if (Target)
        {
            targetPos = Target.transform.position;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (true)//here paste the check is it is the enemy 
        {
            Target.GetComponent<AEnemy>().AddEffect(effect);
            other.gameObject.SendMessage("GetDamage",damage);
            Die();
        }
    }

}
