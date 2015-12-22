using UnityEngine;
using System.Collections;

public class ColliderCoints : MonoBehaviour {
    public Transform target;
    private Car_Controller car_controller;
	public GameObject coin;
	public AudioClip turbo;
    GameObject Player;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
        if (!target && GameObject.FindGameObjectWithTag("Player"))
        {
            //Debug.Log("seteando player");
            target = GameObject.FindGameObjectWithTag("Player").transform;
            car_controller = target.GetComponent<Car_Controller>();

        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
		
		if (other.tag == "Player")
        {
            
            
            GetComponent<AudioSource>().PlayOneShot(turbo);
            car_controller.boost = 100000f;// buscar funcion para activar turbo
            car_controller.topSpeed = 300;
            car_controller.engineTorque = 1550;
            InvokeRepeating("LaunchProjectile", 1, 0); // en segundos */

        }
        else { }
    }
        
    
    
	private void LaunchProjectile()
	{
		
        car_controller.boost = 100f ;// buscar funcion para activar turbo
        car_controller.topSpeed = 100;
        car_controller.engineTorque = 800;
		Destroy(coin);
    }


}

