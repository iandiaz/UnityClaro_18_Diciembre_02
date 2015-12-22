using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class puntajesScene : MonoBehaviour {
	public Text Tiempo;
    public Text Nombre;
    public Text Tiempo2;
    public Text Nombre2;
    public Text Tiempo3;
    public Text Nombre3;
    public Text Tiempo4;
    public Text Nombre4;
    public Text Tiempo5;
    public Text Nombre5;

    // Use this for initialization
    void Start () {
		Constantes.raceFinished = false; 
		Constantes.totalracersCompleted = 0;
        

        Constantes.listaPuntajes = QuickSortPuntajes.ordenarPuntajes (Constantes.listaPuntajes);
		int contador = 1;
        foreach (PuntajeVO puntaje in Constantes.listaPuntajes)
		{
			Debug.Log("TIEMPO:"+puntaje.getTiempo()+" RACER:"+puntaje.getNombreJugador());
           
			if (contador == 1) {
                Tiempo.text = (puntaje.getTiempo());
                Nombre.text = (puntaje.getNombreJugador());
            }
			if (contador == 2)
            {
                Tiempo2.text = (puntaje.getTiempo());
                Nombre2.text = (puntaje.getNombreJugador());
            }
			if (contador == 3)
            {
                Tiempo3.text = (puntaje.getTiempo());
                Nombre3.text = (puntaje.getNombreJugador());
            }
			if (contador == 4)
            {
                Tiempo4.text = (puntaje.getTiempo());
                Nombre4.text = (puntaje.getNombreJugador());
            }
			if (contador == 5)
            {
                Tiempo5.text = (puntaje.getTiempo());
                Nombre5.text = (puntaje.getNombreJugador());
            }

            
			contador++;
        }
	}
	

}
