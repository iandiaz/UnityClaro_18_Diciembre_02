//Path_Creator.cs helps visually create a path around your race track

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PathCreator : MonoBehaviour {
	
	[HideInInspector]
	public Transform[] nodes;
	private Color pathColor = new Color(1,1,1,0.2f);
	private Color nodeColor = Color.yellow;
	
	void OnDrawGizmos() {
		Gizmos.color = nodeColor;
		
		if(nodes.Length > 0){
			for(int i = 1; i < nodes.Length; i++){
				Gizmos.DrawSphere (new Vector3(nodes[i].position.x,nodes[i].position.y + 1.0f,nodes[i].position.z), .75f);
			}
		}
	}
	
	
	void Update () {
		Transform[] transforms = GetComponentsInChildren<Transform>();
		
		nodes = new Transform[transforms.Length];
		
		for(int i = 0; i < nodes.Length; i++)
		{
			nodes[i] = transforms[i];
		}
		
		for(int n = 0; n < nodes.Length; n++)
		{
			Debug.DrawLine(nodes[n].position - Vector3.down, nodes[(n+1)%nodes.Length].position - Vector3.down, pathColor);
		}
		
		int c = 0;
		foreach(Transform child in transforms){
			if(child != transform){
				child.name = (c++).ToString("000");
			}
		}
	}
}
