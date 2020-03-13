using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 1;
    public float speed = 10;
    public GameObject Target = null;
    public Vector3 targetPos = Vector3.zero;
    void Update()
    {
        Vector3 dis = targetPos - transform.position;
        transform.Translate((dis).normalized * speed * Time.deltaTime);
        if (dis.magnitude<0.2f) //here paste the check is it is the enemy 
        {
            Die();
        }
        if (Target)
        {
            targetPos = Target.transform.position;
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (true)//here paste the check is it is the enemy 
        {
            other.gameObject.SendMessage("GetDamage",damage);
            Die();
        }
    }

    public void Die()
    {
        if(Target)Target.SendMessage("GetDamage", damage);
        Destroy(gameObject);
    }
}
