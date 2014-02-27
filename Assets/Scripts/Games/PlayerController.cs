using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour
{

	// Player Handing
	public float gravity = 20;
	public float speed = 8 ;
	public float acceleration = 12;
	public float jumpHeight = 10;
	//
	private float currentSpeed;
	private float targetSpeed;
	private Vector2 amountToMove;
	private PlayerPhysics playerPhysics;
	// New Commits
	void Start ()
	{
		playerPhysics = GetComponent<PlayerPhysics> ();
	}
 
	void Update ()
	{
		targetSpeed = Input.GetAxisRaw ("Horizontal") * speed;
//		MyLog.LogI ("Input.GetAxisRaw (Horizontal) " + Input.GetAxisRaw ("Horizontal"));
		currentSpeed = IncrementTowards (currentSpeed, targetSpeed, acceleration);
//		MyLog.LogI ("targetSpeed " + targetSpeed + " / "+ "currentSpeed " + currentSpeed  );
		if (playerPhysics.grounded){
			amountToMove.y = 0;
			// Jump
			if (Input.GetButtonDown("Jump")){
				amountToMove.y = jumpHeight;
			}
		}
		


		amountToMove.x = currentSpeed;
		amountToMove.y -= gravity * Time.deltaTime;
		playerPhysics.Move (amountToMove * Time.deltaTime);
		
	}

	// Increase n towoards target by speed
	private float IncrementTowards (float currentSpeed, float targetSpeed, float acceleration)
	{
		if (currentSpeed == targetSpeed) {
			return currentSpeed;
		} else {
			float dir = Mathf.Sign (targetSpeed - currentSpeed);
			currentSpeed += acceleration * Time.deltaTime * dir;
			return (dir == Mathf.Sign (targetSpeed - currentSpeed)) ? currentSpeed : targetSpeed;
		}
	}
	
	
	 
}
