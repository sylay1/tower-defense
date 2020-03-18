using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;
    public ATower towerToPlace;
    public enum STATE
    {
        DESTRUCTION,BUILDING,UPGRADING,NOTHING
    }
    public STATE state = STATE.NOTHING;
    public GameObject MouseCheck,TowerRangeIndicator;
    public SpriteRenderer mcr;
    public ImportantThings im;
    public List<ATower> TowersToPlace = new List<ATower>();
    public List<ATower> Towers = new List<ATower>();
    // Update is called once per frame
    private void Start()
    {
        gm = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (state == STATE.BUILDING)
            {
                Destroy(MouseCheck);
                state = STATE.NOTHING;
            }
            else state = STATE.BUILDING;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(MouseCheck)Destroy(MouseCheck);
            state = STATE.NOTHING;
        }

        if (state == STATE.BUILDING)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))ChangeTowertype(TowersToPlace[0]);
            if(Input.GetKeyDown(KeyCode.Alpha2))ChangeTowertype(TowersToPlace[1]);
            if(Input.GetKeyDown(KeyCode.Alpha3))ChangeTowertype(TowersToPlace[2]);
            if(Input.GetKeyDown(KeyCode.Alpha4))ChangeTowertype(TowersToPlace[3]);
            if(Input.GetKeyDown(KeyCode.Alpha5))ChangeTowertype(TowersToPlace[4]);
            if(Input.GetKeyDown(KeyCode.Alpha6))ChangeTowertype(TowersToPlace[5]);
            if(Input.GetKeyDown(KeyCode.Alpha7))ChangeTowertype(TowersToPlace[6]);
            if(Input.GetKeyDown(KeyCode.Alpha8))ChangeTowertype(TowersToPlace[7]);
            if(Input.GetKeyDown(KeyCode.Alpha9))ChangeTowertype(TowersToPlace[8]);
            if (!MouseCheck)
            {
                MouseCheck = new GameObject();
                var c = MouseCheck.AddComponent<CircleCollider2D>();
                mcr = MouseCheck.AddComponent<SpriteRenderer>();
                var tsr = towerToPlace.GetComponent<SpriteRenderer>();
                var indicator = Instantiate(TowerRangeIndicator,MouseCheck.transform.position,MouseCheck.transform.rotation,MouseCheck.transform);
                indicator.transform.localScale = new Vector3(towerToPlace.radious/4,towerToPlace.radious/4,1);
                mcr.sprite = tsr.sprite;
                mcr.color =new Color( tsr.color.r,tsr.color.g,tsr.color.b,0.5f);
                MouseCheck.transform.localScale = towerToPlace.transform.lossyScale;
                Debug.Log(towerToPlace.transform.localScale.x + "  "+towerToPlace.transform.lossyScale.x );
                c.radius = towerToPlace.radiousC/towerToPlace.transform.lossyScale.x;
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
        if (Input.GetMouseButtonDown(0) &&state ==STATE.BUILDING)
        {
            StartCoroutine(BuildATower());
        }
    }

    public bool CanBuild()
    {
        if (!MouseCheck) {                
            mcr.color =new Color( mcr.color.r,mcr.color.g,mcr.color.b,0.2f);
            return false;}
        MouseCheck.GetComponent<CircleCollider2D>().radius = towerToPlace.radiousC/towerToPlace.transform.localScale.x;
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
        b = !b;
        if (!b) { mcr.color =new Color( mcr.color.r,mcr.color.g,mcr.color.b,0.2f);return false;}
        foreach (var VARIABLE in Towers)
        {
            var f = Vector3.Distance(MouseCheck.transform.position, VARIABLE.transform.position);
            if (VARIABLE.radiousC+towerToPlace.radiousC > f)
            {
                mcr.color =new Color( mcr.color.r,mcr.color.g,mcr.color.b,0.2f); return false;
            }
        }
        if(b) mcr.color =new Color( mcr.color.r,mcr.color.g,mcr.color.b,0.5f);
        return b;
    }

    public void ChangeTowertype(ATower tower)
    {
        Destroy(MouseCheck);
        MouseCheck = null;
        towerToPlace = tower;
        MouseCheck = new GameObject();
        var c = MouseCheck.AddComponent<CircleCollider2D>();
        mcr = MouseCheck.AddComponent<SpriteRenderer>();
        
        var tsr = towerToPlace.GetComponent<SpriteRenderer>();
        mcr.sprite = tsr.sprite;
        mcr.color =new Color( tsr.color.r,tsr.color.g,tsr.color.b,0.5f);
        MouseCheck.transform.localScale = towerToPlace.transform.lossyScale;
        Debug.Log(towerToPlace.transform.localScale.x + "  "+towerToPlace.transform.lossyScale.x );
        c.radius = towerToPlace.radiousC/towerToPlace.transform.lossyScale.x;
        var indicator = Instantiate(TowerRangeIndicator,MouseCheck.transform.position,MouseCheck.transform.rotation,MouseCheck.transform);
        indicator.transform.localScale = new Vector3(towerToPlace.radious/4,towerToPlace.radious/4,1);
        MouseCheck.layer = LayerMask.NameToLayer("Abstract");
        MouseCheck.tag = "MouseCheck";
    }

    IEnumerator BuildATower()
    {
        yield return new WaitForEndOfFrame(); 
        yield return new WaitForEndOfFrame(); 
        if(CanBuild())Towers.Add(Instantiate(towerToPlace, MouseCheck.transform.position, transform.rotation));
    }
}