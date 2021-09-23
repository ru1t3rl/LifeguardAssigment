using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AreaMovement : MonoBehaviour
{
    private enum Area : int
    {
        Area1,
        Area2,
        Area3
    }

    public Transform areas;
    public float movementSpeed;
    private bool moving = false;

    [SerializeField]
    private Area currentArea;

    // Start is called before the first frame update
    void Start()
    {
        currentArea = Area.Area2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving && (Input.GetKeyDown("left") || Input.GetKeyDown("a")) && currentArea != Area.Area1)
        {
            // Move left if you can
            if (currentArea == Area.Area2)
            {
                moving = true;
                currentArea = Area.Area1;
                transform.DOMoveX(areas.GetChild(0).position.x, movementSpeed).OnComplete(ResumeMovement);
            }
            else
            {
                moving = true;
                currentArea = Area.Area2;
                transform.DOMoveX(areas.GetChild(1).position.x, movementSpeed).OnComplete(ResumeMovement); ;
            }
        }
        if (!moving && (Input.GetKeyDown("right") || Input.GetKeyDown("d")) && currentArea != Area.Area3)
        {
            // Move right if you can
            if (currentArea == Area.Area2)
            {
                moving = true;
                currentArea = Area.Area3;
                transform.DOMoveX(areas.GetChild(2).position.x, movementSpeed).OnComplete(ResumeMovement); ;
            }
            else
            {
                moving = true;
                currentArea = Area.Area2;
                transform.DOMoveX(areas.GetChild(1).position.x, movementSpeed).OnComplete(ResumeMovement); ;
            }
        }
    }

    private void ResumeMovement()
    {
        moving = false;
    }
}