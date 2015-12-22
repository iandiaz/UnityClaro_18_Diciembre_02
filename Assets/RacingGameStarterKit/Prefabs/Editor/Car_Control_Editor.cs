using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Car_Controller))]
public class Car_Control_Editor : Editor {

	
	Car_Controller m_target;
	
	public void OnEnable () {
    m_target = (Car_Controller)target;
	}
	
	public override void OnInspectorGUI(){
	GUILayout.BeginVertical("Box");
	GUILayout.Box("Wheel Settings",EditorStyles.boldLabel);
	EditorGUILayout.Space();
	
	m_target._propulsion = (Car_Controller.Propulsion)EditorGUILayout.EnumPopup("Propulsion",m_target._propulsion);
	
	EditorGUILayout.Space();
	
	m_target.FL_Wheel = EditorGUILayout.ObjectField("Front Left Wheel Transform",m_target.FL_Wheel,typeof(Transform),true) as Transform;
	m_target.FR_Wheel = EditorGUILayout.ObjectField("Front Right Wheel Transform",m_target.FR_Wheel,typeof(Transform),true) as Transform;
	m_target.RL_Wheel = EditorGUILayout.ObjectField("Rear Left Wheel Transform",m_target.RL_Wheel,typeof(Transform),true) as Transform;
	m_target.RR_Wheel = EditorGUILayout.ObjectField("Rear Right Wheel Transform",m_target.RR_Wheel,typeof(Transform),true) as Transform;
	
	EditorGUILayout.Space();
	
	m_target.FL_WheelCollider = EditorGUILayout.ObjectField("Front Left WheelCollider",m_target.FL_WheelCollider,typeof(WheelCollider),true) as WheelCollider;
	m_target.FR_WheelCollider = EditorGUILayout.ObjectField("Front Right WheelCollider",m_target.FR_WheelCollider,typeof(WheelCollider),true) as WheelCollider;
	m_target.RL_WheelCollider = EditorGUILayout.ObjectField("Rear Left WheelCollider",m_target.RL_WheelCollider,typeof(WheelCollider),true) as WheelCollider;
	m_target.RR_WheelCollider = EditorGUILayout.ObjectField("Rear Right WheelCollider",m_target.RR_WheelCollider,typeof(WheelCollider),true) as WheelCollider;
	
	GUILayout.EndVertical();
	
	EditorGUILayout.Space();
	
	GUILayout.BeginVertical("Box");
	GUILayout.Box("Engine Settings",EditorStyles.boldLabel);
	EditorGUILayout.Space();
	
	m_target.engineTorque = EditorGUILayout.FloatField("Engine Torque",m_target.engineTorque);
	m_target.maxSteerAngle = EditorGUILayout.FloatField("Max Steer Angle",m_target.maxSteerAngle);
	m_target.topSpeed = EditorGUILayout.FloatField("Top Speed",m_target.topSpeed);
	m_target.brakePower = EditorGUILayout.FloatField("Brake Power",m_target.brakePower);
	m_target.numberOfGears = EditorGUILayout.IntField("Total Gears",m_target.numberOfGears);
	m_target.boost = EditorGUILayout.FloatField("Boost",m_target.boost);
    m_target.controllable = EditorGUILayout.Toggle("Controllable",m_target.controllable);
    m_target.canSlipstream = EditorGUILayout.Toggle("Slipstream",m_target.canSlipstream);
    GUILayout.EndVertical();
    
    EditorGUILayout.Space();
    
    GUILayout.BeginVertical("Box");
	GUILayout.Box("Stability Settings",EditorStyles.boldLabel);
	EditorGUILayout.Space();
    //m_target.centerOfMass = EditorGUILayout.ObjectField("Center Of Mass",m_target.centerOfMass,typeof(Transform),true) as Transform;
    m_target.centerOfMass = EditorGUILayout.Vector3Field("Center Of Mass",m_target.centerOfMass);
    EditorGUILayout.Space();
    m_target.antiRollAmount = EditorGUILayout.FloatField("Anti Roll Amount",m_target.antiRollAmount);
    m_target.downforce = EditorGUILayout.FloatField("Downforce",m_target.downforce);
	m_target.steerHelper = EditorGUILayout.Slider("Steer Helper",m_target.steerHelper,0.0f,1.0f);
	m_target.traction = EditorGUILayout.Slider("Traction",m_target.traction,0.0f,1.0f);
    GUILayout.EndVertical();
    
    
    EditorGUILayout.Space();
    
    GUILayout.BeginVertical("Box");
	GUILayout.Box("Sound Settings",EditorStyles.boldLabel);
	EditorGUILayout.Space();
	
	//Engine sound
	m_target.engineAudioSource = EditorGUILayout.ObjectField("Engine AudioSource",m_target.engineAudioSource,typeof(AudioSource),true) as AudioSource;
	m_target.engineSound = EditorGUILayout.ObjectField("Engine Sound",m_target.engineSound,typeof(AudioClip),true) as AudioClip;
	
	EditorGUILayout.Space();
	EditorGUILayout.Space();
	EditorGUILayout.Space();
	
	EditorGUILayout.LabelField("Crash Sounds");
	
	//Crash sounds
	for(int i = 0; i < m_target.crashSounds.Count; i++){
	m_target.crashSounds[i] = EditorGUILayout.ObjectField((i+1).ToString(),m_target.crashSounds[i],typeof(AudioClip),true) as AudioClip;
	}
	EditorGUILayout.Space();
	if(GUILayout.Button("Add Sound",GUILayout.Width(130))){
	AudioClip newClip = null;
	m_target.crashSounds.Add(newClip);
	}
	if(GUILayout.Button("Remove Sound",GUILayout.Width(130))){
	if(m_target.crashSounds.Count > 0){
	m_target.crashSounds.Remove(m_target.crashSounds[m_target.crashSounds.Count - 1]);
	}
	}
	GUILayout.EndVertical();
	
	EditorGUILayout.Space();
    
    GUILayout.BeginVertical("Box");
	GUILayout.Box("Misc Settings",EditorStyles.boldLabel);
	EditorGUILayout.Space();
    m_target.brakelightGroup = EditorGUILayout.ObjectField("Brake Lights",m_target.brakelightGroup,typeof(GameObject),true) as GameObject;
    m_target.steeringWheel = EditorGUILayout.ObjectField("Steering Wheel",m_target.steeringWheel,typeof(GameObject),true) as GameObject;
    if(m_target.canSlipstream){
    m_target.slipstreamRayHeight = EditorGUILayout.FloatField("Slipstream Ray Height",m_target.slipstreamRayHeight);
    }
    m_target.wrongwayRespawn = EditorGUILayout.Toggle("Wrongway Respawn",m_target.wrongwayRespawn);
    GUILayout.EndVertical();
    
    EditorGUILayout.Space();
    
    GUILayout.BeginVertical("Box");
	GUILayout.Box("Input",EditorStyles.boldLabel);
	EditorGUILayout.Space();
    EditorGUI.BeginDisabledGroup (true);
    m_target.motorInput = EditorGUILayout.FloatField("Motor Input",m_target.motorInput);
    m_target.brakeInput = EditorGUILayout.FloatField("Brake Input",m_target.brakeInput);
    m_target.steerInput = EditorGUILayout.FloatField("Steer Input",m_target.steerInput);
    EditorGUI.EndDisabledGroup();
    GUILayout.EndVertical();
    
    //Set dirty
    if(GUI.changed){ EditorUtility.SetDirty(m_target);}
	}
}
