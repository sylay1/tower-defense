using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : ATower
{ 
    void Start()
    {
        StartCoroutine(CheckEnemy());
    }

    // Update is called once per frame
    
    public IEnumerator CheckEnemy()
    {
        while (true)
        {
            if (!CurrentEnemy || Vector3.Distance(CurrentEnemy.transform.position, transform.position) > radious)
            {
                Collider2D[] vars = new Collider2D[1];
                var tab = Physics2D.OverlapCircle(transform.position, radious, GameMaster.gm.im.whatIsEnemy, vars);
                if (vars[0] != null) CurrentEnemy = vars[0].GetComponent<AEnemy>();
                else CurrentEnemy = null;
            }
            yield return new WaitForSeconds(0.1f);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,radious);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position,radiousC);
        Gizmos.color = Color.red;
         if(CurrentEnemy)Gizmos.DrawWireSphere(CurrentEnemy.transform.position,0.5f);
    }
}
