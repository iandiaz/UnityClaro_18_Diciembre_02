using UnityEngine;
using System.Collections;
using Facebook.Unity;
using UnityEngine.UI;
using System.Collections.Generic;

public class FBPuntajes : MonoBehaviour {

    public Text Nombre;
    public Text scores;
    public Text Nombre2;
    public Text scores2;
    public Text Nombre3;
    public Text scores3;
    public Text Nombre4;
    public Text scores4;
    public Text Nombre5;
    public Text scores5;
    public Text Nombre6;
    public Text scores6;
    public Text Nombre7;
    public Text scores7;
    public Text Nombre8;
    public Text scores8;
    public Text Nombre9;
    public Text scores9;
    public Text Nombre10;
    public Text scores10;
    public Text Nombre11;
    public Text scores11;
    public Text Nombre12;
    public Text scores12;
    public Text Nombre13;
    public Text scores13;
    public Text Nombre14;
    public Text scores14;
    public Text Nombre15;
    public Text scores15;
    public Text Rank;
    public Text Rank2;
    public Text Rank3;
    public Text Rank4;
    public Text Rank5;
    public Text Rank6;
    public Text Rank7;
    public Text Rank8;
    public Text Rank9;
    public Text Rank10;
    public Text Rank11;
    public Text Rank12;
    public Text Rank13;
    public Text Rank14;
    public Text Rank15;

    public List<object> scorelist = null;
    private static object score;

    // Use this for initialization
    void Start()
    {
        queryscore();
        InvokeRepeating("LaunchProjectile", 1, 0);



    }

    // Update is called once per frame
    void Update()
    {

    }


    public static void setscore(string mipuntaje)
    {

        var scoredata = new Dictionary<string, string>();
        scoredata["score"] = mipuntaje;
        
        FB.API("/me/scores", HttpMethod.POST, delegate (IGraphResult result)
        {
            Debug.Log("setscore" + result.RawResult);

        }, scoredata
);
    }


    public void queryscore()
    {
        

        FB.API("/1617825028466938/scores", HttpMethod.GET, delegate (IGraphResult result)
        {


            scorelist = Util.DeserializeScores(result.RawResult);

            Constantes.listaPuntajesfb = new ArrayList();
            foreach (object score in scorelist)
            {
                var entry = (Dictionary<string, object>)score;
                var user = (Dictionary<string, object>)entry["user"];
                var puntos = entry["score"].ToString();
                var nombre = entry["user"].ToString();


                if (int.Parse(puntos) > 0)
                {
                    PuntajeVO puntajefb = new PuntajeVO();

                    puntajefb.setTiempo(puntos);
                    puntajefb.setNombreJugador(user["name"].ToString());

                    Constantes.listaPuntajesfb.Add(puntajefb);
                }
                }


            

            
        }
        
        
    
        );
    }


    private void LaunchProjectile()
    {
        Debug.Log("Lista");
        string scorepuntos;
        string scoredebug;
        string scoretiempo;
        Constantes.listaPuntajesfb = QuickSortPuntajesFB.ordenarPuntajesfb(Constantes.listaPuntajesfb);
        int contador = 1;
        foreach (PuntajeVO puntajefb in Constantes.listaPuntajesfb)
        {
           
                if (contador == 1)
                {
                    Rank.text = "1";
                    Nombre.text = puntajefb.getNombreJugador();

                    scorepuntos = puntajefb.getTiempo();
                    scoredebug = scorepuntos.PadLeft(6, '0');
                    scoretiempo = scoredebug.Insert(2, ":");
                    scores.text = scoretiempo.Insert(5, ":");


                }

                if (contador == 2)
                {
                    Rank2.text = "2";
                    Nombre2.text = puntajefb.getNombreJugador();

                    scorepuntos = puntajefb.getTiempo();
                    scoredebug = scorepuntos.PadLeft(6, '0');
                    scoretiempo = scoredebug.Insert(2, ":");
                    scores2.text = scoretiempo.Insert(5, ":");


                }

                if (contador == 3)
                {
                    Rank3.text = "3";
                    Nombre3.text = puntajefb.getNombreJugador();

                    scorepuntos = puntajefb.getTiempo();
                    scoredebug = scorepuntos.PadLeft(6, '0');
                    scoretiempo = scoredebug.Insert(2, ":");
                    scores3.text = scoretiempo.Insert(5, ":");


                }
                if (contador == 4)
                {
                    Rank4.text = "4";
                    Nombre4.text = puntajefb.getNombreJugador();

                    scorepuntos = puntajefb.getTiempo();
                    scoredebug = scorepuntos.PadLeft(6, '0');
                    scoretiempo = scoredebug.Insert(2, ":");
                    scores4.text = scoretiempo.Insert(5, ":");


                }

                if (contador == 5)
                {
                    Rank5.text = "5";
                    Nombre5.text = puntajefb.getNombreJugador();

                    scorepuntos = puntajefb.getTiempo();
                    scoredebug = scorepuntos.PadLeft(6, '0');
                    scoretiempo = scoredebug.Insert(2, ":");
                    scores5.text = scoretiempo.Insert(5, ":");


                }

                if (contador == 6)
                {
                    Rank6.text = "6";
                    Nombre6.text = puntajefb.getNombreJugador();

                    scorepuntos = puntajefb.getTiempo();
                    scoredebug = scorepuntos.PadLeft(6, '0');
                    scoretiempo = scoredebug.Insert(2, ":");
                    scores6.text = scoretiempo.Insert(5, ":");


                }
                if (contador == 7)
                {
                    Rank7.text = "7";
                    Nombre7.text = puntajefb.getNombreJugador();

                    scorepuntos = puntajefb.getTiempo();
                    scoredebug = scorepuntos.PadLeft(6, '0');
                    scoretiempo = scoredebug.Insert(2, ":");
                    scores7.text = scoretiempo.Insert(5, ":");


                }

                if (contador == 8)
                {
                    Rank8.text = "8";
                    Nombre8.text = puntajefb.getNombreJugador();

                    scorepuntos = puntajefb.getTiempo();
                    scoredebug = scorepuntos.PadLeft(6, '0');
                    scoretiempo = scoredebug.Insert(2, ":");
                    scores8.text = scoretiempo.Insert(5, ":");


                }

                if (contador == 9)
                {
                    Rank9.text = "9";
                    Nombre9.text = puntajefb.getNombreJugador();

                    scorepuntos = puntajefb.getTiempo();
                    scoredebug = scorepuntos.PadLeft(6, '0');
                    scoretiempo = scoredebug.Insert(2, ":");
                    scores9.text = scoretiempo.Insert(5, ":");


                }
                if (contador == 10)
                {
                    Rank10.text = "10";
                    Nombre10.text = puntajefb.getNombreJugador();

                    scorepuntos = puntajefb.getTiempo();
                    scoredebug = scorepuntos.PadLeft(6, '0');
                    scoretiempo = scoredebug.Insert(2, ":");
                    scores10.text = scoretiempo.Insert(5, ":");


                }

                if (contador == 11)
                {
                    Rank11.text = "11";
                    Nombre11.text = puntajefb.getNombreJugador();

                    scorepuntos = puntajefb.getTiempo();
                    scoredebug = scorepuntos.PadLeft(6, '0');
                    scoretiempo = scoredebug.Insert(2, ":");
                    scores11.text = scoretiempo.Insert(5, ":");


                }

                if (contador == 12)
                {
                    Rank12.text = "12";
                    Nombre12.text = puntajefb.getNombreJugador();

                    scorepuntos = puntajefb.getTiempo();
                    scoredebug = scorepuntos.PadLeft(6, '0');
                    scoretiempo = scoredebug.Insert(2, ":");
                    scores12.text = scoretiempo.Insert(5, ":");


                }
                if (contador == 13)
                {
                    Rank13.text = "13";
                    Nombre13.text = puntajefb.getNombreJugador();

                    scorepuntos = puntajefb.getTiempo();
                    scoredebug = scorepuntos.PadLeft(6, '0');
                    scoretiempo = scoredebug.Insert(2, ":");
                    scores13.text = scoretiempo.Insert(5, ":");


                }

                if (contador == 14)
                {
                    Rank14.text = "14";
                    Nombre14.text = puntajefb.getNombreJugador();

                    scorepuntos = puntajefb.getTiempo();
                    scoredebug = scorepuntos.PadLeft(6, '0');
                    scoretiempo = scoredebug.Insert(2, ":");
                    scores14.text = scoretiempo.Insert(5, ":");


                }

                if (contador == 15)
                {
                    Rank15.text = "15";
                    Nombre15.text = puntajefb.getNombreJugador();

                    scorepuntos = puntajefb.getTiempo();
                    scoredebug = scorepuntos.PadLeft(6, '0');
                    scoretiempo = scoredebug.Insert(2, ":");
                    scores15.text = scoretiempo.Insert(5, ":");
                }

                if (contador == 16) { break; }
                contador += 1;
            }

        }





    }


         


