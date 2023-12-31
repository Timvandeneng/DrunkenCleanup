using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    [Header("Player Physics")]
    [SerializeField]
    private float speed;
    [SerializeField]
    private float strafeSpeed;
    [SerializeField]
    private float Maxspeed;
    [SerializeField]
    private float jumpForce;

    private Rigidbody hipsrb;
    public bool grounded = true;

    public Animator modelAnim;

    public Transform ForcePosition;

    public ParticleSystem Clouds;
    public float emitSpeed;
    public float activationspeed;
    float resetEmit;

    // Start is called before the first frame update
    void Start()
    {
        hipsrb = GetComponent<Rigidbody>();
        resetEmit = emitSpeed;
    }

    private void FixedUpdate()
    { 
        //Movement forwards and backwards
        //Checking if input is on or off
        //Also use our forceposition to accurately go forward
        float DesiredZvel = Input.GetAxis("Vertical") * speed;
        float DesiredXvel = Input.GetAxis("Horizontal") * strafeSpeed;
        hipsrb.AddForce(-ForcePosition.right * DesiredZvel);
        hipsrb.AddForce(ForcePosition.up * DesiredXvel);

        //making sure our player stands still when no keys are pressed down(no sliding)
        Vector3 restVelocity = new Vector3(0, hipsrb.velocity.y * 1f, 0);
        hipsrb.velocity = DesiredZvel == 0 && DesiredXvel == 0 ? restVelocity : hipsrb.velocity;


        //clamping our Velocity to a max speed factor
        if (hipsrb.velocity.magnitude > Maxspeed)
            hipsrb.velocity = Vector3.ClampMagnitude(hipsrb.velocity, Maxspeed);

        //animations
        //character wil try to go to animations
        modelAnim.SetBool("Walking", DesiredZvel != 0 ? true : false);
        if(transform.InverseTransformDirection(hipsrb.velocity).x > activationspeed)
        {
            emitSpeed -= Time.deltaTime;
            if(emitSpeed < 0)
            {
                Clouds.Emit(1);
                emitSpeed = resetEmit;
            }
        }
        else
        {
            emitSpeed = resetEmit;
        }
    }
}
