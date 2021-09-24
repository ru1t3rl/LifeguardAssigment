using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickEvent : MonoBehaviour
{
    public float raycastDistance = 100f;
    public GameObject debugCube;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                if (hit.transform.tag == "Swimmer" && hit.transform.GetComponent<Swimmer>().state == SwimmerState.Drowning)
                    hit.transform.DOMoveY(4f, 1f).OnComplete(() => Destroy(hit.transform.gameObject));
                else if (hit.transform.tag == "Swimmer")
                    Debug.Log(hit.transform.GetComponent<Swimmer>().state);
            }
        }
    }
}