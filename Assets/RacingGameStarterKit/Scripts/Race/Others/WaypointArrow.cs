//WaypointArrow.cs is used to point an arrow in the direction of the next path node
using UnityEngine;
using System.Collections;

public class WaypointArrow : MonoBehaviour {

	private Statistics _stats;
	private Transform waypointArrow;
	public float rotateSpeed = 2.0f;
	
	void Start () {
		//assing _stats
		_stats = GetComponent<Statistics>();
		
		//find and assign a waypoint arrow
		if(GameObject.Find("WaypointArrow"))
			waypointArrow = GameObject.Find("WaypointArrow").transform;
	}
	
	void Update () {
		if(!waypointArrow || gameObject.tag != "Player" || _stats.path.Count <= 0)
		return;
		
		if(!_stats.finishedRace && !_stats.knockedOut && waypointArrow.transform.root.GetComponent<Camera>().enabled){
			waypointArrow.gameObject.SetActive(true);
			Vector3 targetPosition = _stats.path[_stats.currentNodeNumber].transform.position - waypointArrow.position;
			targetPosition.y = 0;
			Quaternion targetRotation = Quaternion.LookRotation(targetPosition);
			waypointArrow.rotation = Quaternion.Slerp(waypointArrow.rotation,targetRotation, Time.deltaTime * rotateSpeed);
		}
		else{
			waypointArrow.gameObject.SetActive(false);
		}
	}
}
