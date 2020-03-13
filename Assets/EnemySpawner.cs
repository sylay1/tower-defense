using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Waypoint))]
public class EnemySpawner : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public int enemiesSpawned = 0;
    public float timebetween= 1;

    private Waypoint wp;
    // Start is called before the first frame update
    void Start()
    {
        wp = GetComponent<Waypoint>().next;
        StartCoroutine(spawn());
    }

    public void SpawnEnemy(Enemy e )
    {
        Enemy a = Instantiate(e,transform.position,transform.rotation);
        a.Nextpoint = wp;
    }

    public IEnumerator spawn()
    {
        while (enemiesSpawned < enemies.Count)
        {
            SpawnEnemy(enemies[enemiesSpawned++]);
            yield return new WaitForSeconds(timebetween);
        }
    }
    
}
