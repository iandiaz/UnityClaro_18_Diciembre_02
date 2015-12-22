using UnityEngine;
using System.Collections;
using Facebook.Unity; 
using Facebook; 
using System.Collections.Generic;


public class init : MonoBehaviour {


	void Awake(){

		//FB.Init(this.OnInitComplete, this.OnHideUnity);
		//InvokeRepeating("LaunchProjectile", 5, 0);

	}

	public void bttn_loginfb(){
		FB.Init(SetInit, OnHideUnity);  
	}

	public void bttn_jugar(){
		Application.LoadLevel("setuplens");
	}
	private void SetInit()                                                                       
	{                                                                                            
		Debug.Log("SetInit");                                                                  
	
		Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);

		CallFBLogin();
	}    
	private void LaunchProjectile()
	{
		Debug.Log ("PASANDO AL JUEGO");
		Application.LoadLevel("setuplens");
	}

	private void OnHideUnity(bool isGameShown)                                                   
	{                                                                                            
		Debug.Log("Is game showing? " + isGameShown);                                                                                       
	} 

	private void CallFBLogin()
	{
		Debug.Log ("CallFBLogin");
		var perms = new List<string>(){"public_profile", "email", "user_friends"};
		FB.LogInWithReadPermissions(perms, AuthCallback);
	}

	private void AuthCallback (ILoginResult result) {
		Debug.Log ("AuthCallback");

		if (FB.IsLoggedIn) {
			// AccessToken class will have session details
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			// Print current access token's User ID
			Debug.Log(aToken.UserId);
			// Print current access token's granted permissions
			foreach (string perm in aToken.Permissions) {
				Debug.Log(perm);
			}
		} else {
			Debug.Log("User cancelled login");
		}
	}

}
