using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looker : MonoBehaviour
{
    public Transform target;
    public bool xLock = false;
    public bool yLock = false;
    public bool zLock = false;

    private Quaternion lockedRotation;
    private void Start()
    {
        lockedRotation = transform.rotation;
    }

    void Update()
    {
        transform.LookAt(target);

        float xRot = xLock ? lockedRotation.x : transform.rotation.x;
        float yRot = yLock ? lockedRotation.y : transform.rotation.y;
        float zRot = zLock ? lockedRotation.z : transform.rotation.z;

        transform.rotation.Set(xRot, yRot, zRot, 1);
    }
}
