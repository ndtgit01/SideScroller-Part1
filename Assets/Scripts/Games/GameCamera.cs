using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	private Transform target; 
	public float trackSpeed= 10;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void SetTarget (Transform t){
		target = t;
	}

	void LateUpdate(){
		if (target){
			float newX = GameManager.IncrementTowards(transform.position.x,target.position.x,trackSpeed);
			float newY = GameManager.IncrementTowards(transform.position.y,target.position.y,trackSpeed);
			transform.position = new Vector3(newX,newY,transform.position.z);
		}
	}
}
