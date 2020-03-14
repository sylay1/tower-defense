using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Info : MonoBehaviour
{
    public Image Healthbar_prefab;
    public EnemySpawner spawner;
    public List<Image> healthbars;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var f = spawner.GetAllAEnemies();
        while (f.Length<healthbars.Count)
        {
            Destroy(healthbars[healthbars.Count-1]);
            healthbars.Remove(healthbars[healthbars.Count-1]);
        }

        for (int i =0;i<f.Length;i++)
        {
            
            if (i >= healthbars.Count)
            {
                var go = Instantiate(Healthbar_prefab, transform);
                healthbars.Add(go);
            }
            healthbars[i].transform.position = Camera.main.WorldToScreenPoint(f[i].transform.position)+new Vector3(0,50,0);
            healthbars[i].fillAmount = f[i].health / f[i].maxhealth;
        }
    }
}
