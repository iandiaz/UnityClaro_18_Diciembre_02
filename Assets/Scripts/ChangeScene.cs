using UnityEngine;
using System.Collections;
using Facebook.Unity;

public class ChangeScene : MonoBehaviour {

	 public void ChangetoScene(string sceneToChangeTo) {
		Application.LoadLevel(sceneToChangeTo);
	
	}

    

}
