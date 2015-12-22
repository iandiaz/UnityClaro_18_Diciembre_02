using UnityEngine;
using System.Collections;

public class QuickSortPuntajes : MonoBehaviour {

	public static ArrayList ordenarPuntajes(ArrayList puntajes){
		bool ordenado = true; 

		Debug.Log ("CANT PUNTAJES: "+puntajes.Count);
		PuntajeVO puntaje_pivote=null; 

		PuntajeVO puntaje_aux=null; 
		for (int i =0; i<puntajes.Count; i++) {
			
            Debug.Log("QuickSort Ejecutandose");

			if(i==0)puntaje_pivote=(PuntajeVO)puntajes[i];
			else { 
				PuntajeVO puntaje=(PuntajeVO)puntajes[i];

				if(int.Parse(puntaje.getTiempo().Replace(":", ""))>int.Parse(puntaje_pivote.getTiempo().Replace(":", "")))
                {
					puntaje_pivote=(PuntajeVO)puntajes[i];
				}
				else{
					ordenado=false;
					puntaje_aux=puntaje;

					puntajes[i]=puntaje_pivote;
					puntajes[i-1]=puntaje_aux;
				}
			}

        }

		Debug.Log ("DESPUES DE ORDENAR: ");
		string lista = "";
		for (int i =0; i<puntajes.Count; i++) {
			PuntajeVO puntaje_=(PuntajeVO)puntajes[i];
			lista+=" ("+puntaje_.getPosicion()+") "+int.Parse(puntaje_.getTiempo().Replace(":", ""));
		}

		Debug.Log ("L:"+lista);

        if (ordenado == true)
        {
            return puntajes;
        }
        else
        {
            return ordenarPuntajes(puntajes);
        }
		
		

	}
}
