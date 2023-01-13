using UnityEngine;

public class CarCamera : MonoBehaviour
{
    Transform car;
    private GameObject PlayerCar;
    public Vector3 offset;
    public float rotX;
    public Camera cam;

    [Tooltip("If car speed is below this value, then the camera will default to looking forwards.")]
    public float rotationThreshold = 1f;

    [Tooltip("How closely the camera follows the car's position. The lower the value, the more the camera will lag behind.")]
    public float cameraStickiness = 10.0f;

    [Tooltip("How closely the camera matches the car's velocity vector. The lower the value, the smoother the camera rotations, but too much results in not being able to see where you're going.")]
    public float smoothSpeed = 5.0f;

    private void Start()
    {
        PlayerCar = GameObject.FindGameObjectWithTag("PlayerCar");
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        SmoothFollow();
    }

    private void SmoothFollow() 
    {
        Vector3 offsetPos  = PlayerCar.transform.position + offset;
        Vector3 smoothFollow = Vector3.Lerp(transform.position, offsetPos, smoothSpeed * Time.deltaTime);
        //float rotZ = cam.transform.eulerAngles.z;
        //cam.transform.Rotate(rotX, 0, -rotZ);


       

        transform.position = smoothFollow;
        //Quaternion look;

        //car = PlayerCar.transform;
        //look = Quaternion.LookRotation(car.forward);

        //look = Quaternion.Slerp(transform.rotation, look, smoothSpeed * Time.fixedDeltaTime);
        transform.LookAt(PlayerCar.transform);
        //transform.rotation = look;
    }
}