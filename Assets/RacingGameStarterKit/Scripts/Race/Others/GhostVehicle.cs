//GhostVehicle.cs handles ghost behaviour - recording positions & playing them back.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostVehicle : MonoBehaviour {

	[System.Serializable]
	public class VehiclePositions{
		public Vector3 pos;
		public Quaternion rot;
		public Vector3 wheelRot;
		
		public VehiclePositions(Vector3 newPos, Quaternion newRot, Vector3 newWheelRot){
			pos = newPos;
			rot = newRot;
			wheelRot = newWheelRot;
		}
	}
	
	[HideInInspector]public List<VehiclePositions> vehiclePosition = new List<VehiclePositions>();
	[HideInInspector]public List<VehiclePositions> vehiclePositionCache = new List<VehiclePositions>();
	[HideInInspector]public List<Transform> vehicleWheels = new List<Transform>();
	[HideInInspector]public bool record;
	[HideInInspector]public bool play;
	private int progress;
	
	void Start(){
		Transform[] children = transform.GetComponentsInChildren<Transform>();
		foreach(Transform t in children){
			if(t.tag == "Wheel")
				vehicleWheels.Add(t);
		}
		
		if(vehicleWheels.Count <= 0)
			Debug.LogError("No wheels found! Please tag your wheel transforms 'Wheel' or reconfigure your car for the ghost vehicle to work properly");
	}
	
	void FixedUpdate(){
		if(vehicleWheels.Count <= 0)
			return;
			
		if(record)
			vehiclePosition.Add(new VehiclePositions(transform.position,transform.rotation,vehicleWheels[0].localEulerAngles));
			
		if(play)
			Playback();	
	}
	
	 void Playback(){
		if(progress < vehiclePosition.Count - 1){
			progress += 1;
			GetComponent<Rigidbody>().MovePosition(vehiclePosition[progress].pos);
    		transform.rotation = vehiclePosition[progress].rot;
    		
    		for(int i = 0; i < vehicleWheels.Count; i++){
    			vehicleWheels[i].localEulerAngles = new Vector3(vehiclePosition[progress].wheelRot.x,0,0);
    		}
    	}
    	else{
    		Destroy(gameObject);
    	}
    }
    
    
    public void UseCachedValues(){
    	if(vehiclePositionCache.Count <= 0)
    		return;
    	
    	vehiclePosition.Clear();
    	
    	for(int i = 0; i < vehiclePositionCache.Count; i ++){
    		vehiclePosition.Add(vehiclePositionCache[i]);
    	}
    }
    
    public void CacheValues(){
		
		vehiclePositionCache.Clear();
    	
    	for(int i = 0; i < vehiclePosition.Count; i ++){
    		vehiclePositionCache.Add(vehiclePosition[i]);
    	}
    }
    
    public void ClearValues(){
    	vehiclePosition.Clear();
    }
}