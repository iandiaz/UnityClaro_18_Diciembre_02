using UnityEngine;
using System.Collections;
using UnityEditor;

public class RGSK_Editor : EditorWindow {
	
	private Transform modelObjectPrefab;
	public Transform FL;
	public Transform FR;
	public Transform RL;
	public Transform RR;
	private enum CarType{Player,Opponent}
	private CarType _carType;

	[MenuItem ("Window/RacingGameStarterKit/Car Setup Wizard")]
	public static void CreateCar () {
		EditorWindow.GetWindow (typeof (RGSK_Editor), true, "Car Setup Wizard");
	}

	[MenuItem ("Window/RacingGameStarterKit/Create Race Manager")]
	public static void CreateRaceManager () {
	
	if(!GameObject.FindObjectOfType(typeof(RaceManager))){
	
		GameObject _raceManager = new GameObject("Race_Manager");
		
		//Add neccesray components to the race manager
		_raceManager.AddComponent<RaceManager>();
		_raceManager.AddComponent<RaceUI>();
		_raceManager.AddComponent<RankManager>();
		_raceManager.AddComponent<SoundManager>();
		}
		
		else{
		Debug.Log("A Race Manager already exists!");
		}
	}
	
	//Race_Manager_Editor also handles this.
	[MenuItem ("Window/RacingGameStarterKit/Path Creator")]
	public static void CreatePath () {
	
	if(!GameObject.FindObjectOfType(typeof(PathCreator)) && !GameObject.FindObjectOfType(typeof(WaypointCircuit))){
	
		GameObject path = new GameObject("Path");
		GameObject node = new GameObject("01");
		
		node.transform.parent = path.transform;
		
		//Add neccesray components to the path
		Camera camera = SceneView.lastActiveSceneView.camera;
		path.AddComponent<PathCreator>();
		path.transform.position = camera.transform.position;

		//Select the newly created path
		Selection.objects = new Object[] { path };
		SceneView.lastActiveSceneView.FrameSelected();
		}
		
		else{
		Debug.Log("A Path already exists!");
		}
	}

	[MenuItem ("Window/RacingGameStarterKit/Spawnpoint Creator")]
	public static void CreateSpawnpoint () {
	
	if(!GameObject.FindObjectOfType(typeof(SpawnpointContainer))){
	
    GameObject spawnpointC = new GameObject("Spawnpoint_Container");
	GameObject child = new GameObject("01");
	
	//Add neccesray components to the spawnpoint
	Camera camera = SceneView.lastActiveSceneView.camera;
	spawnpointC.AddComponent<SpawnpointContainer>();
	child.transform.parent = spawnpointC.transform;
	child.transform.position = camera.transform.position;
	
	//Select the newly created spawnpoint
	Selection.objects = new Object[] { child };
	SceneView.lastActiveSceneView.FrameSelected();
	}
	else{
	Debug.Log("A Spawnpoint Container already exists!");
	}
	}
	
	[MenuItem ("Window/RacingGameStarterKit/Checkpoint Creator")]
	public static void CreateCheckpoint () {
	
	if(!GameObject.FindObjectOfType(typeof(CheckpointContainer))){
	
    GameObject checkpointC = new GameObject("Checkpoint_Container");
	GameObject child = new GameObject("01");
	child.layer = LayerMask.NameToLayer("Ignore Raycast");
	child.AddComponent<Checkpoint>();
	child.AddComponent<BoxCollider>();
	child.GetComponent<BoxCollider>().isTrigger= true;
	child.GetComponent<BoxCollider>().size = new Vector3(30,10,5);
	
	//Add neccesray components to the checkpointC
	Camera camera = SceneView.lastActiveSceneView.camera;
	checkpointC.AddComponent<CheckpointContainer>();
	child.transform.parent = checkpointC.transform;
	child.transform.position = camera.transform.position;
	
	//Select the newly created checkpointC
	Selection.objects = new Object[] { child };
	SceneView.lastActiveSceneView.FrameSelected();
	}
	else{
	Debug.Log("A Checkpoint Container already exists!");
	}
	}
	
	void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 200, 20), "Car Setup Wizard", EditorStyles.boldLabel);
		
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		
		EditorGUILayout.BeginHorizontal();
		{
			EditorGUILayout.LabelField("Select the type of car");
			_carType = (CarType)EditorGUILayout.EnumPopup( "", _carType);
		}
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
		{
			EditorGUILayout.LabelField("Drag your entire car model here");
			modelObjectPrefab = EditorGUILayout.ObjectField(modelObjectPrefab, typeof(Transform), true) as Transform;
		}
		EditorGUILayout.EndHorizontal();
	
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		
		//Wheels Setup
		GUI.Label(new Rect(10, 80, 200, 20), "Wheel Configuration", EditorStyles.boldLabel);
		
		EditorGUILayout.BeginHorizontal();
		{
			EditorGUILayout.LabelField("Front Left Wheel");
			FL = EditorGUILayout.ObjectField(FL, typeof(Transform), true) as Transform;
		}
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
		{
			EditorGUILayout.LabelField("Front Right Wheel");
			FR = EditorGUILayout.ObjectField(FR, typeof(Transform), true) as Transform;
		}
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
		{
			EditorGUILayout.LabelField("Rear Left Wheel");
			RL = EditorGUILayout.ObjectField(RL, typeof(Transform), true) as Transform;
		}
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
		{
			EditorGUILayout.LabelField("Rear Right Wheel");
			RR = EditorGUILayout.ObjectField(RR, typeof(Transform), true) as Transform;
		}
		EditorGUILayout.EndHorizontal();
		
		
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		
		if(GUILayout.Button("Configure Car") && modelObjectPrefab != null && FL != null && FR != null && RL != null && RR != null)
		{
			CreateNewCar();
		}
	}
	
	//create new car
	void CreateNewCar()
	{
		
		//Create parent objects
		GameObject wheelTransforms = new GameObject("WheelTransorms");
		GameObject wheelColliders = new GameObject("WheelColliders");
		GameObject carColliders = new GameObject("CarColliders");
		GameObject audioParent = new GameObject("Audio");
		
		//Wheel tags
		FL.tag = "Wheel";
		FR.tag = "Wheel";
		RL.tag = "Wheel";
		RR.tag = "Wheel";
		
		//Create wheelcolliders
		GameObject wc_FL = new GameObject("WC_FL");
		wc_FL.transform.position = FL.position;
		wc_FL.AddComponent<WheelCollider>();
		wc_FL.AddComponent<Wheels>();
		
		GameObject wc_FR = new GameObject("WC_FR");
		wc_FR.transform.position = FR.position;
		wc_FR.AddComponent<WheelCollider>();
		wc_FR.AddComponent<Wheels>();
		
		GameObject wc_RL = new GameObject("WC_RL");
		wc_RL.transform.position = RL.position;
		wc_RL.AddComponent<WheelCollider>();
		wc_RL.AddComponent<Wheels>();
		

		GameObject wc_RR = new GameObject("WC_RR");
		wc_RR.transform.position = RR.position;
		wc_RR.AddComponent<WheelCollider>();
		wc_RR.AddComponent<Wheels>();
		
		//Create colliders
		GameObject collider1 = new GameObject("Collider");
		collider1.transform.position = modelObjectPrefab.position;
		collider1.AddComponent<BoxCollider>();
		//Resemble the cars shape - Will need tweaking
		collider1.GetComponent<BoxCollider>().size = new Vector3(2,1.25f,5);
		collider1.GetComponent<BoxCollider>().center = new Vector3(0,0.85f,0);
		
		//This 2nd collider would cause issues by calling OnTriggerEnter twice!
		/*GameObject collider2 = new GameObject("Collider_2");
		collider2.transform.position = modelObjectPrefab.position;
		collider2.AddComponent<BoxCollider>();
		//Resemble the cars shape - Will need tweaking
		collider2.GetComponent<BoxCollider>().size = new Vector3(1.5f,.5f,2.5f);
		collider2.GetComponent<BoxCollider>().center = new Vector3(0,1.25f,0);*/
		
		//Create audio
		GameObject engineAudio = new GameObject("Engine");
		GameObject skidAudio = new GameObject("Skid");
		engineAudio.AddComponent<AudioSource>();
		skidAudio.AddComponent<AudioSource>();
		engineAudio.transform.position = modelObjectPrefab.position;
		skidAudio.transform.position = modelObjectPrefab.position;
		
		//Set parents
		wheelTransforms.transform.parent = modelObjectPrefab;
		wheelColliders.transform.parent = modelObjectPrefab;
		carColliders.transform.parent = modelObjectPrefab;
		audioParent.transform.parent = modelObjectPrefab;
		
		FL.transform.parent = wheelTransforms.transform;
		FR.transform.parent = wheelTransforms.transform;
		RL.transform.parent = wheelTransforms.transform;
		RR.transform.parent = wheelTransforms.transform;
		
		wc_FL.transform.parent = wheelColliders.transform;
		wc_FR.transform.parent = wheelColliders.transform;
		wc_RL.transform.parent = wheelColliders.transform;
		wc_RR.transform.parent = wheelColliders.transform;
		
		collider1.transform.parent = carColliders.transform;
		/*collider2.transform.parent = carColliders.transform;*/
		
		
		engineAudio.transform.parent = audioParent.transform;
		skidAudio.transform.parent = audioParent.transform;
		
		//Add a rigidbody component
		modelObjectPrefab.gameObject.AddComponent<Rigidbody>();
		modelObjectPrefab.GetComponent<Rigidbody>().mass = 1000;
		modelObjectPrefab.GetComponent<Rigidbody>().angularDrag = 0.05f;
		modelObjectPrefab.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
		
		//Add values to wheels.cs
		wc_FL.GetComponent<Wheels>().skidAudioSource = skidAudio.GetComponent<AudioSource>();
		wc_FR.GetComponent<Wheels>().skidAudioSource = skidAudio.GetComponent<AudioSource>();
		wc_RL.GetComponent<Wheels>().skidAudioSource = skidAudio.GetComponent<AudioSource>();
		wc_RR.GetComponent<Wheels>().skidAudioSource = skidAudio.GetComponent<AudioSource>();
			
		//configure by car type
		if(_carType == CarType.Player)
		{
			modelObjectPrefab.gameObject.tag = "Player";
			
			//Add all neccessary components
			modelObjectPrefab.gameObject.AddComponent<Car_Controller>();
			modelObjectPrefab.gameObject.AddComponent<PlayerControl>();
			modelObjectPrefab.gameObject.AddComponent<Statistics>();
			modelObjectPrefab.gameObject.AddComponent<ProgressTracker>();
			modelObjectPrefab.gameObject.AddComponent<CameraSwitcher>();
			modelObjectPrefab.gameObject.AddComponent<WaypointArrow>();
			
			//Assign vars
			modelObjectPrefab.GetComponent<Car_Controller>().FL_Wheel = FL;
			modelObjectPrefab.GetComponent<Car_Controller>().FR_Wheel = FR;
			modelObjectPrefab.GetComponent<Car_Controller>().RL_Wheel = RL;
			modelObjectPrefab.GetComponent<Car_Controller>().RR_Wheel = RR;
			
			modelObjectPrefab.GetComponent<Car_Controller>().FL_WheelCollider = wc_FL.GetComponent<WheelCollider>();
			modelObjectPrefab.GetComponent<Car_Controller>().FR_WheelCollider = wc_FR.GetComponent<WheelCollider>();
			modelObjectPrefab.GetComponent<Car_Controller>().RL_WheelCollider = wc_RL.GetComponent<WheelCollider>();
			modelObjectPrefab.GetComponent<Car_Controller>().RR_WheelCollider = wc_RR.GetComponent<WheelCollider>();
			
			modelObjectPrefab.GetComponent<Car_Controller>().engineAudioSource = engineAudio.GetComponent<AudioSource>();
			
			modelObjectPrefab.GetComponent<Car_Controller>().steerHelper = 0.8f;
	}
	
		else{
			
			modelObjectPrefab.gameObject.tag = "Opponent";
			
			//Add all neccessary components
			modelObjectPrefab.gameObject.AddComponent<Car_Controller>();
			modelObjectPrefab.gameObject.AddComponent<OpponentControl>();
			modelObjectPrefab.gameObject.AddComponent<Statistics>();
			modelObjectPrefab.gameObject.AddComponent<ProgressTracker>();
			
			
			//Assign vars
			modelObjectPrefab.GetComponent<Car_Controller>().FL_Wheel = FL;
			modelObjectPrefab.GetComponent<Car_Controller>().FR_Wheel = FR;
			modelObjectPrefab.GetComponent<Car_Controller>().RL_Wheel = RL;
			modelObjectPrefab.GetComponent<Car_Controller>().RR_Wheel = RR;
			
			modelObjectPrefab.GetComponent<Car_Controller>().FL_WheelCollider = wc_FL.GetComponent<WheelCollider>();
			modelObjectPrefab.GetComponent<Car_Controller>().FR_WheelCollider = wc_FR.GetComponent<WheelCollider>();
			modelObjectPrefab.GetComponent<Car_Controller>().RL_WheelCollider = wc_RL.GetComponent<WheelCollider>();
			modelObjectPrefab.GetComponent<Car_Controller>().RR_WheelCollider = wc_RR.GetComponent<WheelCollider>();
			
			modelObjectPrefab.GetComponent<Car_Controller>().engineAudioSource = engineAudio.GetComponent<AudioSource>();
			
			modelObjectPrefab.GetComponent<Car_Controller>().steerHelper = 0.8f;
			
		}
		
		this.Close();
		
		Debug.Log("Car was successfully configured!");
		
		//Focus on the new car
		Selection.objects = new Object[] { modelObjectPrefab.gameObject };
		SceneView.lastActiveSceneView.FrameSelected();
	}
}