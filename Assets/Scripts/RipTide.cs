using System.Collections.Generic;
using UnityEngine;
using Lifeguard.AI;

public class RipTide : MonoBehaviour
{
    [Header("Rip Tide settings")]
    [SerializeField, Range(0, 100)] int enableChanges;
    [SerializeField] Terrain terrain;
    [SerializeField] Transform[] ripTideLocations;
    [SerializeField] Vector2 inActiveTimeRange, activeTimeRange;
    bool active = false;
    float timeForAction;

    [Header("Swimmer Info")]
    [SerializeField] Transform end;
    [SerializeField] Vector2 endRandomRange;
    [SerializeField] float swimmerSpeedInMui;

    void Awake()
    {
        active = Random.Range(0, 100) > enableChanges ? false : true;
        terrain.enabled = active;

        if (active)
        {
            ActivateRipTide();
        }
        else
            timeForAction = Time.time + Random.Range(inActiveTimeRange.x, inActiveTimeRange.y);
    }

    void Update()
    {
        if (Time.time >= timeForAction)
        {
            active = Random.Range(0, 100) > enableChanges ? false : true;
            terrain.enabled = active;

            if (active)
            {
                ActivateRipTide();
            }
            else
                timeForAction = Time.time + Random.Range(inActiveTimeRange.x, inActiveTimeRange.y);
        }
    }

    void ActivateRipTide()
    {
        transform.position = ripTideLocations[Random.Range(0, ripTideLocations.Length)].position;

        timeForAction = Time.time + Random.Range(activeTimeRange.x, activeTimeRange.y);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!active)
            return;

        Swimmer swimmer = other.gameObject.GetComponent<Swimmer>();
        if (swimmer != null)
        {
            Debug.Log("<b>[Riptide]</b> I ate a simmer");
            swimmer.SetInMui(new Vector3(
                end.position.x + Random.Range(endRandomRange.x, endRandomRange.y),
                end.position.y + Random.Range(endRandomRange.x, endRandomRange.y),
                end.position.z + Random.Range(endRandomRange.x, endRandomRange.y)
                ),
                Random.Range(0.1f, swimmerSpeedInMui));
        }
    }
}
