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
    public bool InTrigger;
    public float yValue;
    public Vector3 s = new Vector3();
    void Start()
    {
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
            Matrix4x4 rotationMatrix = Matrix4x4.TRS((transform.position+next.transform.position)/2f,
                Quaternion.Euler(0,0,(float)atan),
                transform.lossyScale);
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
