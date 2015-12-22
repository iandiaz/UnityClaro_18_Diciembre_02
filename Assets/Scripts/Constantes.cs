using UnityEngine;
using System.Collections;

public class Constantes : MonoBehaviour {
	public static bool raceFinished=false;
	public static int totalracersCompleted = 0;
	public static int totalRacers = 5;

	public static string carselected;
	public static ArrayList listaPuntajes;

	public void setCarSelected(string selection){
		carselected = selection;
	}
}
