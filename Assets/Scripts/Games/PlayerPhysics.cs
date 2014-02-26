using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour
{
	public LayerMask collisionMask;
	private BoxCollider collider;
	private Vector3 size;
	private Vector3 center;
	private float dropDistancePerFrame = 0.005f;
	[HideInInspector]
	public bool
		grounded;
	Ray ray;
	RaycastHit hit;
	
	void Start ()
	{
		MyLog.LogE("haha");MyLog.LogI("haha");MyLog.Log("haha");
		collider = GetComponent<BoxCollider> ();
		size = collider.size;
		center = collider.center;
	}

	public void Move (Vector2 moveAmount)
	{
		float deltaX = moveAmount.x;
		float deltaY = moveAmount.y;
		Vector2 position = transform.position;
		grounded = false;
		for (int i = 0; i < 3; i++) {
			float dir = Mathf.Sign (deltaY);
			float xRay = (position.x + center.x - size.x / 2) + size.x / 2 * i;
			float yRay = position.y + center.y + size.y / 2 * dir; 
			
			ray = new Ray (new Vector2 (xRay, yRay), new Vector2 (0, dir));
			MyLog.DrawRay(ray.origin,ray.direction);
			if (Physics.Raycast (ray, out hit, Mathf.Abs (deltaY), collisionMask)) {
				// Distance Player vs Ground
				float distance = Vector3.Distance (ray.origin, hit.point);
				// Stop player on ground
				if (distance > dropDistancePerFrame) {
					deltaY = -distance + dropDistancePerFrame;
				} else {
					deltaY = 0;
				}
				grounded = true;
				break;
			}
		}
		Vector2 finalMoveAmount = new Vector2 (deltaX, deltaY);
		transform.Translate (finalMoveAmount);
	}
}
