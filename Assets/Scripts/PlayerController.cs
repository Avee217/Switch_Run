using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	private float moveSpeedStore;
	public float speedMultiplier;

	public float speedIncreaseMilestone;
	private float speedIncreaseMilestoneStore;

	private float speedMilestoneCount;
	private float speedMilestoneCountStore;


	public float jumpForce;
	public float jumpTime;
	private float jumpTimeCounter;
	private bool stoppedJumping;

	private bool canDoubleJump;

	private Rigidbody2D myRigidbody;

	public bool grounded;
	public LayerMask whatIsGround;
	public Transform groundCheck;
	public float groundCheckRadius;

	public AudioSource jumpSpund;
	public AudioSource deathSound;

	public bool IsGreen;
	//private Collider2D myCollider;

	private Animator myAnimator;

	public GameManager theGameManager;
	public PlatformGenetator thePlatformGenetator;
	public PauseMenu thePauseMenu;

	void Start () 
	{
		myRigidbody = GetComponent<Rigidbody2D> ();	
		//myCollider = GetComponent<Collider2D> ();
		myAnimator = GetComponent<Animator> ();
		thePlatformGenetator = FindObjectOfType<PlatformGenetator> ();
		jumpTimeCounter = jumpTime;
		speedMilestoneCount = speedIncreaseMilestone;
		moveSpeedStore = moveSpeed;
		speedMilestoneCountStore = speedMilestoneCount;
		speedIncreaseMilestoneStore = speedIncreaseMilestone;
		IsGreen = true;
		stoppedJumping = true;
	}
	

	void Update () {

		//grounded = Physics2D.IsTouchingLayers (myCollider, whatIsGround);

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);

		if (transform.position.x > speedMilestoneCount) 
		{
			speedMilestoneCount += speedIncreaseMilestone;

			speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;
			moveSpeed = moveSpeed * speedMultiplier; 
			if (thePlatformGenetator.distanceBetweenMax < 5) 
			{
				thePlatformGenetator.distanceBetweenMax += thePlatformGenetator.distanceBetweenMax * 0.02f;
			}

		}


		myRigidbody.velocity = new Vector2 (moveSpeed, myRigidbody.velocity.y);


		if (Input.GetKeyDown (KeyCode.Space)) 
		{

			if (grounded) 
			{
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpForce);
				stoppedJumping = false;	
				jumpSpund.Play ();
			}

			if (!grounded && canDoubleJump)
			{
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpForce);
				jumpTimeCounter = jumpTime;
				stoppedJumping = false;
				canDoubleJump = false;
				jumpSpund.Play ();
			} 
		
		}

		if (Input.GetKey (KeyCode.Space) && !stoppedJumping) 
		{
			if (jumpTimeCounter > 0) 
			{
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpForce);
				jumpTimeCounter -= Time.deltaTime;
			}
		}


		if (Input.GetKeyUp (KeyCode.Space) ) 
		{
			jumpTimeCounter = 0;
			stoppedJumping = true;
		}

		if (grounded) 
		{
			jumpTimeCounter = jumpTime;
			canDoubleJump = true;
		}
	

		if( Input.GetKeyUp (KeyCode.P))
		{ 
			thePauseMenu.PauseGame ();
		}

		if (Input.GetKeyDown (KeyCode.X))
		{
			if (IsGreen) {
				IsGreen = false;
				myAnimator.SetBool ("IsGreen", IsGreen);

			}

			else if (!IsGreen) {
				IsGreen = true;
				myAnimator.SetBool ("IsGreen", IsGreen);

			}
		}
		myAnimator.SetFloat ("Speed", myRigidbody.velocity.x);
		myAnimator.SetBool ("Grounded", grounded);
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if  (other.gameObject.tag == "Killbox") 
		{
			deathSound.Play ();
			Time.timeScale = 0f;
			theGameManager.RestartGame ();
			moveSpeed = moveSpeedStore;
			speedMilestoneCount = speedMilestoneCountStore;
			speedIncreaseMilestone = speedIncreaseMilestoneStore;
			IsGreen = true;
		} 

		else if (other.gameObject.tag == "Ground") 
		{
			if (!IsGreen) {
				deathSound.Play ();
				Time.timeScale = 0f;
				theGameManager.RestartGame ();
				moveSpeed = moveSpeedStore;
				speedMilestoneCount = speedMilestoneCountStore;
				speedIncreaseMilestone = speedIncreaseMilestoneStore;
				IsGreen = true;
			}
		} 

		else if (other.gameObject.tag == "Ground1")
		{
			if (IsGreen)
			{
				deathSound.Play ();
				Time.timeScale = 0f;
				theGameManager.RestartGame ();
				moveSpeed = moveSpeedStore;
				speedMilestoneCount = speedMilestoneCountStore;
				speedIncreaseMilestone = speedIncreaseMilestoneStore;
				IsGreen = true;
			}
		}
	}

}
