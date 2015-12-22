using UnityEngine;
using System.Collections;

public class hideCarSelecc : MonoBehaviour {
	public GameObject autoselec1;
	public GameObject autoselec2;

	// Use this for initialization
	void Start () {
		autoselec1.SetActive(true);
		autoselec2.SetActive(false);

	}
	public void SelecAuto1 () {
		autoselec1.SetActive(true);
		autoselec2.SetActive(false);

	}
	public void SelecAuto2 () {
		autoselec2.SetActive(true);
		autoselec1.SetActive(false);

	}

}
