//sound manager.cs handles playing sounds and background music
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]

public class SoundManager : MonoBehaviour {
	
	[System.Serializable]
	public class GameSounds{
		public string soundName;
		public AudioClip sound;
	}
	
	public static SoundManager instance;
	public List <GameSounds> gameSounds = new List <GameSounds>();
	[Header("Background Music")]
	public AudioClip backgroundMusic;
	[Range(0,1)]public float volume = 0.5f;
	
	
	void Awake () {
		instance = this;
		if(!GetComponent<AudioSource>()){
		gameObject.AddComponent<AudioSource>();
		}
	}
	
	void Start(){
	if(backgroundMusic){
	GameObject bgm = new GameObject ("Background Music");
	bgm.AddComponent<AudioSource>();
	bgm.GetComponent<AudioSource>().clip = backgroundMusic;
	bgm.GetComponent<AudioSource>().volume = volume;
	bgm.GetComponent<AudioSource>().loop = true;
	bgm.GetComponent<AudioSource>().spatialBlend = 0;
	bgm.GetComponent<AudioSource>().Play();
	}
	}
	
	//Plays a sound in the list with 2 parameters - it's name and whether it's 2D/3D
	public void PlaySound (string name, bool sound2D) {
		if(sound2D){
			GetComponent<AudioSource>().spatialBlend = 0;
		}
		else{
			GetComponent<AudioSource>().spatialBlend = 1;
		}
		
		for(int i = 0; i < gameSounds.Count; i++){
			if(name == gameSounds[i].soundName){
				GetComponent<AudioSource>().PlayOneShot(gameSounds[i].sound);
			}
		}
	}
	
	//Optional if you want to play sound in the list at a certain location
	public void PlaySoundAtLocation (string name, Vector3 location) {
		GetComponent<AudioSource>().spatialBlend = 1;
		
		for(int i = 0; i < gameSounds.Count; i++){
			if(name == gameSounds[i].soundName){
				AudioSource.PlayClipAtPoint(gameSounds[i].sound,location);
			}
		}
	}
	
	//Optional if you want to play a clip located in a different class at a certain location
	 public void PlayClip (AudioClip clip, Vector3 position, float volume){
		GameObject go = new GameObject("One shot audio");
		go.transform.position = position;
		AudioSource source = go.AddComponent<AudioSource>() as AudioSource;
		source.spatialBlend = 1.0f;
		source.clip = clip;
		source.volume = volume;
		source.Play();
		Destroy(go, clip.length);
	}
}
