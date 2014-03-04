using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour
{

	// Player Handing
	public float gravity = 20;
	public float walkSpeed = 8 ;
	public float runSpeed = 20 ;

	public float acceleration = 12;
	public float slideAcceleration = 10;
	public float jumpHeight = 10;
	//
	private float currentSpeed;
	private float targetSpeed;
	private float animationSpeed;
	private Vector2 amountToMove;
	//
	private PlayerPhysics playerPhysics;
	private Animator animator;
	private bool jumping;
	private bool sliding;
	// New Commits
	void Start ()
	{
		playerPhysics = GetComponent<PlayerPhysics> ();
		animator = GetComponent<Animator>();
	}
 
	void Update ()
	{
	
		// Reset Speed
		if (playerPhysics.movementStopped ){
			targetSpeed= 0;
			currentSpeed= 0;
		}

		// If player on grounded
		if (playerPhysics.grounded){
			amountToMove.y = 0;
			// Jump
			if (jumping){
				jumping = false;
				animator.SetBool("Jumping",false);
			} 
			// Sliding
			if (sliding){
				if ((Mathf.Abs(currentSpeed) < .25f)){
					sliding = false;
					animator.SetBool("Sliding",false);
					playerPhysics.ResetCollider();
				} 
			}
			if (Input.GetButtonDown("Jump")){
				amountToMove.y = jumpHeight;
				jumping = true;
				animator.SetBool("Jumping",true);
			}
		
			if (Input.GetButtonDown("Slide")){ 
				sliding = true;
				animator.SetBool("Sliding",true);
				targetSpeed = 0;
				playerPhysics.SetCollider(new Vector3(9.37f,1.62f,3.39f), new Vector3(0.49f,1.09f,0));
			}

		} 
		// Animation Speed
		animationSpeed = GameManager.IncrementTowards(animationSpeed,Mathf.Abs(currentSpeed),acceleration);
		animator.SetFloat("Speed",animationSpeed);

		// Input
		if (!sliding){
			float speed = Input.GetButton("Run") ? runSpeed : walkSpeed;
			targetSpeed = Input.GetAxisRaw ("Horizontal") * speed;
			currentSpeed = GameManager.IncrementTowards(currentSpeed, targetSpeed, acceleration);
			//		MyLog.LogI ("Input.GetAxisRaw (Horizontal) " + Input.GetAxisRaw ("Horizontal"));
			//		MyLog.LogI ("targetSpeed " + targetSpeed + " / "+ "currentSpeed " + currentSpeed  );

			// Face Directions
			float moveDir = Input.GetAxisRaw("Horizontal");
			if (moveDir != 0){
				transform.eulerAngles = (moveDir > 0 )? Vector3.up * 180 : Vector3.zero;
			}

		}else{
			currentSpeed = GameManager.IncrementTowards(currentSpeed, targetSpeed, slideAcceleration);

		}  
		amountToMove.x = currentSpeed;
		amountToMove.y -= gravity * Time.deltaTime;
		playerPhysics.Move (amountToMove * Time.deltaTime);

	

		
	}


	
	
	 
}
