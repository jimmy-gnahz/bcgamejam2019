using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody player;


    //for walking
    public float groundSpeed;

    //for gliding
    public float speed;
    public float rotateSpeed;
    public float accelration;
    private float currentSpeed;
    private bool onGround=true;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (!onGround)//gliding
        {
            
            //TODO
        }
        Vector3 velocity;
        //moving forward and backward
        velocity = new Vector3(0, 0, groundSpeed*v);       
        velocity = transform.TransformDirection(velocity);
        // turning
        transform.Rotate(0, h * rotateSpeed, 0);

        transform.localPosition += velocity * Time.fixedDeltaTime;
    }
}
