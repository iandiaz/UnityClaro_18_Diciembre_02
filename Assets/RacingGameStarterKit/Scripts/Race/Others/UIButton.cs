//UI_button.cs handles UGUI button presses for car movement. Used for mobile devices.
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

public float inputValue;
public float inputSensitivity = 1.5f;
public bool buttonPressed;

    public void OnPointerDown(PointerEventData eventData){

		buttonPressed = true;

	}

	public void OnPointerUp(PointerEventData eventData){
		 
		buttonPressed = false;
		
	}
	
	void Update(){
	
	if(buttonPressed){
	inputValue += Time.deltaTime * inputSensitivity;
	}
	else{
	inputValue -= Time.deltaTime * inputSensitivity;
	}
	
	inputValue = Mathf.Clamp(inputValue,0,1);
	
	}
}