using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshUpdater : MonoBehaviour
{
    [SerializeField] NavMeshSurface surface;
    [SerializeField] int updateInterval = 60;

    private void LateUpdate()
    {
        if (Time.frameCount % updateInterval == 0)
        {
            surface.BuildNavMesh();
        }
    }
}
