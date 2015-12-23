using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

public class setscore : MonoBehaviour {

	// Use this for initialization
	void Start () {
        setscoretest();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void setscoretest()
    {

        var scoredata = new Dictionary<string, string>();
        scoredata["score"] = Random.Range(20000,40000).ToString() ;

        FB.API("/me/scores", HttpMethod.POST, delegate (IGraphResult result)
        {
            Debug.Log("setscore" + result.RawResult);

        }, scoredata
);
    }
}
