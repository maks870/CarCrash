using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class DialogueCameraChanger : MonoBehaviour
{
    Transform car;
    private bool isDialogueCamera = false;
    private GameObject PlayerCar;
    private SmoothFollow smoothFollow;
    public float rotX;
    public Transform camTransform;
    public Transform lookAtTransform;
    public Vector3 offset;
    public Camera cam;

    [Tooltip("If car speed is below this value, then the camera will default to looking forwards.")]
    public float rotationThreshold = 1f;

    [Tooltip("How closely the camera follows the car's position. The lower the value, the more the camera will lag behind.")]
    public float cameraStickiness = 10.0f;

    [Tooltip("How closely the camera matches the car's velocity vector. The lower the value, the smoother the camera rotations, but too much results in not being able to see where you're going.")]
    public float smoothSpeed = 5.0f;

    private void Start()
    {
        smoothFollow = GetComponent<SmoothFollow>();
        PlayerCar = GameObject.FindGameObjectWithTag("PlayerCar");
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (isDialogueCamera)
            SmoothFollowDialogue();
    }

    private void SmoothFollowDialogue()
    {
        Vector3 offsetPos = camTransform.position + offset;
        //Vector3 smoothFollow = Vector3.Lerp(transform.position, offsetPos, smoothSpeed * Time.deltaTime);


        Vector3 smoothFollow = Vector3.Lerp(transform.position, camTransform.position, smoothSpeed * Time.deltaTime);


        //float rotZ = cam.transform.eulerAngles.z;
        //cam.transform.Rotate(rotX, 0, -rotZ);




        transform.position = smoothFollow;
        //Quaternion look;

        //car = PlayerCar.transform;
        //look = Quaternion.LookRotation(car.forward);

        //look = Quaternion.Slerp(transform.rotation, look, smoothSpeed * Time.fixedDeltaTime);
        transform.LookAt(lookAtTransform);
        //transform.rotation = look;



    }

    public void SetDialogueTarget(Transform camTransform, Transform lookAtTransform)
    {
        this.camTransform = camTransform;
        this.lookAtTransform = lookAtTransform;
        smoothFollow.enabled = false;
        isDialogueCamera = true;
    }


    public void ResetDialogueTarget()
    {
        this.camTransform = null;
        this.lookAtTransform = null;
        smoothFollow.enabled = true;
        isDialogueCamera = false;
    }
}

