using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float radious = 6;
    public GameObject BulletPrefab;
    public float Damage =10;
    public float level = 0;
    public float dps = 0.5f;
    public ContactFilter2D whatIsEnemy;
    public Enemy CurrentEnemy;
    public float _dpsTimer;
    void Start()
    {
        StartCoroutine(CheckEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        if (_dpsTimer > dps&&CurrentEnemy)
        {
            Shoot();
        }
        _dpsTimer += Time.deltaTime;
    }

    public void Shoot()
    {
        GameObject clone = Instantiate(BulletPrefab,transform.position,transform.rotation);
        Bullet b = clone.GetComponent<Bullet>();
        b.Target = CurrentEnemy.gameObject;
        b.damage = Damage;
        b.targetPos = CurrentEnemy.transform.position;
        _dpsTimer = 0;
    }

    public IEnumerator CheckEnemy()
    {
        while (true)
        {
            if (CurrentEnemy == null || Vector3.Distance(CurrentEnemy.transform.position, transform.position) > radious)
            {
                Collider2D[] vars = new Collider2D[1];
                var tab = Physics2D.OverlapCircle(transform.position, radious, whatIsEnemy, vars);
                if (vars[0] != null) CurrentEnemy = vars[0].GetComponent<Enemy>();
                else CurrentEnemy = null;
            }
            yield return new WaitForSeconds(0.1f);

        }
    }

    private void OnDrawGizmos()
    {
         Gizmos.color = Color.green;
         Gizmos.DrawWireSphere(transform.position,radious);
         Gizmos.color = Color.red;
         if(CurrentEnemy)Gizmos.DrawWireSphere(CurrentEnemy.transform.position,0.5f);
    }
}
