using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AreaMovement : MonoBehaviour
{
    public enum Area : int
    {
        Area1,
        Area2,
        Area3
    }

    public Transform areas;
    public float movementSpeed;
    [HideInInspector]
    public Tween moveTween;

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
        if ((Input.GetKeyDown("left") || Input.GetKeyDown("a")) && currentArea != Area.Area1)
        {
            // Move left if you can
            if (currentArea == Area.Area2)
            {
                moveTween.Kill();
                currentArea = Area.Area1;
                MoveToArea(Area.Area1);
            }
            else
            {
                moveTween.Kill();
                currentArea = Area.Area2;
                MoveToArea(Area.Area2);
            }
        }
        if ((Input.GetKeyDown("right") || Input.GetKeyDown("d")) && currentArea != Area.Area3)
        {
            // Move right if you can
            if (currentArea == Area.Area2)
            {
                moveTween.Kill();
                currentArea = Area.Area3;
                MoveToArea(Area.Area3);
            }
            else
            {
                moveTween.Kill();
                currentArea = Area.Area2;
                MoveToArea(Area.Area2);
            }
        }
    }

    public void MoveToArea(Area areaToMoveTo)
    {
        moveTween = transform.DOMoveX(areas.GetChild((int)areaToMoveTo).position.x, movementSpeed);
        currentArea = areaToMoveTo;
    }
}