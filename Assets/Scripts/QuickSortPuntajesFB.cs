using UnityEngine;
using System.Collections;

public class QuickSortPuntajesFB : MonoBehaviour {

    public static ArrayList ordenarPuntajesfb (ArrayList puntajesfb)
    {
        bool ordenado = true;

        Debug.Log("CANT PUNTAJES: " + puntajesfb.Count);
        PuntajeVO puntajefb_pivote = null;

        PuntajeVO puntajefb_aux = null;
        for (int i = 0; i < puntajesfb.Count; i++)
        {
            Debug.Log("QuickSort Ejecutandose");

            if (i == 0) puntajefb_pivote = (PuntajeVO)puntajesfb[i];
            else
            {
                PuntajeVO puntajefb = (PuntajeVO)puntajesfb[i];

                if (int.Parse(puntajefb.getTiempo()) > int.Parse(puntajefb_pivote.getTiempo()))
                {
                    puntajefb_pivote = (PuntajeVO)puntajesfb[i];
                }
                else
                {
                    ordenado = false;
                    puntajefb_aux = puntajefb;

                    puntajesfb[i] = puntajefb_pivote;
                    puntajesfb[i - 1] = puntajefb_aux;
                }
            }

        }

        Debug.Log("DESPUES DE ORDENAR: ");
        string lista = "";
        for (int i = 0; i < puntajesfb.Count; i++)
        {
            PuntajeVO puntaje_ = (PuntajeVO)puntajesfb[i];
            lista += " (" + puntaje_.getPosicion() + ") " + int.Parse(puntaje_.getTiempo().Replace(":", ""));
        }

        Debug.Log("L:" + lista);

        if (ordenado == true)
        {
            return puntajesfb;
        }
        else
        {
            return ordenarPuntajesfb(puntajesfb);
        }



    }
}
