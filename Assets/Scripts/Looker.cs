using UnityEngine;

public class Looker : MonoBehaviour
{
    public Transform target;

    private Vector3 targetVector;

    void Update()
    {
        targetVector = target.position;

        targetVector.x = 0;
        targetVector.y = 1;
        targetVector.z = 0;
       
        transform.LookAt(targetVector);

        

        //transform.rotation.Set(xRot, yRot, zRot, 1);
    }
}
