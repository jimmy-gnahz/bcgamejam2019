﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody player;
    private Quaternion initialRotation;

    public Animator playerAnimator;

    public  int Energy;
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

    public bool isSuiside = false;
    public bool isCrushed = false;

    public Transform robot;
    public bool onGround = true;

    public GameObject youFell;
    //public GameObject LevelAudio;
    public GameObject fadeOut;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody>();
        initialRotation = player.rotation;
        Energy = MaxEnergy;
        isSuiside = false;
        isCrushed = false;
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 velocity; // on ground
        Vector3 velocity2d; // flying planar velocity
        Vector3 velocityVertical; // falling velocity
        if (isSuiside) //leap of faith
        {
            onGround = false;
            player.transform.Translate(new Vector3(0, fallingSpeed * 2 * Time.fixedDeltaTime, 0));
            player.transform.Rotate(new Vector3(0, 1.5f, 0));
        }
        else if (isCrushed)
        {
            crash();
        }
        else
        {
            if (!onGround)//gliding
            {
                EnergyTimer += Time.fixedDeltaTime;// moving or not, gliding consumes energy
                if (EnergyTimer > 1f / EnergyCostGlide)//glide long enough to lose energy
                {
                    Energy--;
                    EnergyTimer = 0;
                }

                velocityVertical = new Vector3(0, -fallingSpeed, 0);
                transform.localPosition += velocityVertical * Time.fixedDeltaTime;
                velocity2d = new Vector3(0, speed * v, 0);
                transform.Rotate(0, 0, -h * rotateSpeed);

                if (v > 0)
                {
                    velocity2d = transform.TransformDirection(velocity2d);
                    transform.localPosition += velocity2d * Time.fixedDeltaTime;
                }

                int currentRotationY = (int) Mathf.Round(robot.localRotation.eulerAngles.y);
                if (h > 0)
                {
					robot.localRotation = Quaternion.Euler(0, -135, 0);
                }
                else if (h < 0)
                {
					robot.localRotation = Quaternion.Euler(0, -45, 0);
                }
                else
                {
                    robot.localRotation = Quaternion.Euler(0, -90, 0);
                }

            }

            else
            {
                //moving forward and backward
                velocity = new Vector3(0, 0, groundSpeed * v);
                if (v != 0) //only walking forward or backward will consume energy
                    EnergyTimer += Time.fixedDeltaTime;
                {
                    if (EnergyTimer > 1 / EnergyCostWalk)//walk long enough to lose energy
                    {
                        Energy--;
                        EnergyTimer = 0;
                    }
                }
                if (v < 0)
                {
                    Debug.Log("ReadEnergy");
                    Debug.Log(Energy);
                }

                velocity = transform.TransformDirection(velocity);
                // turning
                transform.Rotate(0, h * rotateSpeed, 0);

                transform.localPosition += velocity * Time.fixedDeltaTime;

                if (velocity.Equals(Vector3.zero))
                    playerAnimator.SetBool("isMoving", false);
                else
                    playerAnimator.SetBool("isMoving", true);
            }

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        Debug.Log(collision.gameObject.tag.Equals("floor"));
        if (!onGround)//flying, and dying
        {
            if (!isSuiside)//flying for sure
            {
                if (collision.gameObject.tag.Equals("Building"))//crash on the building
                {
                    
                    isCrushed = true;
                    Energy -= CrashDamage;
                    robot.localRotation = Quaternion.Euler(0, -90, 0);
                    player.transform.Translate(0, -1.8f, 0);
                    player.velocity = new Vector3(0, 0, 0);
                }
            }
        }

        if (collision.gameObject.tag.Equals("floor") && !onGround)//landing
        {
            if (isCrushed)
            {
                isCrushed = false;
            }
            if (isSuiside)
            {
                Energy = 0;
                isSuiside = false;
                StartCoroutine(YouFellOff());
            }
            EnergyTimer = 0;//grace reset on landing and taking off
            Debug.Log("oncollision ");
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
        yield return new WaitForSeconds(2);
        //LevelAudio.SetActive(false);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Roof"))// taking off
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                player.transform.Translate(0, 0, -1.8f);
                player.transform.Rotate(180, 0, 0);

                isSuiside = true;
                playerAnimator.SetBool("isFlying", true);

				PlayerGlidingPPChange pp = GetComponent<PlayerGlidingPPChange>();
				pp.SetIsFlying(true);
			}
            else
            {
                EnergyTimer = 0;//grace reset on landing and taking off
                player.useGravity = false;
                onGround = false;
                player.transform.Translate(0, 0, 0.8f);
                player.transform.Rotate(90, 0, 0);
                // player.constraints

                playerAnimator.SetBool("isFlying", true);

				PlayerGlidingPPChange pp = GetComponent<PlayerGlidingPPChange>();
				pp.SetIsFlying(true);
			}


        }

    }

    private void crash()
    {
        player.transform.Translate(new Vector3(0,0, fallingSpeed * 4 * Time.fixedDeltaTime));
        player.transform.Rotate(new Vector3(0,0 ,3.5f));
        //Todo
    }
}