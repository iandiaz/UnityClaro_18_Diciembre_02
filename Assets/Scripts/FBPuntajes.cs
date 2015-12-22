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
   
    private List<object> scorelist = null;
    private static object score;

    // Use this for initialization
    void Start()
    {
        
        queryscore();
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
        string scorepuntos;
        string scoredebug;
        string scoretiempo;

        FB.API("/1617825028466938/scores", HttpMethod.GET, delegate (IGraphResult result)
        {
            

            scorelist = Util.DeserializeScores(result.RawResult);
            int contador = 1;
            foreach (object score in scorelist)
            {
                var entry = (Dictionary<string, object>)score;
                var user = (Dictionary<string, object>)entry["user"];

                    if (contador == 1)
                    {

                        Nombre.text = user["name"].ToString();

                        scorepuntos = entry["score"].ToString();
                        scoredebug = scorepuntos.PadLeft(6, '0');
                        scoretiempo = scoredebug.Insert(2, ":");
                        scores.text = scoretiempo.Insert(5, ":");


                    }

                    if (contador == 2)
                    {

                        Nombre2.text = user["name"].ToString();

                        scorepuntos = entry["score"].ToString();
                        scoredebug = scorepuntos.PadLeft(6, '0');
                        scoretiempo = scoredebug.Insert(2, ":");
                        scores2.text = scoretiempo.Insert(5, ":");


                    }

                    if (contador == 3)
                    {

                        Nombre3.text = user["name"].ToString();

                        scorepuntos = entry["score"].ToString();
                        scoredebug = scorepuntos.PadLeft(6, '0');
                        scoretiempo = scoredebug.Insert(2, ":");
                        scores3.text = scoretiempo.Insert(5, ":");


                    }
                    if (contador == 4)
                    {

                        Nombre4.text = user["name"].ToString();

                        scorepuntos = entry["score"].ToString();
                        scoredebug = scorepuntos.PadLeft(6, '0');
                        scoretiempo = scoredebug.Insert(2, ":");
                        scores4.text = scoretiempo.Insert(5, ":");


                    }

                    if (contador == 5)
                    {

                        Nombre5.text = user["name"].ToString();

                        scorepuntos = entry["score"].ToString();
                        scoredebug = scorepuntos.PadLeft(6, '0');
                        scoretiempo = scoredebug.Insert(2, ":");
                        scores5.text = scoretiempo.Insert(5, ":");


                    }

                    if (contador == 6)
                    {

                        Nombre6.text = user["name"].ToString();

                        scorepuntos = entry["score"].ToString();
                        scoredebug = scorepuntos.PadLeft(6, '0');
                        scoretiempo = scoredebug.Insert(2, ":");
                        scores6.text = scoretiempo.Insert(5, ":");


                    }
                    if (contador == 7)
                    {

                        Nombre7.text = user["name"].ToString();

                        scorepuntos = entry["score"].ToString();
                        scoredebug = scorepuntos.PadLeft(6, '0');
                        scoretiempo = scoredebug.Insert(2, ":");
                        scores7.text = scoretiempo.Insert(5, ":");


                    }

                    if (contador == 8)
                    {

                        Nombre8.text = user["name"].ToString();

                        scorepuntos = entry["score"].ToString();
                        scoredebug = scorepuntos.PadLeft(6, '0');
                        scoretiempo = scoredebug.Insert(2, ":");
                        scores8.text = scoretiempo.Insert(5, ":");


                    }

                    if (contador == 9)
                    {

                        Nombre9.text = user["name"].ToString();

                        scorepuntos = entry["score"].ToString();
                        scoredebug = scorepuntos.PadLeft(6, '0');
                        scoretiempo = scoredebug.Insert(2, ":");
                        scores9.text = scoretiempo.Insert(5, ":");


                    }
                    if (contador == 10)
                    {

                        Nombre10.text = user["name"].ToString();

                        scorepuntos = entry["score"].ToString();
                        scoredebug = scorepuntos.PadLeft(6, '0');
                        scoretiempo = scoredebug.Insert(2, ":");
                        scores10.text = scoretiempo.Insert(5, ":");


                    }

                    if (contador == 11)
                    {

                        Nombre11.text = user["name"].ToString();

                        scorepuntos = entry["score"].ToString();
                        scoredebug = scorepuntos.PadLeft(6, '0');
                        scoretiempo = scoredebug.Insert(2, ":");
                        scores11.text = scoretiempo.Insert(5, ":");


                    }

                    if (contador == 12)
                    {

                        Nombre12.text = user["name"].ToString();

                        scorepuntos = entry["score"].ToString();
                        scoredebug = scorepuntos.PadLeft(6, '0');
                        scoretiempo = scoredebug.Insert(2, ":");
                        scores12.text = scoretiempo.Insert(5, ":");


                    }
                    if (contador == 13)
                    {

                        Nombre13.text = user["name"].ToString();

                        scorepuntos = entry["score"].ToString();
                        scoredebug = scorepuntos.PadLeft(6, '0');
                        scoretiempo = scoredebug.Insert(2, ":");
                        scores13.text = scoretiempo.Insert(5, ":");


                    }

                    if (contador == 14)
                    {

                        Nombre14.text = user["name"].ToString();

                        scorepuntos = entry["score"].ToString();
                        scoredebug = scorepuntos.PadLeft(6, '0');
                        scoretiempo = scoredebug.Insert(2, ":");
                        scores14.text = scoretiempo.Insert(5, ":");


                    }

                    if (contador == 15)
                    {

                        Nombre15.text = user["name"].ToString();

                        scorepuntos = entry["score"].ToString();
                        scoredebug = scorepuntos.PadLeft(6, '0');
                        scoretiempo = scoredebug.Insert(2, ":");
                        scores15.text = scoretiempo.Insert(5, ":");


                    }
                    contador += 1; }
            
        }
        );
    }



}

         


