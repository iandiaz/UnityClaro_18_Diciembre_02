/*Race_Manager.cs handles the race logic - countdown, spawning cars, asigning racer names, checking race status, formatting time strings etc */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO; 

public class RaceManager : MonoBehaviour {
	
	public static RaceManager instance;
	public enum RaceType{Circuit, LapKnockout, TimeTrial, SpeedTrap}
	public RaceType _raceType;
	public int totalLaps = 3;
	public int totalRacers = 5; //The total number of racers (player included)
	public int playerStartRank = 4; //The rank you will start the race as
	public float raceDistance; //Your race track's distance.
	public float countdownDelay = 3.0f;
	public GameObject playerCar;
	public List <GameObject> opponentCars = new List <GameObject>();
	public Transform pathContainer;
	public Transform spawnpointContainer;
	public Transform checkpointContainer;
	public List<Transform> spawnpoints = new List <Transform>();
	public string playerName = "You";
	public List<string> opponentNamesList = new List<string>();
	public TextAsset opponentNames;
	public StringReader nameReader;
	public GameObject playerPointer, opponentPointer, racerName;
	public bool continueAfterFinish = true; //Should the racers keep driving after finish.
	public bool showRacerNames = true; //Should names appear above player cars
	public bool showRacerPointers = false; //Should minimap pointers appear above all racers
	public bool showRaceInfoMessages = false;//Show final lap indication , new best lap, speed trap & racer knockout information texts
	public bool raceStarted;    //has the race began
    public bool Carreraempezada;
    public bool raceCompleted; //has the player car finished the race
	public bool racePaused; //is the game paused
	public bool raceKO;//**LapKnockout Mode Only** has the player car been knockedOut
	
	//Time Trial
	public Transform startPoint;
	public bool enableGhostVehicle = true;
	public GameObject activeGhostCar;
	
	void Awake () {
		//create an instance
		instance = this;
	}
	
	void Start(){
		InitializeRace();
	}
	
	void InitializeRace(){
		switch(_raceType){
			
		case RaceType.LapKnockout : 
			if(totalRacers < 2){
				totalRacers = 2;
			}
			totalLaps = totalRacers - 1;
			break;
			
		case RaceType.TimeTrial :
			totalRacers = 1;
			break;
		}
		
		ConfigureNodes();
		SpawnRacers();
	}
	
	void SpawnRacers(){
		Debug.Log ("" + Constantes.carselected);
        //Constantes.carselected = "CARS/F1_Black_player";
        playerCar = (GameObject)Resources.Load("CARS/AutoJugador" + Constantes.carselected , typeof(GameObject));//GameObject.Find ("F1_red_AI");
		if(!playerCar){
			Debug.LogError("Please add a player car!");
			return;

		}
	

		spawnpoints.Clear();
		
		//Find the children of the spawnpoint container and add them to the spawnpoints List.
		Transform[] _sp = spawnpointContainer.GetComponentsInChildren<Transform>();
		foreach(Transform point in _sp){
			if(point != spawnpointContainer){
				spawnpoints.Add(point);
			}
		}
		
		
		//limit the total amount of race cars according to the spawnpoint count
		if(totalRacers > spawnpoints.Count){
			totalRacers = spawnpoints.Count;
		}
		else if (totalRacers <= 0){
			totalRacers = 1;
		}
		
		//Make sure the player spawns at a reasonable rank if "playerStartRank" is configured in an incorrect manner
		if(playerStartRank > spawnpoints.Count){
			playerStartRank = spawnpoints.Count;
		}
		else if(playerStartRank > totalRacers){
			playerStartRank = totalRacers;
		}
		else if(playerStartRank <= 0){
			playerStartRank = 1;
		}
		
		//spawn the cars
		for(int i = 0; i < totalRacers; i++){
			if(spawnpoints[i] != spawnpoints[playerStartRank-1] && opponentCars.Count > 0){
				Instantiate(opponentCars[Random.Range(0,opponentCars.Count)],spawnpoints[i].position,spawnpoints[i].rotation);
			}
			else if(spawnpoints[i] == spawnpoints[playerStartRank-1] && playerCar){
				if(_raceType != RaceType.TimeTrial){
					Instantiate(playerCar,spawnpoints[i].position,spawnpoints[i].rotation);
				}
				else{
					GameObject player = (GameObject)Instantiate(playerCar,startPoint.position,startPoint.rotation);
					player.AddComponent<TimeTrialConfig>();
					}
				}
			}
				
		//Set racer names, pointers and begin countdown after spawning the racers
		RankManager.instance.RefreshRacerCount();
		StartCoroutine(Countdown());
		SetRacerPreferences();
	}
	
	void SetRacerPreferences(){
		
		//Load opponent names if they havent already been loaded
		if(opponentNamesList.Count <= 0){
			LoadRacerNames();
		}
		
		//Assingn names/pointers to player & opponents
		Statistics[] racers = GameObject.FindObjectsOfType(typeof(Statistics)) as Statistics[];
		for(int i = 0; i < racers.Length; i++){
			
			if(racers[i].gameObject.tag == "Player"){
				racers[i].gameObject.name = playerName;
				if(showRacerPointers){
					GameObject m_pointer = (GameObject)Instantiate(playerPointer);
					m_pointer.GetComponent<RacerPointer>().target = racers[i].transform;
				}
			}
			else{
				racers[i].gameObject.name = opponentNamesList[Random.Range(0,opponentNamesList.Count)];
				if(showRacerNames){
					GameObject _name = (GameObject)Instantiate(racerName);
					_name.GetComponent<RacerName>().target = racers[i].transform;
				}
				if(showRacerPointers){
					GameObject o_pointer = (GameObject)Instantiate(opponentPointer);
					o_pointer.GetComponent<RacerPointer>().target = racers[i].transform;
				}
			}
		}
	}
	
	
	//Loads racer names from a .txt resource file
	public void LoadRacerNames(){
		if(!(TextAsset)Resources.Load("RacerNames",typeof(TextAsset))){
			Debug.Log("Names not found! Please add a .txt file named 'RacerNames' with a list of names to /Resources folder.");
			return;
		}
		int lineCount = 0;
		opponentNames = (TextAsset)Resources.Load("RacerNames",typeof(TextAsset));
		nameReader = new StringReader(opponentNames.text);
		
		
		string txt = nameReader.ReadLine();
		while (txt != null){
			lineCount++;
			if(opponentNamesList.Count < lineCount){
				opponentNamesList.Add(txt);
			}
			txt = nameReader.ReadLine();
		}
	}
	
	IEnumerator Countdown(){
		if(_raceType == RaceType.TimeTrial)
			yield break;
		
		//Display 3
		yield return new WaitForSeconds(countdownDelay);
		RaceUI.instance.countdown.text = "3";
        
        SoundManager.instance.PlaySound("countdown",true);
		
		//Display 2
		yield return new WaitForSeconds(1);
		RaceUI.instance.countdown.text = "2";
        
        SoundManager.instance.PlaySound("countdown",true);
		
		//Display 1
		yield return new WaitForSeconds(1);
		RaceUI.instance.countdown.text = "1";
        
        SoundManager.instance.PlaySound("countdown",true);
		
		//Display GO! and call StartRace();
		yield return new WaitForSeconds(1);
		RaceUI.instance.countdown.text = "VAMOS!";
        
        SoundManager.instance.PlaySound("start",true);
		StartRace();
		
		//Wait for 1 second and hide the text.
		yield return new WaitForSeconds(1);
		RaceUI.instance.countdown.text = "";
	}

	void Update(){
        if (Carreraempezada == true)
        {
            Constantes.carselected = "";
        }

    }
	
	//Used to knockout a racer
	public void KnockoutRacer(GameObject racer){
		
		racer.GetComponent<Car_Controller>().topSpeed = 25;
		racer.GetComponent<Statistics>().knockedOut = true;
		
		if(racer.tag == "Player"){
			raceKO = true;
			racer.GetComponent<Statistics>().AIMode();
		}
		
		
		if(showRaceInfoMessages){
			RaceUI.instance.StartCoroutine(RaceUI.instance.ShowRaceInfo(racer.name + " has been knocked out",2.0f));
		}
		
		RankManager.instance.RefreshRacerCount();
		ChangeLayer(racer.transform,"IgnoreCollision");
		racer.name += "(K.O)";
		
		//maximize the car's caution speed due to reduced top speed
		racer.GetComponent<OpponentControl>().cautionSpeed = 1.0f;
	}
	
	
	public void StartRace () {
		//enable cars to start racing
		Car_Controller[] cars = GameObject.FindObjectsOfType(typeof(Car_Controller)) as Car_Controller[];
		foreach(Car_Controller c in cars){
			c.controllable = true;
		}
		raceStarted = true;
        Car_Controller.Carreraempezada = true;
	}
	
	public void EndRace(int rank){
		raceCompleted = true;
		
		switch(rank){
		case 1 : Debug.Log("You finished 1st in " + _raceType + " race"); break;
		case 2 : Debug.Log("You finished 2nd in " + _raceType + " race"); break;
		case 3 : Debug.Log("You finished 3rd in " + _raceType + " race"); break;
		case 4 : Debug.Log("You finished 4th in " + _raceType + " race"); break;
		}
	}
	
	//Creates an active ghost car
	public void CreateGhostVehicle(GameObject racer){
		
		//Destroy any active ghost
		if(activeGhostCar){
			Destroy(activeGhostCar);
		}
		
		//Create a duplicate car
		GameObject ghost = (GameObject)Instantiate(racer,Vector3.zero,Quaternion.identity);
		ghost.name = "Ghost";
		ghost.tag = "Untagged";
		ghost.GetComponent<GhostVehicle>().record = false;
		ghost.GetComponent<GhostVehicle>().play = true;
		activeGhostCar = ghost;
		
		//change its layer and materials
		ChangeLayer(ghost.transform,"IgnoreCollision");
		ChangeMaterial(ghost.transform,"GhostMaterial");
		
		//Handle rigidbody tweaks
		ghost.GetComponent<Rigidbody>().useGravity = false;
		ghost.GetComponent<Rigidbody>().isKinematic = true;
		
		//Remove all other components
		Destroy(ghost.GetComponent<PlayerControl>());
		Destroy(ghost.GetComponent<Car_Controller>());
		Destroy(ghost.GetComponent<Statistics>());
		Destroy(ghost.GetComponent<ProgressTracker>());
		
		if(ghost.GetComponent<CameraSwitcher>()){
			Destroy(ghost.GetComponent<CameraSwitcher>());
		}
		
		if(ghost.GetComponent<WaypointArrow>()){
			Destroy(ghost.GetComponent<WaypointArrow>());
		}
		
		Transform[] components = ghost.GetComponentsInChildren<Transform>();
		foreach(Transform t in components){
			
			if(t.GetComponent<Wheels>()){
				Destroy(t.GetComponent<Wheels>());
			}
			
			if(t.GetComponent<WheelCollider>()){
				Destroy(t.GetComponent<WheelCollider>());
			}
			
			if(t.GetComponent<AudioSource>()){
				Destroy(t.GetComponent<AudioSource>());
			}
			
			if(t.GetComponent<ParticleEmitter>()){
				Destroy(t.gameObject);
			}
			
			if(t.GetComponent<Camera>()){
				Destroy(t.gameObject);
			}
		}
	}
	
	
	//Format a float to a time string
	public string FormatTime(float time){
		int minutes  = (int)Mathf.Floor(time / 60);
		int seconds = (int)time % 60;
		int milliseconds = (int)(time * 100) % 100;
		
		return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
	}
	
	
	//* Checks if all racers have finished
	public bool AllRacersFinished(){
		bool allFinished = false;
		Statistics[] allRacers = GameObject.FindObjectsOfType(typeof(Statistics)) as Statistics[];
		for(int i = 0; i < allRacers.Length; i++){
            if (allRacers[i].finishedRace)
                allFinished =  true;
			
				allFinished = false;
		}
		
		return allFinished;
	}
	//*/
	
	//Used to calculate track distance(in Meters) & rotate the nodes correctly
	void ConfigureNodes(){
		Transform[] m_path = pathContainer.GetComponentsInChildren<Transform>();
		List<Transform> m_pathList = new List<Transform>();
		foreach(Transform node in m_path){
			if( node != pathContainer){
				m_pathList.Add(node);
			}
		}
		for(int i = 0; i < m_pathList.Count; i++){
			if(i < m_pathList.Count-1){
				m_pathList[i].transform.LookAt(m_pathList[i+1].transform);
				raceDistance += Vector3.Distance(m_pathList[i].position,m_pathList[i + 1].position);
			}
			else{
				m_pathList[i].transform.LookAt(m_pathList[0].transform);
			}
		}
	}
	
	//used to change a racers layer to "ignore collision" after being knocked out & on respawn
	public void ChangeLayer(Transform racer, string LayerName){
		for (int i = 0; i < racer.childCount; i++){
			racer.GetChild(i).gameObject.layer = LayerMask.NameToLayer(LayerName);
			ChangeLayer(racer.GetChild(i), LayerName);
		}
	}
	
	
	//used to change a racers material when creating a ghost car
	public void ChangeMaterial(Transform racer, string MaterialName){
		Transform[] m = racer.GetComponentsInChildren<Transform>();
		
		foreach(Transform t in m){
			if(t.GetComponent<Renderer>()){
				if(t.GetComponent<Renderer>().materials.Length == 1){
					t.gameObject.GetComponent<Renderer>().material = (Material)Resources.Load("Material/" + MaterialName);
				}
				else{
					Material[] newMats = new Material[t.GetComponent<Renderer>().materials.Length];
					for(int i = 0; i < newMats.Length; i++){
						newMats[i] = (Material)Resources.Load("Material/" + MaterialName);
					}
					
					t.gameObject.GetComponent<Renderer>().materials = newMats;
				}
			}
		}
	}
    private void LaunchProjectile()
    {
        Debug.Log("terminado");
        Application.LoadLevel("instrucciones3");
    }
}
