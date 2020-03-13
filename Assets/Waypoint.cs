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
         if(next)Gizmos.DrawLine(transform.position,next.transform.position);
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
