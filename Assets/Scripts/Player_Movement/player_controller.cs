using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float strafeSpeed;
    [SerializeField]
    private float jumpForce;

    private Rigidbody hipsrb;
    public bool grounded = true;

    public Animator modelAnim;


    // Start is called before the first frame update
    void Start()
    {
        hipsrb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    { 
        //Movement forwards and backwards
        //Checking if input is on or off
        float DesiredZvel = Input.GetAxis("Vertical") * speed;
        float DesiredXvel = Input.GetAxis("Horizontal") * strafeSpeed;
        hipsrb.AddForce(-transform.right * DesiredZvel);
        hipsrb.AddForce(transform.forward * DesiredXvel);

        //animations
        //character wil try to go to animations
        modelAnim.SetBool("Walking", DesiredZvel != 0 ? true : false);

    }
}
