using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour, IDragHandler
{
    private Vector2 offset;
    public Waypoint next = null;
    public double atan;
    public double vectorsangle;
    public static GameObject MouseCheck;
    public float yValue;
    public Vector3 s = new Vector3();
    public Vector2 rotated = new Vector2();
    public GameObject SphereSprite, BoxSprite;
    void Start()
    {
        if (SphereSprite==null)
        {
            SphereSprite = Instantiate(Resources.Load("Wcircle") as GameObject,
                transform.position,transform.rotation,
                transform);
        }
        SphereSprite.transform.localScale = new Vector3(0.25f,0.25f,1);
        SphereSprite.GetComponent<SpriteRenderer>().color = new Color(0.5f,0.5f,0.5f,0.5f);
        SphereSprite.GetComponent<SpriteRenderer>().sortingOrder = -1;
        if (next)
        {
            s = -transform.position + next.transform.position;
            Rect r = new Rect((Vector2) transform.position, 
                new Vector2(Vector3.Distance(transform.position, next.transform.position) * 0.5f, 0.5f));
            atan = Math.Atan(-s.y / -s.x) * 180 / Math.PI;
            if (Mathf.Sign(s.x) == 1 && Mathf.Sign(s.y) == -1) atan = 180 + atan;
            if (Mathf.Sign(s.x) == 1 && Mathf.Sign(s.y) == 1) atan += 180;
            if (Mathf.Sign(s.x) == -1 && Mathf.Sign(s.y) == 1) atan += 360;
            yValue = Vector2.Distance((transform.position - s / 2f), Vector2.zero);
            if (BoxSprite==null)
            {
                BoxSprite = Instantiate(Resources.Load("Wbox") as GameObject,
                    (transform.position + next.transform.position) / 2f,
                    Quaternion.Euler(0, 0, (float)atan),
                    transform);
            }
            BoxSprite.transform.position = (transform.position + next.transform.position) / 2f;
            BoxSprite.transform.rotation = Quaternion.Euler(0, 0, (float) atan+90);
            BoxSprite.transform.localScale = new Vector3(0.25f,
                0.25f*Vector3.Distance(transform.position,next.transform.position),
                1);
            BoxSprite.GetComponent<SpriteRenderer>().color = new Color(0.5f,0.5f,0.5f,0.5f);
            BoxSprite.GetComponent<SpriteRenderer>().sortingOrder = -1;

        }
    }


    void Update()
    {
        if (Application.isEditor)
        {
            if (!next)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.cyan;
            }
        }
        
        Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position,0.5f);
        if (next)
        {
            Gizmos.DrawLine(transform.position,next.transform.position);
             s = -transform.position + next.transform.position;
             atan = Math.Atan(-s.y / -s.x) * 180 / Math.PI;
             if (Mathf.Sign(s.x) == 1 && Mathf.Sign(s.y) == -1) atan = 180 + atan;
             if (Mathf.Sign(s.x) == 1 && Mathf.Sign(s.y) == 1) atan += 180;
             if (Mathf.Sign(s.x) == -1 && Mathf.Sign(s.y) == 1) atan += 360;
             vectorsangle = Vector2.Angle(
                 (transform.position+next.transform.position)/2f,
                 Vector2.up);
             Gizmos.color = new Color(transform.position.x/10,transform.position.y/10,22);
             Gizmos.DrawWireSphere(Camera.main.ScreenToWorldPoint(Input.mousePosition),1);
             Gizmos.DrawWireCube(new Vector2(0,yValue), new Vector2(Vector3.Distance(transform.position,next.transform.position)*0.5f,0.5f));
             Gizmos.DrawWireSphere(rotated,2);
            Matrix4x4 rotationMatrix = Matrix4x4.TRS((transform.position+next.transform.position)/2f,
                Quaternion.Euler(0,0,(float)atan),
                transform.lossyScale);
            rotated = rotate(Camera.main.ScreenToWorldPoint(Input.mousePosition),(float)atan);
            Gizmos.matrix = rotationMatrix;
           Gizmos.DrawWireCube(Vector3.zero, new Vector2(Vector3.Distance(transform.position,next.transform.position)*0.5f,0.5f));

        }
         
    }
    
    public static Vector2 rotate(Vector2 v, float delta)
    {
        delta *= Mathf.Deg2Rad;
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }

    public bool CheckForMouse()
    {
        var tab = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach(Collider2D co in tab)
        {
            if (co.gameObject.CompareTag("MouseCheck"))
            {
                print("Cant place a tower there");
                return true;
            }
        }
        if (!next) return false;
        tab = Physics2D.OverlapBoxAll((transform.position + next.transform.position) / 2f,
            new Vector2(Vector3.Distance(transform.position, next.transform.position), 1f),
            (float) atan);
        if (tab.Length==0) return false;
        foreach(Collider2D co in tab)
        {
            if (co.gameObject.CompareTag("MouseCheck"))
            {
                print("Cant place a tower there");
                return true;
            }
        }

        return false;
    }


    public void OnDrag(PointerEventData eventData)
    {
        transform.position =Input.mousePosition;
    }

    public void OnMouseDown()
    {
        offset =  Input.mousePosition-transform.position;
    }

    public void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position += new Vector3(0, 0, 10);
    }
}
