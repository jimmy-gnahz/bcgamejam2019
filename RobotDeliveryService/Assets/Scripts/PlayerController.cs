using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody player;
    private Quaternion initialRotation;

    //for walking
    public float groundSpeed;

    //for gliding
    public float speed;
    public float rotateSpeed;
    public float accelration;
    public float fallingSpeed;


    public bool onGround=true;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody>();
        initialRotation = player.rotation;
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 velocity; // on ground
        Vector3 velocity2d; // flying planar velocity
        Vector3 velocityVertical; // falling velocity
        if (!onGround)//gliding
        {
            velocityVertical = new Vector3(0, -fallingSpeed, 0);
            transform.localPosition += velocityVertical * Time.fixedDeltaTime;
            velocity2d = new Vector3(0,  speed * v,0);
            transform.Rotate( 0,0,-h * rotateSpeed);

            if (v > 0)
            {
                velocity2d = transform.TransformDirection(velocity2d);
                transform.localPosition += velocity2d * Time.fixedDeltaTime;
            }

            if (h > 0)
            {
                Debug.Log(transform.eulerAngles.y);
                transform.Rotate(0, 1, 0);
                if (transform.eulerAngles.y > 300)
                {
                    Debug.Log("in here");
                    transform.Rotate(0, 0, 1);
                }
                
            }

        }

        else{
            //moving forward and backward
            velocity = new Vector3(0, 0, groundSpeed * v);
            velocity = transform.TransformDirection(velocity);
            // turning
            transform.Rotate(0, h * rotateSpeed, 0);

            transform.localPosition += velocity * Time.fixedDeltaTime;
        }
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("floor")&&!onGround)//landing
        {
            onGround = true;
            player.useGravity = true ;
            player.transform.Rotate(-90, 0, 0);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Roof"))// taking off
        {
            
            player.useGravity = false;
            onGround = false;
            player.transform.Translate(0, 0, 0.8f);
            player.transform.Rotate(90, 0, 0);
           // player.constraints
        }
               
    }
}
