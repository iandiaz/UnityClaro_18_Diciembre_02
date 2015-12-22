using UnityEngine;
using System.Collections;
using Facebook.Unity;

public class Ranking : MonoBehaviour {
    private bool FinishedAndTested = false;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Constantes.raceFinished && !FinishedAndTested)
        {
            //salto a la nueva escena 
            InvokeRepeating("LaunchProjectile", 5, 0);
            Debug.Log("CARRERA FINALIZADA ");
            Debug.Log("TOTAL RACERS " + RankManager.instance.totalRacers);
            string mipuntaje = "";

            Constantes.listaPuntajes = new ArrayList();

            for (int i = 0; i < RankManager.instance.totalRacers-1; i++)
            {
                Debug.Log("RANKING: " + RankManager.instance.racerRanks[i].racer.GetComponent<Statistics>().rank);
                Debug.Log("RACE TIME: " + RankManager.instance.racerRanks[i].racer.GetComponent<Statistics>().totalRaceTime);
                Debug.Log("NOMBRE JUGADOR: " + RankManager.instance.racerRanks[i].racer.name);

                PuntajeVO puntaje = new PuntajeVO();
                puntaje.setPosicion(RankManager.instance.racerRanks[i].racer.GetComponent<Statistics>().rank);
                puntaje.setTiempo(RankManager.instance.racerRanks[i].racer.GetComponent<Statistics>().totalRaceTime);
                puntaje.setNombreJugador(RankManager.instance.racerRanks[i].racer.name);

                if (RankManager.instance.racerRanks[i].racer.name == "Jugador")
                {
                    mipuntaje = RankManager.instance.racerRanks[i].racer.GetComponent<Statistics>().totalRaceTime;
                    mipuntaje = mipuntaje.Replace(":", "");
                    int numVal = int.Parse(mipuntaje);

                    Debug.Log("mi puntaje:" + numVal.ToString());
                    if (FB.IsLoggedIn)
                    {
                        FBPuntajes.setscore(numVal.ToString());
                    }
                    else { }
                }

                Constantes.listaPuntajes.Add(puntaje);


            }

            FinishedAndTested = true;
        }

    }
    private void LaunchProjectile()
    {
        Debug.Log("terminado");
        Application.LoadLevel("instrucciones3");
    }

}
