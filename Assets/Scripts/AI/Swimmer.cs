using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using DG.Tweening;

[RequireComponent(typeof(NavMeshAgent))]
public class Swimmer : MonoBehaviour
{
    public SwimmerState state { get; private set; }
    NavMeshAgent agent;

    float timeForNext = 0;

    [Header("Drown")]
    public UnityEvent OnDrown;

    [Header("Idle")]
    [SerializeField, Tooltip("X: Min time | Y: Max time")]
    Vector2 idleTime;
    public UnityEvent OnIdle;

    [Header("Movement Settings")]
    [SerializeField] float speed;
    [SerializeField, Range(0, 5)] float speedTweenTime = 1f;
    [SerializeField] LayerMask waterMask;
    [SerializeField] List<Transform> randomDirections;
    [SerializeField, Range(0, 10)] float minDistanceFromDestination;

    float swimmingDestPercentage = .05f;
    bool notAtDestination = false;

    void Awake()
    {
        state = SwimmerState.Idling;
        agent = GetComponent<NavMeshAgent>();

        SetDestination();
    }

    void Update()
    {
        if (InMui() && state != SwimmerState.Drowning)
        {
            Mui();
        }
        else if (!notAtDestination)
            StateManager();
        else if (DestInRange(swimmingDestPercentage))
            SetDestination();
    }

    void StateManager()
    {

        switch (state)
        {
            case SwimmerState.Idling:
                if (Time.time <= timeForNext)
                    return;

                if (Random.Range(0, 100) <= 90)
                    SetDestination();
                else
                    Drown();
                break;
            case SwimmerState.Swimming:
                if (!DestInRange() || notAtDestination)
                    return;

                int r = Random.Range(0, 100);
                if (r <= 5)
                    Idle();
                else if (r <= 95)
                {
                    if (DestInRange(swimmingDestPercentage))
                        SetDestination();
                    else
                    {
                        notAtDestination = true;
                    }
                }
                else
                    Drown();
                break;
        }
    }


    void Idle()
    {
        DOTween.To(() => agent.speed, x => agent.speed = x, 0, speedTweenTime);
        // Play Idle Animation in the OnComplete of DoTween
        timeForNext = Time.time + Random.Range(idleTime.x, idleTime.y);

        OnIdle?.Invoke();
        state = SwimmerState.Idling;
    }

    void Drown()
    {
        DOTween.To(() => agent.speed, x => agent.speed = x, 0, speedTweenTime);
        // Play Animation in OnComplete of the Tweener

        OnDrown?.Invoke();
        state = SwimmerState.Drowning;
    }

    void Mui()
    {
        DOTween.To(() => agent.speed, x => agent.speed = x, 0, speedTweenTime);
        // Move the player in the direction of the current/flow

        state = SwimmerState.Mui;
    }

    void SetDestination()
    {
        notAtDestination = false;

        DOTween.To(() => agent.speed, x => agent.speed = x, speed, speedTweenTime);

        agent.speed = speed;
        agent.SetDestination(RandomPosition().position);
        state = SwimmerState.Swimming;
    }

    Transform RandomPosition()
    {
        Transform t = randomDirections[Random.Range(0, randomDirections.Count - 1)];
        RaycastHit hit;
        if (Physics.Raycast(t.position, Vector3.down, out hit, 20))
        {
            if (hit.transform.gameObject.layer == waterMask.ToLayerIndex())
                return t;
            else
                return RandomPosition();
        }
        else
            return RandomPosition();
    }
    bool DestInRange(float perc = 1) => (agent.destination - transform.position).sqrMagnitude <= minDistanceFromDestination * minDistanceFromDestination * perc;

    bool InMui()
    {
        // TODO: if some check is true set state

        return false;
    }
}

public enum SwimmerState
{
    Swimming,
    Idling,
    Drowning,
    Mui
}
