//CheckpointContainer.cs helps you create checkpoints for your race track
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CheckpointContainer : MonoBehaviour {
	
	[HideInInspector]
	public Transform[] checkpoints;
		
	void Update () {
		Transform[] transforms = GetComponentsInChildren<Transform>();
		
		checkpoints = new Transform[transforms.Length];
		
		for(int i = 0; i < checkpoints.Length; i++){
			checkpoints[i] = transforms[i];
		}
		
		foreach(Transform child in transforms){
			if(child != transform){
				if(child.GetComponent<Checkpoint>().checkpointType == Checkpoint.CheckpointType.Speedtrap)
					child.name = "Checkpoint(Speedtrap)";
			}
		}
	}
}
