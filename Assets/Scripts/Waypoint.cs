using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour,IDragHandler
{
    private Vector2 offset;
    public Waypoint next = null;
    public  double atan;
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
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position,0.5f);
        if (next)
        {
            Gizmos.DrawLine(transform.position,next.transform.position);
            var s = -transform.position + next.transform.position;
             atan = Math.Atan(s.y / s.x)*180/Math.PI;
            Matrix4x4 rotationMatrix = Matrix4x4.TRS((transform.position+next.transform.position)/2f,
                Quaternion.Euler(0,0,(float)atan),
                transform.lossyScale);
            Gizmos.matrix = rotationMatrix;
            Gizmos.DrawWireCube(Vector3.zero, new Vector2(Vector3.Distance(transform.position,next.transform.position)*0.5f,0.5f));
        }
         
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
