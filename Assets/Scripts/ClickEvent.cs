using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEvent : MonoBehaviour
{
    public float raycastDistance = 100f;
    public GameObject debugCube;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                Debug.DrawLine(ray.origin, hit.point);
                Instantiate(debugCube, hit.point, Quaternion.identity);
            }
        }
    }
}