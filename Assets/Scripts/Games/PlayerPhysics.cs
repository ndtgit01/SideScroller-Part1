using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour
{
	public LayerMask collisionMask;
	private BoxCollider collider;
	private Vector3 size;
	private Vector3 center;

	private Vector3 originalSize;
	private Vector3 originalCenter; 
	private float colliderScale;

	private int collisionDivisionsX = 3;
	private int collisionDivisionsY = 10;

	private float moveStep = 0.005f;

	[HideInInspector]
	public bool grounded;
	[HideInInspector]
	public bool movementStopped;

	Ray ray;
	RaycastHit hit;
	
	void Start ()
	{ 
		collider = GetComponent<BoxCollider> ();
		colliderScale = transform.localScale.x;
		originalSize = collider.size ;
		originalCenter = collider.center;
		SetCollider(originalSize,originalCenter);
	}

	public void Move (Vector2 moveAmount)
	{
		float newX = moveAmount.x;
		float newY = moveAmount.y;
		Vector2 position = transform.position;

		// Collision Top & Down
		grounded = false;
		for (int i = 0; i < collisionDivisionsX; i++) {
			float dir = Mathf.Sign (newY);
			float xRay = (position.x + center.x - size.x / 2) + size.x / (collisionDivisionsX-1) * i;
			float yRay = position.y + center.y + size.y / 2 * dir; 
			
			ray = new Ray (new Vector2 (xRay, yRay), new Vector2 (0, dir));
			MyLog.DrawRay(ray.origin,ray.direction);
			if (Physics.Raycast (ray, out hit, Mathf.Abs (newY)+moveStep, collisionMask)) {
				// Distance Player vs Ground
				float distance = Vector3.Distance (ray.origin, hit.point);
				// Stop player on ground
				if (distance > moveStep) {
					newY = -distance + moveStep;
				} else {
					newY = 0;
				}
				grounded = true;
				break;
			}
		}

		// Collision Left & Right
		movementStopped = false;
		for (int i = 0; i < collisionDivisionsY; i++) {
			float dir = Mathf.Sign (newX);

			float xRay = position.x + center.x + size.x / 2 * dir; 
			float yRay =  (position.y + center.y - size.y /2 ) + size.y /(collisionDivisionsY-1  ) * i; 
			
			ray = new Ray (new Vector2 (xRay, yRay), new Vector2 (dir, 0));
			MyLog.DrawRay(ray.origin,ray.direction);
			if (Physics.Raycast (ray, out hit, Mathf.Abs (newX)+ moveStep, collisionMask)) {
				// Distance Player vs Ground
				float distance = Vector3.Distance (ray.origin, hit.point);
				// Stop player on ground
				if (distance > moveStep) {
					newX = -distance + moveStep;
				} else {
					newX = 0;
				}
				movementStopped = true;
				break;
			}
		}

		if (!grounded && !movementStopped){
			// Collision Edge
			Vector3 playerDir = new Vector3(newX,newY,0);
			Vector3 startRay = new Vector3(position.x + center.x + size.x / 2 * Mathf.Sign (newX),position.y + center.y + size.y / 2 * Mathf.Sign (newY),0);
			MyLog.DrawRay(startRay,playerDir.normalized);
			ray = new Ray(startRay,playerDir.normalized);
			if (Physics.Raycast(ray,Mathf.Sqrt(newX*newX+newY*newY),collisionMask)){
				MyLog.LogI(" Collision with Edges: "+(Mathf.Sqrt(newX*newX+newY*newY)).ToString());
				grounded = true;
				newY = 0;
			}
		}




		Vector2 finalMoveAmount = new Vector2 (newX, newY);
		transform.Translate (finalMoveAmount,Space.World);
	}

	public void SetCollider(Vector3 size, Vector3 center){
		collider.size = size;
		collider.center = center; 
		this.size = size * colliderScale;
		this.center  = center * colliderScale; 
	}

	public void ResetCollider (){ 
		SetCollider(originalSize,originalCenter); 
	}

}
