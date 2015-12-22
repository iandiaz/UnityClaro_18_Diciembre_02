//Race_UI.cs handles displaying all UI in the race.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RaceUI : MonoBehaviour {
    public GameObject bandera1;
    public GameObject bandera2;
    float timer;
    [System.Serializable]
    public class RaceStandings {
        [Header("In Race Standing")]
        public Text racePanelRank;

        [Header("End Race Standing")]
        public Text pos;
        public Text name;
        public Text time;
    }
    private void LaunchProjectile()
    { Application.LoadLevel("instrucciones3"); }

    public static RaceUI instance;
    private GameObject player;

    [Header("Panels")]
    //Panels
    public GameObject racePanel;
    public GameObject pausePanel;
    public GameObject raceCompletePanel;
    public GameObject knockoutPanel;

    [Header("Texts")]
    //Texts
    public Text rank;
    public Text lap;
    public Text currentLapTime;
    public Text previousLapTime;
    public Text bestLapTime;
    public Text totalTime;
    public Text countdown;
    public Text countdown1;
    public Text raceInfo;
    public Text wrongwayText;
    public List<RaceStandings> raceStandings = new List<RaceStandings>();
    public string menuScene = "Menu";

    #region VehicleUI vars
    [Header("Vehicle UI")]
    public Text currentSpeed;
    public Text currentGear;
    [Header("Speedometer")]
    //Speedometer
    public RectTransform needle;
    public float minNeedleAngle = -20.0f;
    public float maxNeedleAngle = 160.0f;
    public float rotationMultiplier = 0.85f;
    private float needleRotation;
    #endregion

    [Header("ScreenFade")]
    //Screen Fader
    public Image screenFade;
    public float fadeSpeed = 0.5f;
    public bool fadeOnStart = true;
    public bool fadeOnExit = true;

    void Awake() {
        instance = this;
    }

    void Start() {
        InitializeUI();

        if (fadeOnStart && screenFade)
            StartCoroutine(ScreenFadeOut(fadeSpeed));
    }

    void InitializeUI() {
        //Clear race standing texts
        if (raceStandings.Count > 0) {
            for (int i = 0; i < raceStandings.Count; i++) {
                raceStandings[i].pos.text = "";
                raceStandings[i].name.text = "";
                raceStandings[i].time.text = "";
                if (raceStandings[i].racePanelRank != null)
                    raceStandings[i].racePanelRank.text = "";
            }
        }

        //clear race info
        if (raceInfo) {
            raceInfo.text = "";
        }
    }

    void Update() {
        if (!player) {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else {
            UpdateUI();
            HandlePanelActivation();
        }
    }

    void UpdateUI() {
        
        //speed & gear text
        currentSpeed.text = player.GetComponent<Car_Controller>().currentSpeed + " mph";
        currentGear.text = "Gear " + player.GetComponent<Car_Controller>().currentGear.ToString();

        //speedometer
        if (needle) {
            float fraction = player.GetComponent<Car_Controller>().currentSpeed / maxNeedleAngle;
            needleRotation = Mathf.Lerp(minNeedleAngle, maxNeedleAngle, (fraction * rotationMultiplier));
            needle.transform.eulerAngles = new Vector3(needle.transform.eulerAngles.x, needle.transform.eulerAngles.y, -needleRotation);
        }


        //rank 
        rank.text = "" + player.GetComponent<Statistics>().rank + "/" + RankManager.instance.currentRacers;

        //in race standings
        if (raceStandings.Count > 0) {
            for (int i = 0; i < RankManager.instance.totalRacers; i++) {
                if (RaceManager.instance._raceType != RaceManager.RaceType.SpeedTrap) {
                    if (raceStandings[i].racePanelRank && RankManager.instance.totalRacers > 1)
                        raceStandings[i].racePanelRank.text = RankManager.instance.racerRanks[i].racer.GetComponent<Statistics>().rank + ".  " + RankManager.instance.racerRanks[i].racer.name;
                }
                else {
                    if (raceStandings[i].racePanelRank && RankManager.instance.totalRacers > 1)
                        raceStandings[i].racePanelRank.text = RankManager.instance.racerRanks[i].racer.GetComponent<Statistics>().rank + ".  " + RankManager.instance.racerRanks[i].racer.name + " [" + RankManager.instance.racerRanks[i].speedRecord + " mph]";
                }
            }
        }

        
                      //current lap/maxlaps
            if (RaceManager.instance._raceType != RaceManager.RaceType.TimeTrial) {
            lap.text = "Lap " + player.gameObject.GetComponent<Statistics>().lap;
             }
        else {
            lap.text = "Lap " + player.gameObject.GetComponent<Statistics>().lap;
        }

        //current lap time
        currentLapTime.text = "Current " + player.gameObject.GetComponent<Statistics>().currentLapTime;

        //total race time
        totalTime.text = "Total " + player.gameObject.GetComponent<Statistics>().totalRaceTime;

        //previous lap time
        if (player.gameObject.GetComponent<Statistics>().prevLapTime != "") {
            previousLapTime.text = "Last " + player.gameObject.GetComponent<Statistics>().prevLapTime;
        }
        else {
            previousLapTime.text = "Last --:--:--";
        }

        //Best lap time
        if (PlayerPrefs.GetString("BestTime" + Application.loadedLevelName) != "") {
            bestLapTime.text = "Best " + PlayerPrefs.GetString("BestTime" + Application.loadedLevelName);
        }
        else {
            bestLapTime.text = "Best --:--:--";
        }

        //wrong way indication
        if (player.GetComponent<Statistics>().goingWrongway) {
            wrongwayText.text = "Wrong Way!";
        }
        else {
            wrongwayText.text = "";
        }

    }

    void HandlePanelActivation() {
        //if not paused, set all other panels active to false except from the race panel
        if (!RaceManager.instance.racePaused) {
            racePanel.SetActive(true);
            pausePanel.SetActive(false);
            raceCompletePanel.SetActive(false);

            if (knockoutPanel)
                knockoutPanel.SetActive(false);
        }

        //if paused, set all other panels active to false except from the pause panel
        if (RaceManager.instance.racePaused) {
            pausePanel.SetActive(true);
            racePanel.SetActive(false);
            raceCompletePanel.SetActive(false);

            if (knockoutPanel)
                knockoutPanel.SetActive(false);
        }

        //if the race is complete, set all other panels active to false except from the completion panel
        if (RaceManager.instance.raceCompleted) {
            pausePanel.SetActive(false);
            racePanel.SetActive(false);
			Constantes.raceFinished=true;

            //raceCompletePanel.SetActive(false);

            if (knockoutPanel){
				knockoutPanel.SetActive(false);
			}
			
			//loop through the total number of cars & show their race standings
			if(raceStandings.Count > 0){
				for(int i = 0; i < RankManager.instance.totalRacers; i++){
					//Display position
					raceStandings[i].pos.text = RankManager.instance.racerRanks[i].racer.GetComponent<Statistics>().rank.ToString();
					
					//Display name
					if(RaceManager.instance._raceType != RaceManager.RaceType.SpeedTrap){
						raceStandings[i].name.text = RankManager.instance.racerRanks[i].racer.name;
					}
					else{
						raceStandings[i].name.text = RankManager.instance.racerRanks[i].racer.name + " [" +  RankManager.instance.racerRanks[i].speedRecord + " mph]";
					}
					
					//Display time
					if(RankManager.instance.racerRanks[i].racer.GetComponent<Statistics>().finishedRace){
						raceStandings[i].time.text = RankManager.instance.racerRanks[i].racer.GetComponent<Statistics>().totalRaceTime;
					}
					else if(RankManager.instance.racerRanks[i].racer.GetComponent<Statistics>().knockedOut){
						raceStandings[i].time.text = "Knocked Out";
					}
					else{
						raceStandings[i].time.text = "Running...";
					}
				}
			}
		}
		
		
		//if the player is knocked out, set all other panels active to false except from the ko panel
		if(RaceManager.instance.raceKO){
			pausePanel.SetActive(false);
			racePanel.SetActive(false);
			raceCompletePanel.SetActive(false);
			knockoutPanel.SetActive(true);
		}
	}
	
	//Used to show useful race info
	public IEnumerator ShowRaceInfo(string info,float time){
		if(raceInfo.text == ""){
			raceInfo.text = info;
			yield return new WaitForSeconds(time);
			raceInfo.text = "";
		}
	}
	
	public IEnumerator ScreenFadeOut(float speed){
		//Get the color
		Color col = screenFade.color;
		
		//Change the alpha to 1
		col.a = 1;
		screenFade.color = col;
		
		//Fade out
		while(col.a > 0.0f){
			col.a -= Time.deltaTime * speed;
			screenFade.color = col;
			yield return null;
		}
	}
	
	public IEnumerator ScreenFadeIn(float speed){
		//Get the color
		Color col = screenFade.color;
		
		//Change the alpha to 0
		col.a = 0;
		screenFade.color = col;
		
		//Fade in
		while(col.a < 1.0f){
			col.a += Time.deltaTime * speed;
			screenFade.color = col;
			yield return null;
			
			//Load the menu scene when fade completes
			if(col.a >= 1.0f)
				Application.LoadLevel(menuScene);
		}
		
	}
	
	//UI button functions
	public void PauseResume(){
		RaceManager.instance.racePaused = !RaceManager.instance.racePaused;
	}
	
	public void Restart(){
		Application.LoadLevel(Application.loadedLevel);
	}
	
	public void Exit(){
		
		if(fadeOnExit && screenFade){
			if(RaceManager.instance.racePaused){
				PauseResume();
			}
			StartCoroutine(ScreenFadeIn(fadeSpeed * 2));
		}
		else{
			Application.LoadLevel(menuScene);
		}
	}
}

internal class yield
{
}