using UnityEngine;

public class Looker : MonoBehaviour
{
    public Transform target;
    public bool xLock = false;
    public bool yLock = false;
    public bool zLock = false;

    private Vector3 targetVector;

    void Update()
    {
        targetVector = target.position;

        targetVector.x = xLock ? 0 : targetVector.x;
        targetVector.y = yLock ? 0 : targetVector.y;
        targetVector.z = zLock ? 0 : targetVector.z;
       
        transform.LookAt(targetVector);

        

        //transform.rotation.Set(xRot, yRot, zRot, 1);
    }
}
