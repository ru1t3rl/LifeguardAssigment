using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TideSystem : MonoBehaviour
{
    [SerializeField]
    private float waterHeightDifference = 0.6f, ebbAndFlowTimer = 10f;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveY(-waterHeightDifference, ebbAndFlowTimer).OnComplete(() => EbbAndFlow(false));
    }

    void EbbAndFlow(bool ebb)
    {
        if (ebb)
            transform.DOMoveY(-waterHeightDifference, ebbAndFlowTimer).OnComplete(() => EbbAndFlow(false));
        else
            transform.DOMoveY(0f , ebbAndFlowTimer).OnComplete(() => EbbAndFlow(true));
    }
}