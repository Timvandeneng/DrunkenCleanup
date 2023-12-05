using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
    [Header("Player and camera Physics")]
    public float rotationspeed = 1;
    public float followSpeed;

    [Header("Necessary Attributes")]
    public Transform RotationAnchor;
    public Transform Target;
    public Transform CamAnchor;

    float mouseX;

    public ConfigurableJoint hipjoint;

    public float X, Y, Z;

    public bool LookAtToilet;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //TODO
        //FIX ROTATIONS
        //HAS TO DO WITH ORIENTATION I CHANGED LINE 35

        //Make sure we rotate using the mouse and our camera follows accordingly
        mouseX += Input.GetAxis("Mouse X") * rotationspeed;
        RotationAnchor.rotation = Quaternion.Euler(X, mouseX, Z);

        //also make sure our hip only rotates towards our anchor when moving to get a free cam effects
        float input = Input.GetAxis("Vertical");
        bool isgrabbingR = Input.GetKey(KeyCode.Mouse1);
        bool isgrabbingL = Input.GetKey(KeyCode.Mouse0);
        Quaternion desiredRot = RotationAnchor.rotation; 
        hipjoint.targetRotation = input != 0 || isgrabbingR || isgrabbingL ? desiredRot : hipjoint.targetRotation;
    }

    private void FixedUpdate()
    {
        //always make sure we look at our target
        transform.LookAt(Target);
        //make sure we stay behind our character
        transform.position = Vector3.Lerp(transform.position, CamAnchor.position, followSpeed * Time.deltaTime);
    }
}
