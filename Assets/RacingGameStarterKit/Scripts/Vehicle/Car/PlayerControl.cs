//PlayerControl.cs handles user input to control the car

using UnityEngine;
using System.Collections;


public class PlayerControl : MonoBehaviour {
	public enum InputTypes{Desktop,Mobile,Automatic}
	public enum SteerType{TiltToSteer,TouchSteer}
	private Car_Controller car_controller;
	public InputTypes inputType = InputTypes.Automatic;
	
	//Mobile Control Buttons
	[Header("Mobile Control Settings")]
	public SteerType steerType = SteerType.TiltToSteer;
	public UIButton accelerate;
	public UIButton brake;
	public UIButton rightTurn;
	public UIButton leftTurn;
	
	void Awake () {
		car_controller = GetComponent<Car_Controller>();
	}
	
	void Start(){
		if(inputType == InputTypes.Mobile)
			FindUI();
	}
	
	void FindUI(){
		//Finds the UI buttons. Make sure the UI Button exist and the names match orelse you will get an error! 
		if(GameObject.FindObjectOfType(typeof(UIButton)))
			accelerate = GameObject.Find("Accelerate_UI").GetComponent<UIButton>();
			brake = GameObject.Find("Brake_UI").GetComponent<UIButton>();
			if(steerType == SteerType.TouchSteer){
			rightTurn = GameObject.Find("TurnRight").GetComponent<UIButton>();
			leftTurn = GameObject.Find("TurnLeft").GetComponent<UIButton>();
			}
		
	}
	
	void Update () {
		switch(inputType){
		case InputTypes.Desktop :
			DesktopControl();
			break;
			
		case InputTypes.Mobile : 
			MobileControl();
			break;
			
		case InputTypes.Automatic :
			#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
			DesktopControl();
			#else
			MobileControl();
			#endif
			break;
		}
	}
	
	void DesktopControl(){
		
	/*	car_controller.steerInput = Mathf.Clamp(Input.GetAxis("Horizontal"),-1,1);
		car_controller.motorInput = Mathf.Clamp01(Input.GetAxis("Vertical"));
		car_controller.brakeInput = Mathf.Clamp01(-Input.GetAxis("Vertical"));
		
		//Respawn the car if we press the Enter key
		if(Input.GetKey(KeyCode.Return) && RaceManager.instance){
			if(RaceManager.instance.raceStarted)
				car_controller.Respawn();
		}*/
	}
	
	void MobileControl(){
		if(!accelerate || !brake)
		return;
		
		if(steerType == SteerType.TiltToSteer){
			//Calibrate(Landscape)
			Vector3 dir = Vector3.zero;
			dir.x = -Input.acceleration.y;
			dir.y = Input.acceleration.x;
			dir.z = Input.acceleration.z - 90;
			if(dir.sqrMagnitude > 1) dir.Normalize();
			dir *= Time.deltaTime * 2.5f;
			
			//steer according to the device tilt amount
			car_controller.steerInput = Input.acceleration.x;
		}
		else{
			//steer with the on-screen ui buttons
			if(rightTurn != null && leftTurn != null){
				car_controller.steerInput = rightTurn.inputValue;
				
				if(leftTurn.buttonPressed){
					car_controller.steerInput = -(leftTurn.inputValue);
				}
			}
		}
		
		//accelerate/brake/reverse with the on-screen ui buttons
		car_controller.motorInput = accelerate.inputValue;
		car_controller.brakeInput = brake.inputValue;	
	}
}
