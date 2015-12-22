using UnityEngine;
using System.Collections;
using Facebook.Unity;

public class controladorPrincipalInv2: MonoBehaviour {
	private bool FinishedAndTested = false;
	public Transform target;
	private Car_Controller car_controller;
	CardboardHead head = null;
    
    void Start () {
		//carinput=GetComponent< IRDSCarControllerAI > ();
		//car = IRDSStatistics.GetCurrentCar();
		head = Camera.main.GetComponent<StereoController>().Head;
		
	}

    // Update is called once per frame
    void Update() {

		


		//Debug.Log ("X HEAD: "+head.transform.eulerAngles.x);
		//Debug.Log ("Y HEAD: "+head.transform.eulerAngles.y);
		//Debug.Log ("Z HEAD: "+head.transform.eulerAngles.z);
		if (!target && GameObject.FindGameObjectWithTag ("Player")) {
			//Debug.Log("seteando player");
			target = GameObject.FindGameObjectWithTag ("Player").transform;
			car_controller = target.GetComponent<Car_Controller> ();

		}

		if (target && GameObject.FindGameObjectWithTag ("Player")) {


			/**
             *    ALGORITMO PARA DOBLAR CON LA CABEZA 
             */
			if (head.transform.eulerAngles.z > 5 && head.transform.eulerAngles.z < 90) {

				//formula= 90=1
				//            angulo=x
				// angulo/90

				car_controller.steerInput = Mathf.Clamp (-(head.transform.eulerAngles.z / 90), -1, 1);
				//Debug.Log("HEAD doblando izquierda");
			} else if (head.transform.eulerAngles.z < 359 && head.transform.eulerAngles.z > 270) {

				car_controller.steerInput = Mathf.Clamp ((head.transform.eulerAngles.z - 360) / (-90), -1, 1);
				//Debug.Log("HEAD doblando derecha");
			} else {
				car_controller.steerInput = Mathf.Clamp (0, -1, 1);
				//Debug.Log("HEAD centrando");
			}

			/**
             *    ALGORITMO PARA ACELERAR O RETROCEDER CON LA CABEZA 
             */

			if ((head.transform.eulerAngles.x < 360 && head.transform.eulerAngles.x > 160) ||
				(head.transform.eulerAngles.x >= 0 && head.transform.eulerAngles.x < 20)) {
				//retrocede
				//Debug.Log("HEAD acelerando");
				car_controller.motorInput = Mathf.Clamp01 (1);
				car_controller.brakeInput = Mathf.Clamp01 (0);
			} else if (head.transform.eulerAngles.x >= 20 && head.transform.eulerAngles.x < 150) {
				//acelera
				//Debug.Log("HEAD retrocediendo");
				car_controller.motorInput = Mathf.Clamp01 (0);
				car_controller.brakeInput = Mathf.Clamp01 (1);
			} else {
				car_controller.motorInput = Mathf.Clamp01 (0);
				car_controller.brakeInput = Mathf.Clamp01 (0);
			}
            if (Input.GetKey(KeyCode.Return) && RaceManager.instance)
            {
                if (RaceManager.instance.raceStarted)
                    car_controller.Respawn();
            }
        } 
	
	}
			
			
    

}
	 
	
