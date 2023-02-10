using UnityEngine;

public class Looker : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        if (target != null)
            transform.LookAt(new Vector3(target.position.x, 0, target.position.z));



        //transform.rotation.Set(xRot, yRot, zRot, 1);
    }
}
