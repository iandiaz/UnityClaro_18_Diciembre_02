using UnityEngine;
using System.Collections;

public class ChangeScene1 : MonoBehaviour {

	 public void ChangetoScene1(string sceneToChangeTo) {
        Application.LoadLevel("start" + Constantes1.modeselected ); 
	
	}

	

}
