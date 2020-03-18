using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_Info : MonoBehaviour
{
    public TMPro.TextMeshProUGUI stateInfo;
    public TMPro.TextMeshProUGUI buildingInfo;
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
            healthbars[i].transform.position = Camera.main.WorldToScreenPoint(f[i].transform.position)+new Vector3(0,Screen.height*0.05f,0);
            healthbars[i].fillAmount = f[i].health / f[i].maxhealth;
        }

        stateInfo.text = "chuj";
        switch (GameMaster.gm.state)
        {
            case GameMaster.STATE.NOTHING:
                stateInfo.text = "doin nothin";
                break;
            case GameMaster.STATE.BUILDING:
                stateInfo.text = "building: "+GameMaster.gm.towerToPlace.name;
                break;
            case GameMaster.STATE.DESTRUCTION:
                stateInfo.text = "destructing";
                break;
        }
        buildingInfo.text = "chuj";
        if (GameMaster.gm.state == GameMaster.STATE.BUILDING)
        {
            if (GameMaster.gm.CanBuild())
            {
                buildingInfo.text = "Can Build";
            }
            else buildingInfo.text = "Can't build";
        }
        else
        {
            buildingInfo.text = "";
        }
    }
}
