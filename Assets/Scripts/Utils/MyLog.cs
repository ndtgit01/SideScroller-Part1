using UnityEngine;
using System.Collections;

public class MyLog : MonoBehaviour
{
	private static bool IN_DEBUG = true;
  
	public static void Log (string message)
	{
		if (IN_DEBUG) {
			Debug.Log ( message);
		} 
	}
	public static void LogI (string message)
	{
		if (IN_DEBUG) {
			Debug.Log ("<color=green>"+message+"</color> " );
		} 
	}
	public static void LogE (string message)
	{
		if (IN_DEBUG) {
			Debug.Log ("<color=red>"+message+"</color> " );
		} 
	}
	
	public static void DrawRay (Vector3 start, Vector3 dir)
	{
		if (IN_DEBUG) {
			Debug.DrawRay (start,dir);
		} 
	}
	
}
