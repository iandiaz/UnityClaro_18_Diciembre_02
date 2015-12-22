using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(RaceUI))]
public class RaceUI_Editor : Editor {
/* This Class Is Currently Not Used!
	RaceUI m_target;
	
	public void OnEnable () {
		m_target = (RaceUI)target;
	}
	
	
	public override void OnInspectorGUI () {
		
		//Race Panels
		GUILayout.BeginVertical("Box");
		GUILayout.Box("Race Panels",EditorStyles.boldLabel);
		EditorGUILayout.Space();
		m_target.racePanel = EditorGUILayout.ObjectField("Race",m_target.racePanel,typeof(GameObject),true) as GameObject;
		m_target.pausePanel = EditorGUILayout.ObjectField("Pause",m_target.pausePanel,typeof(GameObject),true) as GameObject;
		m_target.raceCompletePanel = EditorGUILayout.ObjectField("Race Complete",m_target.raceCompletePanel,typeof(GameObject),true) as GameObject;
		m_target.knockoutPanel = EditorGUILayout.ObjectField("Knockout",m_target.knockoutPanel,typeof(GameObject),true) as GameObject;
		GUILayout.EndVertical();
		
		//Race Texts
		GUILayout.BeginVertical("Box");
		GUILayout.Box("Race Texts",EditorStyles.boldLabel);
		EditorGUILayout.Space();
		m_target.rank = EditorGUILayout.ObjectField("Pos",m_target.rank,typeof(Text),true) as Text;
		m_target.lap = EditorGUILayout.ObjectField("Lap",m_target.lap,typeof(Text),true) as Text;
		m_target.currentLapTime = EditorGUILayout.ObjectField("CurrentTime",m_target.currentLapTime,typeof(Text),true) as Text;
		m_target.previousLapTime = EditorGUILayout.ObjectField("PreviousTime",m_target.previousLapTime,typeof(Text),true) as Text;
		m_target.bestLapTime = EditorGUILayout.ObjectField("BestTime",m_target.bestLapTime,typeof(Text),true) as Text;
		m_target.totalTime = EditorGUILayout.ObjectField("TotalTime",m_target.totalTime,typeof(Text),true) as Text;
		m_target.currentSpeed = EditorGUILayout.ObjectField("Speed",m_target.currentSpeed,typeof(Text),true) as Text;
		m_target.currentGear = EditorGUILayout.ObjectField("Gear",m_target.currentGear,typeof(Text),true) as Text;
		m_target.countdown = EditorGUILayout.ObjectField("Countdown",m_target.countdown,typeof(Text),true) as Text;
		m_target.raceInfo = EditorGUILayout.ObjectField("RaceInfo",m_target.raceInfo,typeof(Text),true) as Text;
		m_target.wrongwayText = EditorGUILayout.ObjectField("WrongWay",m_target.wrongwayText,typeof(Text),true) as Text;
		m_target.menuScene = EditorGUILayout.TextField("MenuScene",m_target.menuScene);
		GUILayout.EndVertical();
		
		//Race Standings
		GUILayout.BeginVertical("Box");
		GUILayout.Box("Race Standings",EditorStyles.boldLabel);
		EditorGUILayout.Space();
		for(int i = 0; i < m_target.raceStandings.Count; i++){
			EditorGUILayout.BeginHorizontal();
			GUILayout.Label(i+1 + ". ");
			EditorGUILayout.Space();
			GUILayout.Label("Pos");
			m_target.raceStandings[i].pos = EditorGUILayout.ObjectField(m_target.raceStandings[i].pos,typeof(Text),true,GUILayout.Width(75)) as Text;
			GUILayout.Label("Name");
			m_target.raceStandings[i].name = EditorGUILayout.ObjectField(m_target.raceStandings[i].name,typeof(Text),true,GUILayout.Width(75)) as Text;
			GUILayout.Label("Time");
			m_target.raceStandings[i].time = EditorGUILayout.ObjectField(m_target.raceStandings[i].time,typeof(Text),true,GUILayout.Width(75)) as Text;
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();
		}
		
		if(GUILayout.Button("Add",GUILayout.Width(130))){
			m_target.raceStandings.Add(new RaceUI.RaceStandings());
		}
		
		if(GUILayout.Button("Remove",GUILayout.Width(130))){
			if(m_target.raceStandings.Count > 0){
				m_target.raceStandings.Remove(m_target.raceStandings[m_target.raceStandings.Count - 1]);
			}
		}
		
		GUILayout.EndVertical();
		
		
		GUILayout.BeginVertical("Box");
		GUILayout.Box("Speedometer Settings",EditorStyles.boldLabel);
		EditorGUILayout.Space();
		m_target.needle = EditorGUILayout.ObjectField("Needle",m_target.needle,typeof(RectTransform),true) as RectTransform;
		m_target.minNeedleAngle = EditorGUILayout.FloatField("Total Laps",m_target.minNeedleAngle);
		m_target.maxNeedleAngle = EditorGUILayout.FloatField("Total Laps",m_target.maxNeedleAngle);
		m_target.rotationMultiplier = EditorGUILayout.FloatField("Total Laps",m_target.rotationMultiplier);
		GUILayout.EndVertical();
		
		GUILayout.BeginVertical("Box");
		GUILayout.Box("ScreenFade Settings",EditorStyles.boldLabel);
		EditorGUILayout.Space();
		m_target.screenFade = EditorGUILayout.ObjectField("Image",m_target.screenFade,typeof(Image),true) as Image;
		m_target.fadeSpeed = EditorGUILayout.FloatField("Total Laps",m_target.fadeSpeed);
		m_target.fadeOnStart = EditorGUILayout.Toggle("FadeOnStart",m_target.fadeOnStart);
		m_target.fadeOnExit = EditorGUILayout.Toggle("FadeOnExit",m_target.fadeOnExit);
		GUILayout.EndVertical();
		
		//Set dirty
		if(GUI.changed)
			EditorUtility.SetDirty(m_target);
	}
*/
}
