using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour {
	
	public Transform target;
	public float distance = 5.0f;
	public float height = 1.5f;
	public float heightDamping = 2.0f;
	
	public float lookAtHeight = 0.0f;
	
	public float rotationSnapTime = 0.35f; //Time taken to snap back to original rotation
	public float distanceSnapTime = 1.5f; //Time taken to snap back to the original distance
	public float distanceMultiplier = 0.025f; //Rate at which speed zoom occurs.
	
	private Vector3 lookAtVector;
	
	private float usedDistance;
	
	float wantedRotationAngle;
	float wantedHeight;
	
	private float currentRotationAngle = 45.0f;
	private float currentHeight;
	
	private Quaternion currentRotation;
	private Vector3 wantedPosition;
	
	private float yVelocity = 0.0F;
	private float zVelocity = 0.0F;
	
	void Start () {
		lookAtVector =  new Vector3(0,lookAtHeight,0);
		
		if(currentRotationAngle <= 0){
			currentRotationAngle = 45.0f;
		}
		
		//reduce the depth(this allows the the minimap camera to draw on top)
		GetComponent<Camera>().depth = -1;
	}
	
	void Update(){
		if(!target && GameObject.FindGameObjectWithTag("Player"))
			target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void LateUpdate () {
		if(target != null){
			
			wantedHeight = target.position.y + height;
			currentHeight = transform.position.y; 
			
			currentRotationAngle = Mathf.SmoothDampAngle(currentRotationAngle, wantedRotationAngle, ref yVelocity, rotationSnapTime);
			
			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
			
			wantedPosition = target.position;
			wantedPosition.y = currentHeight;
			
			usedDistance = Mathf.SmoothDampAngle(usedDistance, distance + (target.GetComponent<Rigidbody>().velocity.magnitude * distanceMultiplier), ref zVelocity, distanceSnapTime); 
			
			wantedPosition += Quaternion.Euler(0, currentRotationAngle, 0) * new Vector3(0, 0, -usedDistance);
			
			HandleRotation();
			
			transform.position = wantedPosition;
			//transform.LookAt(target.position + lookAtVector);
		}
	}
	
	private void HandleRotation(){
		if(target.GetComponent<Car_Controller>()){
			if(target.GetComponent<Car_Controller>().reversing && target.GetComponent<Car_Controller>().currentSpeed > 3.0f || Input.GetKey(KeyCode.C)){
				wantedRotationAngle = target.eulerAngles.y + 180;
			}
			else{
				wantedRotationAngle = target.eulerAngles.y;
			}
		}
		else if(target != null && !target.GetComponent<Car_Controller>()){
			if(Input.GetKey(KeyCode.C)){
				wantedRotationAngle = target.eulerAngles.y + 180;
			}
			else{
				wantedRotationAngle = target.eulerAngles.y;
			}
		}
	}
}