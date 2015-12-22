using UnityEngine;
using System.Collections;
using Facebook.Unity; 
using Facebook; 

using System.Collections.Generic;
using UnityEngine.UI;
using Facebook.MiniJSON;


public class FBInicio : MonoBehaviour {

	public GameObject UIBttnFBlogin;
	public Text UINombre;

	// Use this for initialization
	void Start () {
		FB.Init(SetInit, OnHideUnity);  
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void SetInit()                                                                       
	{                                                                                            
		Debug.Log("SetInit");                                                                  
		
		Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
		if (FB.IsLoggedIn)
			DealMenuFBlogin (true);
		else {
			DealMenuFBlogin (false);

		}
	}    

	private void OnHideUnity(bool isGameShown)                                                   
	{                                                                                            
		Debug.Log("Is game showing? " + isGameShown);                                                                                       
	} 

	public void CallFBLogin()
	{
		Debug.Log ("CallFBLogin");
		var perms = new List<string>(){"public_profile", "email", "user_friends"};
		FB.LogInWithReadPermissions(perms, AuthCallback);
	}
	
	private void AuthCallback (ILoginResult result) {
		Debug.Log ("AuthCallback");
		
		if (FB.IsLoggedIn) {
			DealMenuFBlogin(true);
			// AccessToken class will have session details
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			// Print current access token's User ID
			Debug.Log(aToken.UserId);
			// Print current access token's granted permissions
			foreach (string perm in aToken.Permissions) {
				Debug.Log(perm);
			}
		} else {
			DealMenuFBlogin(false);
			Debug.Log("User cancelled login");
		}
	}

	void DealWithUserName (IGraphResult result)
	{
		if(result.Error != null)
		{
			Debug.Log ("Problem getting username");
			//LOG: Error Text

			
			FB.API ("/me?fields=id,first_name", HttpMethod.GET, DealWithUserName);
			return;
		}
		

		
		Debug.Log ("Hello, " + result);
		/*
		var dict = Json.Deserialize (result.ResultDictionary) as Dictionary<string,object>;
		string userName = dict["name"];
		Debug.Log ("Hello, " + userName);
		UINombre.text="Bienvenido "+userName;*/
		  
	}
	
	void DealMenuFBlogin(bool isLogged){
		if (isLogged) {
			UIBttnFBlogin.SetActive (false);	
			
			FB.API("/me/", HttpMethod.GET, delegate (IGraphResult result) {
				// Add error handling here
				if (result.ResultDictionary != null) {
					foreach (string key in result.ResultDictionary.Keys) {
						Debug.Log(key + " : " + result.ResultDictionary[key].ToString());
						if(key.Equals("name")){
							UINombre.text="Bienvenido "+result.ResultDictionary[key].ToString();
						}

					}
				}
			});


		}
			
		else {
			UIBttnFBlogin.SetActive (true);
		}
	} 


}
