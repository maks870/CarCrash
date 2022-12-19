using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollowerPosition : MonoBehaviour
{
    [SerializeField] private Transform target;
    // [SerializeField] private Vector3 offsetPosition;
    [SerializeField] private float speedUpdate;
    [SerializeField] private bool play;
    private Vector3 initWorldPosition;
    private float factor;

    [Header("Following in world settings")]
    [SerializeField] private bool followWorldX = true;
    [SerializeField] private bool followWorldY = true;
    [SerializeField] private bool followWorldZ = true;

    [Header("Curve change position")]
    [SerializeField] private AnimationCurve curvePosition;

    public bool Play { get => play; set => play = value; }

    private void Awake()
    {
        initWorldPosition = transform.position;
    }

    public void SetTarget(Transform target)
    {
        StopFollow();
        this.target = target;
        StartFollow();
    }

    public void StartFollow()
    {
        StopFollow();
        play = true;
    }

    private void LateUpdate()
    {
        if (play && target != null)
            UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector3 newPosition;
        Vector3 intendedPosition = target.transform.position;

        if (!followWorldX)
            intendedPosition.x = initWorldPosition.x;

        if (!followWorldY)
            intendedPosition.y = initWorldPosition.y;

        if (!followWorldZ)
            intendedPosition.z = initWorldPosition.z;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance < 0.3)
        {
            StopFollow();
        }

        factor = Mathf.Lerp(0f, 1f, factor + Time.deltaTime * speedUpdate);

        newPosition = Vector3.Lerp(initWorldPosition, intendedPosition, curvePosition.Evaluate(factor));

        transform.position = newPosition;
    }

    private void StopFollow()
    {
        play = false;
        factor = 0;
        initWorldPosition = transform.position;
    }
}
