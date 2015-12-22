//DataLoader.cs simply loads race preferences and assigns them to the RaceManager.
using UnityEngine;
using System.Collections;

public class DataLoader : MonoBehaviour {

	public string ResourceFolder = "PlayerCars/"; //the name of the folder within the Resources folder where your cars are stored.
	public bool loadPreferences;
	
	private void OnEnable(){
		if(loadPreferences)
			LoadRacePreferences();
	}
	
	private void LoadRacePreferences(){
		//Load race prefernce if there is an active RaceManager
		if(!RaceManager.instance)
			return;
		
		//load player cars from the resources folder
		if(PlayerPrefs.GetString("PlayerCar") != ""){
			RaceManager.instance.playerCar = (GameObject)Resources.Load(ResourceFolder + PlayerPrefs.GetString("PlayerCar"));
		}
		
		//load player name
		if(PlayerPrefs.GetString("PlayerName") != ""){
			RaceManager.instance.playerName = PlayerPrefs.GetString("PlayerName");
		}
		
		//load laps
		if(PlayerPrefs.GetInt("Laps") != 0){ 
			RaceManager.instance.totalLaps = PlayerPrefs.GetInt("Laps");
		}
		
    	//load racers
		RaceManager.instance.totalRacers = PlayerPrefs.GetInt("Opponents") + 1;

    	//load race type
    	switch(PlayerPrefs.GetInt("RaceType")){
			case 1 :
			RaceManager.instance._raceType = RaceManager.RaceType.Circuit;
			break;
			
			case 2 :
			RaceManager.instance._raceType = RaceManager.RaceType.LapKnockout;
			break;
		
			case 3 :
			RaceManager.instance._raceType = RaceManager.RaceType.TimeTrial;
			break;
			
			case 4 :
			RaceManager.instance._raceType = RaceManager.RaceType.SpeedTrap;
			break;
		}
	}
}
