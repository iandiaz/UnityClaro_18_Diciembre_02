using UnityEngine;
using System.Collections;

public class setuplens : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("START");
		InvokeRepeating("LaunchProjectile", 5, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void LaunchProjectile()
	{
		Debug.Log ("PASANDO AL JUEGO");
		//Application.LoadLevel("MobileScene");
		Application.LoadLevel("Initial Scene");
	}

}
