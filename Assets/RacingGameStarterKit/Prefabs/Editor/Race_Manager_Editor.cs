using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(RaceManager))]
public class Race_Manager_Editor : Editor {
	
	RaceManager m_target;
	
	
	public void OnEnable () {
		m_target = (RaceManager)target;
	}
	
	public override void OnInspectorGUI(){
		//RACE SETTINGS
		GUILayout.BeginVertical("Box");
		GUILayout.Box("Race Settings",EditorStyles.boldLabel);
		EditorGUILayout.Space();
		m_target._raceType = (RaceManager.RaceType)EditorGUILayout.EnumPopup("Race Type",m_target._raceType);
		
		EditorGUILayout.Space();
		
		if(m_target._raceType != RaceManager.RaceType.LapKnockout && m_target._raceType != RaceManager.RaceType.TimeTrial){
			m_target.totalLaps = EditorGUILayout.IntField("Total Laps",m_target.totalLaps);
		}
		
		if(m_target._raceType != RaceManager.RaceType.TimeTrial){
			m_target.totalRacers = EditorGUILayout.IntField("Total Racers",m_target.totalRacers);
		}
		
		if(m_target._raceType == RaceManager.RaceType.TimeTrial){
			m_target.startPoint = EditorGUILayout.ObjectField("Start Point",m_target.startPoint,typeof(Transform),true) as Transform;
			m_target.enableGhostVehicle = EditorGUILayout.Toggle("Use Ghost Vehicle",m_target.enableGhostVehicle);
		}
		
		GUILayout.EndVertical();
		
		EditorGUILayout.Space();
		
		//RACE CONTAINER SETTINGS
		GUILayout.BeginVertical("Box");
		GUILayout.Box("Race Container Settings",EditorStyles.boldLabel);
		EditorGUILayout.Space();
		
		//Path
		if(!m_target.pathContainer){
			
			if(!GameObject.FindObjectOfType(typeof(WaypointCircuit))){
				EditorGUILayout.HelpBox("Create a Path!",MessageType.Warning);
			}
			else{
				EditorGUILayout.HelpBox("Assign the Path!",MessageType.Info);
			}
			
			EditorGUILayout.Space();
			if(!GameObject.FindObjectOfType(typeof(WaypointCircuit))){
				if(GUILayout.Button("Create Path",GUILayout.Width(190))){
					RGSK_Editor.CreatePath();
				}
			}
			else{
				if(GUILayout.Button("Assign Path",GUILayout.Width(190))){
					WaypointCircuit path = GameObject.FindObjectOfType(typeof(WaypointCircuit)) as WaypointCircuit;
					m_target.pathContainer = path.GetComponent<Transform>();
				}
			}
		}
		EditorGUILayout.Space();
		
		m_target.pathContainer = EditorGUILayout.ObjectField("Path Container",m_target.pathContainer,typeof(Transform),true) as Transform;
		
		//Spawnpoint
		if(!m_target.spawnpointContainer){
			
			if(!GameObject.FindObjectOfType(typeof(SpawnpointContainer))){
				EditorGUILayout.HelpBox("Create a Spawnpoint Container!",MessageType.Warning);
			}
			else{
				EditorGUILayout.HelpBox("Assign the Spawnpoint Container!",MessageType.Info);
			}
			
			EditorGUILayout.Space();
			
			if(!GameObject.FindObjectOfType(typeof(SpawnpointContainer))){
				if(GUILayout.Button("Create Spawnpoint Container",GUILayout.Width(190))){
					RGSK_Editor.CreateSpawnpoint();
				}
			}
			else{
				if(GUILayout.Button("Assign Spawnpoint Container",GUILayout.Width(190))){
					SpawnpointContainer sp = GameObject.FindObjectOfType(typeof(SpawnpointContainer)) as SpawnpointContainer;
					m_target.spawnpointContainer = sp.GetComponent<Transform>();
				}
			}
		}
		
		m_target.spawnpointContainer = EditorGUILayout.ObjectField("Spawnpoint Container",m_target.spawnpointContainer,typeof(Transform),true) as Transform;
		
		//Checkpoint
		if(!m_target.checkpointContainer){
			if(!GameObject.FindObjectOfType(typeof(CheckpointContainer))){
					EditorGUILayout.HelpBox("Speed Trap races require checkpoints. You can create a Checkpoint Container using the button below",MessageType.Info);
			}
			else{
				EditorGUILayout.HelpBox("Assign the Checkpoint Container!",MessageType.Info);
			}
			
			EditorGUILayout.Space();
			
			if(!GameObject.FindObjectOfType(typeof(CheckpointContainer))){
				if(GUILayout.Button("Create Checkpoint Container",GUILayout.Width(190))){
					RGSK_Editor.CreateCheckpoint();
				}
			}
			else{
				if(GUILayout.Button("Assign Checkpoint Container",GUILayout.Width(190))){
					CheckpointContainer cp = GameObject.FindObjectOfType(typeof(CheckpointContainer)) as CheckpointContainer;
					m_target.checkpointContainer = cp.GetComponent<Transform>();
				}
			}
		}
		m_target.checkpointContainer = EditorGUILayout.ObjectField("Checkpoint Container",m_target.checkpointContainer,typeof(Transform),true) as Transform;
		
		GUILayout.EndVertical();
		
		EditorGUILayout.Space();
		
		//RACE CAR SETTINGS
		GUILayout.BeginVertical("Box");
		GUILayout.Box("Race Car Settings",EditorStyles.boldLabel);
		EditorGUILayout.Space();
		m_target.playerCar = EditorGUILayout.ObjectField("Player Car Prefab:",m_target.playerCar,typeof(GameObject),true) as GameObject;
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		GUILayout.Label("Opponent Car Prefabs :");
		EditorGUILayout.Space();
		for(int i = 0; i < m_target.opponentCars.Count; i++){
			m_target.opponentCars[i] = EditorGUILayout.ObjectField((i+1).ToString(),m_target.opponentCars[i],typeof(GameObject),true) as GameObject;
		}
		EditorGUILayout.Space();
		if(GUILayout.Button("Add Opponent",GUILayout.Width(130))){
			GameObject newOpponent = null;
			m_target.opponentCars.Add(newOpponent);
		}
		if(GUILayout.Button("Remove Opponent",GUILayout.Width(130))){
			if(m_target.opponentCars.Count > 0){
				m_target.opponentCars.Remove(m_target.opponentCars[m_target.opponentCars.Count - 1]);
			}
		}
		GUILayout.EndVertical();
		
		EditorGUILayout.Space();
		
		//SPAWN SETTINGS
		GUILayout.BeginVertical("Box");
		GUILayout.Box("Spawn Settings",EditorStyles.boldLabel);
		EditorGUILayout.Space();
		m_target.playerStartRank = EditorGUILayout.IntField("Player Start Rank",m_target.playerStartRank);
		GUILayout.EndVertical();
		
		EditorGUILayout.Space();
		
		//MISC SETTINGS
		GUILayout.BeginVertical("Box");
		GUILayout.Box("Misc Settings",EditorStyles.boldLabel);
		EditorGUILayout.Space();
		m_target.continueAfterFinish = EditorGUILayout.Toggle("Racers Continue After Finish",m_target.continueAfterFinish);
		m_target.showRacerNames = EditorGUILayout.Toggle("Show Racer Names",m_target.showRacerNames);
		m_target.showRacerPointers = EditorGUILayout.Toggle("Minimap pointers",m_target.showRacerPointers);
		m_target.showRaceInfoMessages = EditorGUILayout.Toggle("Race Info Messages",m_target.showRaceInfoMessages);
		m_target.countdownDelay = EditorGUILayout.FloatField("Countdown Delay",m_target.countdownDelay);
		/*m_target.showSplitTimes = EditorGUILayout.Toggle("Show Split Times ",m_target.showSplitTimes);
		EditorGUI.BeginDisabledGroup (true);
		m_target.raceStarted = EditorGUILayout.Toggle("Race Started",m_target.raceStarted);
		m_target.raceCompleted = EditorGUILayout.Toggle("Race Completed",m_target.raceCompleted);
		m_target.raceKO = EditorGUILayout.Toggle("Knocked Out",m_target.raceKO);
		m_target.racePaused = EditorGUILayout.Toggle("Race Paused",m_target.racePaused);
		EditorGUI.EndDisabledGroup ();
		*/
		GUILayout.EndVertical();
		
		EditorGUILayout.Space();
		
		if(m_target.showRacerPointers){
			GUILayout.BeginVertical("Box");
			GUILayout.Box("Minimap Settings",EditorStyles.boldLabel);
			EditorGUILayout.Space();
			m_target.playerPointer = EditorGUILayout.ObjectField("Player Pointer",m_target.playerPointer,typeof(GameObject),true) as GameObject;
			m_target.opponentPointer = EditorGUILayout.ObjectField("Opponent Pointer",m_target.opponentPointer,typeof(GameObject),true) as GameObject;
			GUILayout.EndVertical();
			
			EditorGUILayout.Space();
		}
		
		
		//RACE NAMES SETTINGS
		GUILayout.BeginVertical("Box");
		GUILayout.Box("Racer Names",EditorStyles.boldLabel);
		EditorGUILayout.Space();
		m_target.playerName = EditorGUILayout.TextField("Player Name :",m_target.playerName);
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		if(m_target.opponentNamesList.Count <= 0){
			EditorGUILayout.HelpBox("Recomeneded : Generate opponent names in edit mode",MessageType.Warning);
		}
		else{
			EditorGUILayout.HelpBox(m_target.opponentNamesList.Count + " opponent names have been successfully generated from the racernames.txt file",MessageType.Info);
		}
		EditorGUILayout.Space();
		
		if(m_target.showRacerNames){
			m_target.racerName = EditorGUILayout.ObjectField("Racer Name Prefab",m_target.racerName,typeof(GameObject),true) as GameObject;
		}
		
		/*GUILayout.Label("Opponent Names :");
	for(int i = 0; i < m_target.opponentNamesList.Count; i++){
	EditorGUI.BeginDisabledGroup (true);
	m_target.opponentNamesList[i] = EditorGUILayout.TextField("", m_target.opponentNamesList[i]);
	EditorGUI.EndDisabledGroup ();
	}*/
		
		EditorGUILayout.Space();
		if(GUILayout.Button("Generate Opponent Names",GUILayout.Width(170))){
			m_target.LoadRacerNames();
		}
		if(GUILayout.Button("Clear Opponent Names",GUILayout.Width(170))){
			m_target.opponentNamesList.Clear();
		}
		GUILayout.EndVertical();
		
		
		//Set dirty
		if(GUI.changed){ EditorUtility.SetDirty(m_target);}
	}
}
