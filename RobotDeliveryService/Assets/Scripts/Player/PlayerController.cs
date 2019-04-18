using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	private InteractionOperator interactionOperator;

	private Rigidbody rb;
    private Quaternion initialRotation;

	public Quest CurrentQuest { get; set; }

	[SerializeField] private int energy;
    [SerializeField] private int MaxEnergy;
	private float ratioMaxEnergy;

	public float EnergyCostWalk; //units of energy per second
    public float EnergyCostGlide; //units of energy per second
    private float EnergyTimer;
    public int CrashDamage;

    //for walking
	[Header("Walking Variables")]
    public float groundSpeed;

	//for gliding
	[Header("Gliding Variables")]
	public float speedMin;
	public float speedMax;
	public float rotateSpeed;
    public float accelration;
    public float fallingSpeed;

    private bool isSuiciding = false;
    private bool isCrashing = false;
    public float takeOffCd = 0.5f; // do we need this?
    public float currentTakeOffCd = 0; // and this?


    public Animator playerAnimator;
	public Transform robot;
    public bool onGround = true;

    public GameObject youFell;
    public GameObject fadeOut;
    public GameObject LevelAudio;

    // Start is called before the first frame update
    void Awake()
    {
		interactionOperator = GetComponent<InteractionOperator>();

		rb = GetComponent<Rigidbody>();
        initialRotation = rb.rotation;
        energy = MaxEnergy;
        isSuiciding = false;
        isCrashing = false;

		ratioMaxEnergy = (float) 1 / MaxEnergy;
	}

	private void Update() {
		if (Input.GetButtonDown("Accept")) interactionOperator.Interact();

		UIElements.inst.UpdateHealthUI(energy, ratioMaxEnergy);
	}
	private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        HandleCd();
        
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
                energy--;
                EnergyTimer = 0;
            }
            if (energy <= 0)
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
		float velY = Mathf.Lerp(speedMin, speedMax, (v*.5f) + .5f);
        Vector3 velocityPlanar = new Vector3(0, velY, 0); // flying planar velocity
        Vector3 velocityVertical = new Vector3(0, -fallingSpeed, 0); // falling velocity
    
        EnergyTimer += Time.fixedDeltaTime;
        if (EnergyTimer > 1f / EnergyCostGlide)
        {
            energy--;
            EnergyTimer = 0;
        }

        transform.localPosition += velocityVertical * Time.fixedDeltaTime;
        transform.Rotate(0, 0, -h * rotateSpeed);

		playerAnimator.SetFloat("InputY", v);
		playerAnimator.SetFloat("InputX", h);
		
        velocityPlanar = transform.TransformDirection(velocityPlanar);
        transform.localPosition += velocityPlanar * Time.fixedDeltaTime;
        

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
        rb.transform.Translate(new Vector3(0, fallingSpeed * 2 * Time.fixedDeltaTime, 0));
        rb.transform.Rotate(new Vector3(0, 1.5f, 0));
    }
    private void HandleCrash()
    {
        rb.transform.Translate(new Vector3(0, 0, fallingSpeed * 4 * Time.fixedDeltaTime));
        rb.transform.Rotate(new Vector3(0, 0, 3.5f));
        //Todo
    }
    private void HandleCd()
    {
        currentTakeOffCd -= Time.fixedDeltaTime;
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
                    energy -= CrashDamage;
                    robot.localRotation = Quaternion.Euler(0, -90, 0);
                    rb.transform.Translate(0, -1.8f, 0);
                    rb.velocity = new Vector3(0, 0, 0);
                }
            }
        }

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("floor")) && !onGround) //landing
        {
            if (isCrashing)
                isCrashing = false;

            if (isSuiciding)
            {
                energy = 0;
                isSuiciding = false;
                StartCoroutine(YouFellOff());
            }

            EnergyTimer = 0;
            currentTakeOffCd = takeOffCd;
            onGround = true;
            rb.useGravity = true;
            rb.transform.Rotate(-90, 0, 0);

            playerAnimator.SetBool("isFlying", false);
			robot.localRotation = Quaternion.Euler(0, -90, 0);
		}
    }
    private void OnCollisionExit(Collision collision)
    {
        // taking off from a roof
        if (collision.gameObject.tag.Equals("Roof") && currentTakeOffCd < 0)
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                rb.transform.Translate(0, 0, -1.8f);
                rb.transform.Rotate(180, 0, 0);

                isSuiciding = true;
			}
            else
            {
                EnergyTimer = 0;
                rb.useGravity = false;
                onGround = false;
                rb.transform.Translate(0, 0, 0.8f);
                rb.transform.Rotate(90, 0, 0);

                playerAnimator.SetBool("isFlying", true);
			}
        }
    }

	public void RewardQuest(Quest q) {
		energy += q.Energy;
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


}