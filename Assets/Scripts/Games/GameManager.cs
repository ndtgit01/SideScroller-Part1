using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject player;
	private GameCamera camera;
	// Use this for initialization
	void Start () {
		// player = GetComponent<PlayerController>() as GameObject;
		camera = GetComponent<GameCamera>();
		SpawnPlayer();
	}
	 
	private void SpawnPlayer(){
		camera.SetTarget((Instantiate(player,Vector3.zero,Quaternion.identity)as GameObject).transform);
	}
	
	// Increase n towoards target by speed
	public static float IncrementTowards (float currentSpeed, float targetSpeed, float acceleration)
	{
		if (currentSpeed == targetSpeed) {
			return currentSpeed;
		} else {
			float dir = Mathf.Sign (targetSpeed - currentSpeed);
			currentSpeed += acceleration * Time.deltaTime * dir;
			return (dir == Mathf.Sign (targetSpeed - currentSpeed)) ? currentSpeed : targetSpeed;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}



}
