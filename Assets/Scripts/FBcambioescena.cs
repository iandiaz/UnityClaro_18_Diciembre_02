using UnityEngine;
using System.Collections;
using Facebook.Unity;

public class FBcambioescena : MonoBehaviour {

    public void ChangetoSceneFB(string sceneToChangeToFB)
    {
        if (FB.IsLoggedIn)
        {
            Application.LoadLevel("PuntajesFB");
        }

        else
        {
            Application.LoadLevel("Inicio");
        }
    }

}

