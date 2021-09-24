using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RipTide : MonoBehaviour
{
    [Header("Rip Tide settings")]
    [SerializeField, Range(0, 100)] int enableChanges;
    [SerializeField] Terrain terrain;
    [SerializeField] Transform[] ripTideLocations;
    [SerializeField] Vector2 inActiveTimeRange, activeTimeRange;
    bool active = false, prevActive = false;
    float timeForAction;

    [SerializeField] float fadeDistance;
    [SerializeField] float fadeDuration;

    [Header("Swimmer Info")]
    [SerializeField] Transform end;
    [SerializeField] Vector2 endRandomRange;
    [SerializeField] float swimmerSpeedInMui;

    void Awake()
    {
        prevActive = active;
        active = Random.Range(0, 100) > enableChanges ? false : true;
        SetState(active);

        timeForAction = Time.time + Random.Range(inActiveTimeRange.x, inActiveTimeRange.y);
    }
    void SetState(bool active, bool usePrev = false)
    {
        if (!usePrev)
        {
            if (active)
            {
                terrain.enabled = true;
                ActivateRipTide();
                transform.position = new Vector3(transform.position.x,
                    transform.position.y - fadeDistance,
                    transform.position.z);

                transform.DOMoveY(transform.position.y + fadeDistance, fadeDuration);
            }
            else
            {
                transform.DOMoveY(transform.position.y - fadeDistance, fadeDuration).OnComplete(() =>
                {
                    terrain.enabled = false;
                    transform.position = new Vector3(transform.position.x,
                        transform.position.y + fadeDistance,
                        transform.position.z);
                });
            }
        }
        else
        {
            if (prevActive)
            {
                transform.DOMoveY(transform.position.y - fadeDistance, fadeDuration).OnComplete(() =>
                {
                    terrain.enabled = false;
                    ActivateRipTide();
                    transform.position = new Vector3(transform.position.x,
                        transform.position.y + fadeDistance,
                        transform.position.z);

                    SetState(active);
                });
            }
        }
    }

    void Update()
    {
        if (Time.time >= timeForAction)
        {
            prevActive = active;
            active = Random.Range(0, 100) > enableChanges ? false : true;
            SetState(active, true);

            timeForAction = Time.time + Random.Range(inActiveTimeRange.x, inActiveTimeRange.y);
        }
    }

    void ActivateRipTide()
    {
        transform.position = ripTideLocations[Random.Range(0, ripTideLocations.Length)].position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!active)
            return;

        Swimmer swimmer = other.gameObject.GetComponent<Swimmer>();
        if (swimmer != null)
        {
            swimmer.SetInMui(new Vector3(
                end.position.x + Random.Range(endRandomRange.x, endRandomRange.y),
                end.position.y + Random.Range(endRandomRange.x, endRandomRange.y),
                end.position.z + Random.Range(endRandomRange.x, endRandomRange.y)
                ),
                Random.Range(0.1f, swimmerSpeedInMui));
        }
    }
}
