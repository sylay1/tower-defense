using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameObject towerToPlace;

    public enum STATE
    {
        DESTRUCTION,BUILDING,UPGRADING,NOTHING
    }
    public STATE state = STATE.NOTHING;
    public GameObject MouseCheck;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            state = STATE.BUILDING;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            state = STATE.NOTHING;
        }

        if (state == STATE.BUILDING)
        {
            if (!MouseCheck)
            {
                MouseCheck = new GameObject();
                var c = MouseCheck.AddComponent<CircleCollider2D>();
                c.radius = 0.5f;
                MouseCheck.layer = LayerMask.NameToLayer("Abstract");
                MouseCheck.tag = "MouseCheck";
            }
            MouseCheck.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
        }
        if (Input.GetMouseButtonDown(1)&&MouseCheck&&state==STATE.BUILDING)
        {
            var wp = GameObject.FindObjectsOfType<Waypoint>();
            bool b = false;
            foreach (Waypoint w in wp)
            {
               b|= w.CheckForMouse();
               if (b){ Debug.Log("found something");break;}
            }
            if (!b)
            {
            Debug.Log("found nothing");
            }
        }
    }
}
