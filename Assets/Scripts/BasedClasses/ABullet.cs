﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABullet : MonoBehaviour
{
    public float damage = 1;
    public float speed = 10;
    public GameObject Target = null;
    public Vector3 targetPos = Vector3.zero;
    public AEffect effect = null;
    public void Die()
    {
        Destroy(gameObject);
    }

}
