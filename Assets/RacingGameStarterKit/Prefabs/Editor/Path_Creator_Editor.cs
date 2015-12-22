using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PathCreator))]
public class Path_Creator_Editor : Editor {

    PathCreator m_target;
    
	public void OnEnable () {
    m_target = (PathCreator)target;
	}
	
	public override void OnInspectorGUI(){
	EditorGUILayout.HelpBox("This component helps you visually create a path around your track.\nCreate a complete path around your track and click the 'Finish' button when you are done",MessageType.Info);
	
	if(GUILayout.Button("Finish")){
	CreateWaypointCircuit();
	}
	}
	
	public void CreateWaypointCircuit(){
	m_target.gameObject.AddComponent<WaypointCircuit>();
	DestroyImmediate(m_target.gameObject.GetComponent<PathCreator>());
	}
}
