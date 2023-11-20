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

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Make sure we rotate using the mouse and our camera follows accordingly
        mouseX += Input.GetAxis("Mouse X") * rotationspeed;
        RotationAnchor.rotation = Quaternion.Euler(0, mouseX, 0);

        //also make sure our hip only rotates towards our anchor when moving to get a free cam effects
        float input = Input.GetAxis("Vertical");
        bool isgrabbingR = Input.GetKey(KeyCode.Mouse1);
        bool isgrabbingL = Input.GetKey(KeyCode.Mouse0);
        hipjoint.targetRotation = input != 0 || isgrabbingR || isgrabbingL ? Quaternion.Inverse(RotationAnchor.rotation) : hipjoint.targetRotation;
    }

    private void FixedUpdate()
    {
        //always make sure we look at our target
        transform.LookAt(Target);
        //make sure we stay behind our character
        transform.position = Vector3.Lerp(transform.position, CamAnchor.position, followSpeed * Time.deltaTime);
    }
}
