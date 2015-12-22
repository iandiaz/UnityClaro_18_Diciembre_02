//Simple class that initializes a time trial race.
using UnityEngine;
using System.Collections;

public class TimeTrialConfig : MonoBehaviour {


	void Start () {
		//Set AI to drive to the starting point
		GetComponent<Car_Controller>().controllable = true;
		GetComponent<PlayerControl>().enabled = false;
		gameObject.AddComponent<OpponentControl>();
		RaceUI.instance.StartCoroutine(RaceUI.instance.ShowRaceInfo("Auto Drive...",3.5f));
	}
	
	void OnTriggerEnter (Collider other) {
		if(other.tag == "FinishLine"){
			StartCoroutine(StartTimeTrial());
		}
	}
	
	IEnumerator StartTimeTrial(){
		//Handle UI
		RaceUI.instance.countdown.text = "GO!";
		SoundManager.instance.PlaySound("start",true);
		RaceManager.instance.StartRace();
			
		//Begin recording the ghost vehicle
		if(RaceManager.instance.enableGhostVehicle){
			if(!GetComponent<GhostVehicle>()){
				gameObject.AddComponent<GhostVehicle>();
				GetComponent<GhostVehicle>().record = true;
			}
		}
			
		//Enable player input and get rid of AI
		GetComponent<Statistics>().PlayerMode();
		
		yield return new WaitForSeconds(1);
		RaceUI.instance.countdown.text = "";
		Destroy(this);
	}
}
