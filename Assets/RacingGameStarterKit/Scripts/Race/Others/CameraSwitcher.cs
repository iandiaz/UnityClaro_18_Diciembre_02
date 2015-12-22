//CameraSwitcher.cs handles switching between you car's camera views
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CameraSwitcher : MonoBehaviour {
	
	private List<Camera> cameras = new List<Camera>();
	private int index;
	
	void Start () {

		//Find the main race Cam first
		PlayerCamera mainCam = GameObject.FindObjectOfType(typeof(PlayerCamera)) as PlayerCamera;
		cameras.Add(mainCam.GetComponent<Camera>());
		
		//Find all child cameras next
		if(transform.GetComponentInChildren<Camera>()){
			Camera[] children = transform.GetComponentsInChildren<Camera>();
			foreach(Camera c in children){
				cameras.Add(c);
				
				//reduce the depth(this allows the the minimap camera to draw on top)
				c.GetComponent<Camera>().depth = -1;
			}
		}
		
		//find a camera button if one exists
		if(GameObject.Find("Camera_UI")){
			GameObject.Find("Camera_UI").GetComponent<Button>().onClick.AddListener(() => SwitchCamera());
		}
		
		EnableCamera(0);
	}
	
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.V)){
			SwitchCamera();
		}
	}
	
	void SwitchCamera(){
		index++;
			if(index >= cameras.Count){
				index = 0;
			}
		EnableCamera(index);
	}
	
	void EnableCamera(int cameraIndex){
		for(int i = 0; i < cameras.Count; i++){
			if(i == cameraIndex){
				cameras[i].enabled = true;
				cameras[i].GetComponent<AudioListener>().enabled = true;
			}
			else{
				cameras[i].enabled = false;
				cameras[i].GetComponent<AudioListener>().enabled = false;
			}
		}
	}
}
