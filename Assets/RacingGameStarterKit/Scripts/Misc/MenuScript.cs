//MenuScript.cs handles all menu functionality. You can either use it for your menu, learn from it or extend it to suit your menu's requirements

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

   [System.Serializable]
   public class MenuCar{
   public string name;
   public GameObject car;
   public string resourceName; //make sure this string matches the name of the car under "Resources" folder!
   }
   
   [System.Serializable]
   public class RaceTracks{
   public string trackName; 
   public Sprite trackImage;
   public string sceneName;
   }
   
   public enum MenuState{Main, CarSelection, TrackSelection};
   
   public MenuState menuState = MenuState.Main;
   public List<MenuCar> menuCars = new List<MenuCar>();
   public List<RaceTracks> raceTracks = new List<RaceTracks>();
   private int carIndex;
   private int trackIndex;
   private int raceType = 1;
   private int laps = 3;
   private int opponents = 3;
   public bool shouldRotateCar;
   
   [Header("Panels")]
   public GameObject mainPanel;
   public GameObject carSelection;
   public GameObject trackSelection;
   
   [Space(10)]
   public Text carName;
   public InputField raceTypeField;
   public InputField lapField;
   public InputField opponentField;
   public InputField playerNameField;
   public Image trackpanelImage;
   public Text trackNameText;
   public Text trackBestTime;
   
   private Vector3 initialRotation;
   
   
	void Start () {
	
		Time.timeScale = 1.0f;
	
		//Gets initial car rotation and set one active car
		InitializeVehicles();
	}
	
	void InitializeVehicles(){
		for(int i = 0; i < menuCars.Count; i++){
			initialRotation = menuCars[i].car.transform.eulerAngles;
			if(i != carIndex){
				menuCars[i].car.SetActive(false);
			}
		}
	}
	
	void Update () {

	//clamp values
	carIndex = Mathf.Clamp(carIndex,0,(menuCars.Count - 1));
	trackIndex = Mathf.Clamp(trackIndex,0,(raceTracks.Count - 1));
	raceType = Mathf.Clamp(raceType,1,4);
	
	PanelActivation();
	
    //handle selection input
    if(Input.GetKeyDown(KeyCode.LeftArrow)){
	Prev();
	}
	if(Input.GetKeyDown(KeyCode.RightArrow)){
	Next();
	}
	if(Input.GetKeyDown(KeyCode.Escape)){
	Back();
	}
	
	//handle car activation & rotation
		for(int i = 0; i < menuCars.Count; i++){
			if(i != carIndex){
				menuCars[i].car.SetActive(false);
				menuCars[i].car.transform.eulerAngles = initialRotation;
			}
			else{
				menuCars[i].car.SetActive(true);
				if(shouldRotateCar){
					menuCars[i].car.transform.Rotate(Vector3.up * 2 * Time.deltaTime);
				}
			}
		}
	}
	
	
	void PanelActivation(){
		switch(menuState){
			case MenuState.Main :
				mainPanel.SetActive(true);
				carSelection.SetActive(false);
				trackSelection.SetActive(false);
				
			break;
			
			case MenuState.CarSelection :
				mainPanel.SetActive(false);
				carSelection.SetActive(true);
				trackSelection.SetActive(false);
				
				//update car related UI
				carName.text = menuCars[carIndex].name;
			break;
			
			case  MenuState.TrackSelection :
				mainPanel.SetActive(false);
				carSelection.SetActive(false);
				trackSelection.SetActive(true);
				
				//update track related UI
				lapField.text = laps.ToString();
				opponentField.text = opponents.ToString();
				trackpanelImage.sprite = raceTracks[trackIndex].trackImage;
    			trackNameText.text = raceTracks[trackIndex].trackName;
				trackBestTime.text = "Best time : " + PlayerPrefs.GetString("BestTime"+raceTracks[trackIndex].sceneName);
				
				if(raceType == 1){
					raceTypeField.text = "Circuit";
				}
				else if(raceType == 2){
					raceTypeField.text = "Lap KO";
				}
				else if(raceType == 3){
					raceTypeField.text = "Time Trial";
				}
				else{
					raceTypeField.text = "Speed Trap";
				}
			break;
		}
	}
	
	#region UI button functions
	
	public void CarSelect(){
		PlayButtonSFX();
		menuState = MenuState.CarSelection;
	}
	
	public void TrackSelect(){
		PlayButtonSFX();
		menuState = MenuState.TrackSelection;
	}
	
	public void NextRaceType(){
		PlayButtonSFX();
		raceType++;
	}
	
	public void PrevRaceType(){
		PlayButtonSFX();
		raceType--;
	}
	
	public void AddLap(){
		PlayButtonSFX();
		laps++;
	}
	
	public void MinusLap(){
		PlayButtonSFX();
		if(laps > 1)
			laps--;
	}
	
	public void AddOpponent(){
		PlayButtonSFX();
		if(opponents < 5)
			opponents++;
	}
	
	public void MinusOpponent(){
		PlayButtonSFX();
		if(opponents > 0)
			opponents--;
	}
	
	public void Next(){
		PlayButtonSFX();
		switch(menuState){
			case MenuState.CarSelection : carIndex++; break;
			case MenuState.TrackSelection : trackIndex++; break;
		}
	}
	
	public void Prev(){
		PlayButtonSFX();
		switch(menuState){
			case MenuState.CarSelection : carIndex--; break;
			case MenuState.TrackSelection : trackIndex--; break;
		}
	}
	
	public void Back(){
		PlayButtonSFX();
		switch(menuState){
			case MenuState.CarSelection : menuState = MenuState.Main; break;
			case MenuState.TrackSelection : menuState = MenuState.Main; break;
		}
	}
	
	public void LoadTrack(){
	//save all prefernces
	PlayerPrefs.SetString("PlayerCar",menuCars[carIndex].resourceName);
	PlayerPrefs.SetInt("Laps",laps);
	PlayerPrefs.SetInt("Opponents",opponents);
	PlayerPrefs.SetString("PlayerName",playerNameField.text);
	//I use an int to determine the raceType (1 is circuit & 2 is lap knockout)
	PlayerPrefs.SetInt("RaceType",raceType);
	
	//load the selected track's scene
	Application.LoadLevel(raceTracks[trackIndex].sceneName);
	}
	
	#endregion UI button functions
	
	void PlayButtonSFX(){
		SoundManager.instance.PlaySound("Button",true);
	}
}
