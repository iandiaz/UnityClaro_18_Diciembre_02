//Statistics.cs keeps track of the racer's rank, lap, race times, race state, saving best times, split times , wrong way detecion etc.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Statistics : MonoBehaviour {
    public GameObject BanderaPartida;
	
	//Int
	public int rank;//current rank
	public int lap; //current lap
    public static bool CarreraTerminada;
    
	
	//Strings
	public string currentLapTime; //current lap time string displayed by RaceUI.cs
	public string prevLapTime; //Previous lap time string displayed by RaceUI.cs
	public string totalRaceTime; //Total lap time string displayed by RaceUI.cs
	
	//Floats
	private float lapTimeCounter; // keeps track of our current Lap time counter
	private float totalTimeCounter; //keeps track of our total race time
	private float currentBestTime; //keeps track of the current session best time in TimeTrial
	private float dotProduct; //used for wrong way detection
	private float registerDistance = 10.0f; //distance to register a passed node
	
	
	//Hidden Vars
	[HideInInspector]public Transform lastPassedNode; //last node to passed - used when respawning.
	[HideInInspector]public Transform target; //progress tracker target
	[HideInInspector]public int currentNodeNumber; //next node index in the "path" list
	[HideInInspector]public List <Transform> path = new List<Transform>();
	[HideInInspector]public List <bool> passednodes = new List<bool>();
	[HideInInspector]public List <Transform> checkpoints = new List<Transform>();
	[HideInInspector]public List <bool> passedcheckpoints = new List<bool>();
	[HideInInspector]public bool finishedRace;
	[HideInInspector]public bool knockedOut;
	[HideInInspector]public bool goingWrongway;
	[HideInInspector]public bool passedAllNodes;
	[HideInInspector]public float speedRecord;//speed trap top speed
	
	void OnEnable () {
		//Disable components of no race manager is found.
		if(!RaceManager.instance){
			this.enabled = false;
			GetComponent<ProgressTracker>().enabled = this.enabled;
		}
		else{
			FindPath();
			FindCheckpoints();
			Initialize();
		}
	}
	
	void Initialize(){
		lap = 1;
	}
	
	
	void FindPath(){
		Transform pathContainer = RaceManager.instance.pathContainer;
		Transform[] nodes = pathContainer.GetComponentsInChildren<Transform>();
		
		foreach(Transform p in nodes){
			
			if(p != pathContainer){
				path.Add(p);
			}
		}
		passednodes = new List <bool>(new bool[path.Count]);
		lastPassedNode = path[0];
	}
	
	
	void FindCheckpoints(){
		if(!RaceManager.instance.checkpointContainer)
			return;
		
		Transform checkpointContainer = RaceManager.instance.checkpointContainer;
		Transform[] _checkpoint = checkpointContainer.GetComponentsInChildren<Transform>();
		
		foreach(Transform t in _checkpoint){
			
			if(t != checkpointContainer){
				checkpoints.Add(t);
			}
		}
		
		passedcheckpoints = new List <bool>(new bool[checkpoints.Count]);
	}
	
	
	void Update () {
		GetPath();
		CalculateLapTime();
		CalculateAngleDifference();
	}
	
	
	void GetPath(){
		int n = currentNodeNumber;
		
		Transform node = path[n] as Transform;
		Vector3 nodeVector = target.InverseTransformPoint(node.position);
		
		//register that we have passed this node
		if (nodeVector.magnitude <= registerDistance){
			currentNodeNumber++;
			passednodes[n] = true;
			
			//set our last passed node
			if(n != 0)
				lastPassedNode = path[n - 1];
			else
				lastPassedNode = path[path.Count - 1];
		}
		
		//Check if all nodes have been passed
		foreach(bool pass in passednodes){
			if(pass == true){
				passedAllNodes = true;
			}
			else{
				passedAllNodes = false;
			}
		}
		
		//Reset the currentNodeNumber after passing all the nodes
		if(currentNodeNumber >= path.Count){  
			currentNodeNumber = 0; 
		}
	}
	
	
	// Check for wrong way
	void CalculateAngleDifference(){
		float nodeAngle = target.transform.eulerAngles.y;
		float transformAngle = transform.eulerAngles.y;
		float angleDifference = nodeAngle - transformAngle;
		
		if (Mathf.Abs(angleDifference) <= 230f && Mathf.Abs(angleDifference) >= 120){
			if(GetComponent<Rigidbody>().velocity.magnitude * 2.237f > 10.0f){
				goingWrongway = true;
			}
			else{
				goingWrongway = false;
			}
		}
		else{
			goingWrongway = false;
		}
	}
	
	
	// Race time calculations
	void CalculateLapTime(){
		
		if(RaceManager.instance.raceStarted && !knockedOut && !finishedRace){
			lapTimeCounter += Time.deltaTime;
			totalTimeCounter += Time.deltaTime;
		}
		
		//Format the time strings
		currentLapTime = RaceManager.instance.FormatTime(lapTimeCounter);
		totalRaceTime = RaceManager.instance.FormatTime(totalTimeCounter);
	}
	
	//Called on new lap
	public void NewLap(){
		
		if(gameObject.tag =="Player"){
			
			//Save a new best time if we dont currently have one
			if(PlayerPrefs.GetFloat("BestTimeFloat"+Application.loadedLevelName) == 0){
				PlayerPrefs.SetString("BestTime"+Application.loadedLevelName,currentLapTime);
				PlayerPrefs.SetFloat("BestTimeFloat"+Application.loadedLevelName,lapTimeCounter);
				PlayerPrefs.Save();
				
				//Show that we have a new best time
				if(RaceManager.instance.showRaceInfoMessages){
					RaceUI.instance.StartCoroutine(RaceUI.instance.ShowRaceInfo("You have a new best time!",2.0f));
				}
			}
			//Save a new best time if we beat our current best time
			if(PlayerPrefs.GetFloat("BestTimeFloat"+Application.loadedLevelName) > lapTimeCounter){
				PlayerPrefs.SetString("BestTime"+Application.loadedLevelName,currentLapTime);
				PlayerPrefs.SetFloat("BestTimeFloat"+Application.loadedLevelName,lapTimeCounter);
				PlayerPrefs.Save();
				
				//Show that we have a new best time
				if(RaceManager.instance.showRaceInfoMessages){
					RaceUI.instance.StartCoroutine(RaceUI.instance.ShowRaceInfo("You have a new best time!",2.0f));
				}
			}
		}
		
		
		//Check for knockout
		if(RaceManager.instance._raceType == RaceManager.RaceType.LapKnockout){
			//knock out the last racer when the 2nd last racer passes the finish line!
			if(this.rank == RankManager.instance.currentRacers - 1){
				RaceManager.instance.KnockoutRacer(RankManager.instance.racerRanks[RankManager.instance.currentRacers - 1].racer);
			}
		}
		
		//Reset our passed nodes & checkpoints
		for(int i = 0; i < passednodes.Count; i++){
			passednodes[i] = false;
		}
		
		for(int i = 0; i < passedcheckpoints.Count; i++){
			passedcheckpoints[i] = false;
		}
		
		//add a lap or finish the race deopending on our current lap and Race Type
		if(RaceManager.instance._raceType == RaceManager.RaceType.TimeTrial){
			
			//Create the ghost vehicle
			if(RaceManager.instance.enableGhostVehicle){
				
				if(lap == 1){
					currentBestTime = lapTimeCounter;
					if(GetComponent<GhostVehicle>()){
						GetComponent<GhostVehicle>().CacheValues();
					}
					RaceManager.instance.CreateGhostVehicle(gameObject);
				}
				
				//if we beat our last lap update the ghost
				else if(lap > 1 && lapTimeCounter < currentBestTime){
					currentBestTime = lapTimeCounter;
					if(GetComponent<GhostVehicle>()){
						GetComponent<GhostVehicle>().CacheValues();
					}
					RaceManager.instance.CreateGhostVehicle(gameObject);
				}
				
				//if we dont beat our last lap use the same ghost
				else if(lap > 1 && lapTimeCounter > currentBestTime){
					if(GetComponent<GhostVehicle>()){
						GetComponent<GhostVehicle>().UseCachedValues();
					}
					RaceManager.instance.CreateGhostVehicle(gameObject);
				}
			}
			
			//Reset the recorded positions on a new lap
			if(GetComponent<GhostVehicle>()){
				GetComponent<GhostVehicle>().ClearValues();
			}
			
			lap++;
		}
		else{
			if(lap < RaceManager.instance.totalLaps){
				lap++; 
				
				//Show the final lap indication text if set to true in RaceManager
				if(lap == RaceManager.instance.totalLaps && RaceManager.instance.showRaceInfoMessages && gameObject.tag == "Player"){
                    //RaceUI.instance.StartCoroutine(RaceUI.instance.ShowRaceInfo("Final Lap!",2.0f));
                    Debug.Log("Ultima vuelta");
                    DestroyObject(BanderaPartida);
				}
			}
			else{
				if(!knockedOut && !finishedRace){
					FinishRace();
				}
			}
		}
		
		//Set the previous lap time and reset the lap counter
		prevLapTime = currentLapTime;
		lapTimeCounter = 0.0f;
	}
	
	
	void FinishRace(){

		//decimos que cantiad de players ha terinado la carrera 
		Constantes.totalracersCompleted++;
		Debug.Log ("termino el player n:"+Constantes.totalracersCompleted);

		if (Constantes.totalracersCompleted == Constantes.totalRacers) {
			Constantes.totalracersCompleted=0;
			Constantes.raceFinished=true;
		}
		//Tell the RaceManager that player has finished the race
		if(gameObject.tag == "Player"){
			RaceManager.instance.EndRace(rank);
            CarreraTerminada = true;
        }

		//Continue after finish
		if(RaceManager.instance.continueAfterFinish){
			AIMode();
		}
		else{
			GetComponent<Car_Controller>().controllable = false;
		}
		
		finishedRace = true;	
	}
	
	
	// Switches a player car to an AI controlled car
	public void AIMode(){
		if(GetComponent<PlayerControl>()){
			Destroy(GetComponent<PlayerControl>());
			gameObject.AddComponent<OpponentControl>();
		}
	}
	
	
	// Switches a AI car to an human controlled car
	public void PlayerMode(){
		if(GetComponent<OpponentControl>()){
			Destroy(GetComponent<OpponentControl>());
			if(!GetComponent<PlayerControl>()){
				gameObject.AddComponent<PlayerControl>();
			}
			else{
				GetComponent<PlayerControl>().enabled = true;
			}
		}
	}
	
	
	void RegisterCheckpoint(Checkpoint.CheckpointType type){
		if(finishedRace || goingWrongway)
			return;
		
		switch(type){
			
		case Checkpoint.CheckpointType.Speedtrap : 
			if(RaceManager.instance._raceType != RaceManager.RaceType.SpeedTrap)
				return;
			
			//add to the cars total speed
			speedRecord += GetComponent<Car_Controller>().currentSpeed;
			
			//play a sound and show info
			if(gameObject.tag =="Player"){
				SoundManager.instance.PlaySound("checkpoint",true);
				if(RaceManager.instance.showRaceInfoMessages)
					RaceUI.instance.StartCoroutine(RaceUI.instance.ShowRaceInfo("+ " + GetComponent<Car_Controller>().currentSpeed + " mph", 1.0f));
			}
			
			break;
		}
	}
	
	
	void OnTriggerEnter(Collider other){
		//Finish line
		if(other.tag == "FinishLine" && passedAllNodes){
			NewLap();
		}
		
		//Checkpoint
		if(other.GetComponent<Checkpoint>()){
			for(int i = 0; i < checkpoints.Count; i++){
				if(checkpoints[i] == other.transform && !passedcheckpoints[i]){
					passedcheckpoints[i] = true;
					RegisterCheckpoint(checkpoints[i].GetComponent<Checkpoint>().checkpointType);
				}
			}
		}
	}
}
