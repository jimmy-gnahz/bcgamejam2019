using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAnimationTestScript : MonoBehaviour
{
	Animator anim;

	public bool isFlying = false;
	public bool isMoving = false;
    void Start()
    {
		anim = GetComponent<Animator>();
    }
	
    void Update()
    {
		anim.SetBool("isFlying", isFlying);
		anim.SetBool("isMoving", isMoving);
    }
}
