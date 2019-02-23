using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody player;
    private Quaternion initialRotation;

    public Animator playerAnimator;

    public int Energy;
    public int MaxEnergy;
    public float EnergyCostWalk; //units of energy per second
    public float EnergyCostGlide; //units of energy per second
    private float EnergyTimer;
    public int CrashDamage;

    //for walking
    public float groundSpeed;

    //for gliding
    public float speed;
    public float rotateSpeed;
    public float accelration;
    public float fallingSpeed;

    public bool isSuiciding = false;
    public bool isCrashing = false;

    public Transform robot;
    public bool onGround = true;

    public GameObject youFell;
    public GameObject LevelAudio;
    public GameObject fadeOut;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody>();
        initialRotation = player.rotation;
        Energy = MaxEnergy;
        isSuiciding = false;
        isCrashing = false;
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        if (isSuiciding) //jump backwards from a building
            HandleSuicide();
        else if (isCrashing) // crash onto a building during flight
            HandleCrash();
        else if (!onGround)// gliding
            HandleGliding(h, v);
        else // walking
            HandleWalking(h, v);
    }

    private void HandleWalking(float h, float v)
    {
        Vector3 velocity = new Vector3(0, 0, groundSpeed * v);
        if (v != 0) 
        {
            //only walking forward or backward will consume energy
            EnergyTimer += Time.fixedDeltaTime;
            if (EnergyTimer > 1 / EnergyCostWalk)
            {
                Energy--;
                EnergyTimer = 0;
            }
            if (Energy <= 0)
                StartCoroutine(YouFellOff());
        }

        velocity = transform.TransformDirection(velocity);
        transform.Rotate(0, h * rotateSpeed, 0);
        transform.localPosition += velocity * Time.fixedDeltaTime;

        // Animation
        if (velocity.Equals(Vector3.zero))
            playerAnimator.SetBool("isMoving", false);
        else
            playerAnimator.SetBool("isMoving", true);
    }

    private void HandleGliding(float h, float v)
    {
        Vector3 velocityPlanar = new Vector3(0, speed * v, 0); // flying planar velocity
        Vector3 velocityVertical = new Vector3(0, -fallingSpeed, 0); // falling velocity
    
        EnergyTimer += Time.fixedDeltaTime;
        if (EnergyTimer > 1f / EnergyCostGlide)
        {
            Energy--;
            EnergyTimer = 0;
        }

        transform.localPosition += velocityVertical * Time.fixedDeltaTime;
        transform.Rotate(0, 0, -h * rotateSpeed);

		playerAnimator.SetFloat("InputY", v);
		playerAnimator.SetFloat("InputX", h);
		Debug.Log("Set floats to input");
		if (v > 0)
        {
            velocityPlanar = transform.TransformDirection(velocityPlanar);
            transform.localPosition += velocityPlanar * Time.fixedDeltaTime;
        }
		else if (v > 0) {
			velocityPlanar = transform.TransformDirection(velocityPlanar) * 0.5f;
			transform.localPosition += velocityPlanar * Time.fixedDeltaTime;
		}

			int currentRotationY = (int)Mathf.Round(robot.localRotation.eulerAngles.y);
		if (h > 0) { }
			//robot.localRotation = Quaternion.Euler(0, -135, 0);
		else if (h < 0) { }
		//robot.localRotation = Quaternion.Euler(0, -45, 0);
		else { }
            //robot.localRotation = Quaternion.Euler(0, -90, 0);
    }

    private void HandleSuicide()
    {
        onGround = false;
        player.transform.Translate(new Vector3(0, fallingSpeed * 2 * Time.fixedDeltaTime, 0));
        player.transform.Rotate(new Vector3(0, 1.5f, 0));
    }

    private void HandleCrash()
    {
        player.transform.Translate(new Vector3(0, 0, fallingSpeed * 4 * Time.fixedDeltaTime));
        player.transform.Rotate(new Vector3(0, 0, 3.5f));
        //Todo
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!onGround)//flying or upon a crash
        {
            if (!isSuiciding)
            {
                if (collision.gameObject.tag.Equals("Building"))//crash on the building
                {
                    isCrashing = true;
                    Energy -= CrashDamage;
                    robot.localRotation = Quaternion.Euler(0, -90, 0);
                    player.transform.Translate(0, -1.8f, 0);
                    player.velocity = new Vector3(0, 0, 0);
                }
            }
        }

        if (collision.gameObject.tag.Equals("floor") && !onGround)//landing
        {
            if (isCrashing)
                isCrashing = false;

            if (isSuiciding)
            {
                Energy = 0;
                isSuiciding = false;
                StartCoroutine(YouFellOff());
            }

            EnergyTimer = 0;
            onGround = true;
            player.useGravity = true;
            player.transform.Rotate(-90, 0, 0);

            playerAnimator.SetBool("isFlying", false);
			robot.localRotation = Quaternion.Euler(0, -90, 0);

			PlayerGlidingPPChange pp = GetComponent<PlayerGlidingPPChange>();
			pp.SetIsFlying(false);
		}
    }

    IEnumerator YouFellOff()
    {
        youFell.SetActive(true);
		Time.timeScale = 0.3f;
        yield return new WaitForSecondsRealtime(2);
		LevelAudio.SetActive(false);
		fadeOut.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    private void OnCollisionExit(Collision collision)
    {
        // taking off from a roof
        if (collision.gameObject.tag.Equals("Roof"))
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                player.transform.Translate(0, 0, -1.8f);
                player.transform.Rotate(180, 0, 0);

                isSuiciding = true;

				PlayerGlidingPPChange pp = GetComponent<PlayerGlidingPPChange>();
				pp.SetIsFlying(true);
			}
            else
            {
                EnergyTimer = 0;
                player.useGravity = false;
                onGround = false;
                player.transform.Translate(0, 0, 0.8f);
                player.transform.Rotate(90, 0, 0);

                playerAnimator.SetBool("isFlying", true);

				PlayerGlidingPPChange pp = GetComponent<PlayerGlidingPPChange>();
				pp.SetIsFlying(true);
			}
        }
    }
}