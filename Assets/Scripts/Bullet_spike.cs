using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_spike :ABullet
{
    public float lifetime = 3;
    public float lifetime_timer = 0;
    void Update()
    {
     transform.Translate(targetPos*Time.deltaTime*speed);
     lifetime_timer += Time.deltaTime;
     if (lifetime_timer > lifetime) Die();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (true)//here paste the check is it is the enemy 
        {
            other.gameObject.SendMessage("GetDamage",damage);
        }
    }
}