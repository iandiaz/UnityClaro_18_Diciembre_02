//racerpointer.cs is used to display a minimap pointer above a racer

using UnityEngine;
using System.Collections;

public class RacerPointer : MonoBehaviour {

    public Transform target;
    public float height = 25.0f;
    private float wantedHeight;
    
    void Start(){
    wantedHeight = transform.position.y + height;
    }
    
	void Update () {
	transform.position = new Vector3(target.position.x, wantedHeight, target.position.z);
	}
}
