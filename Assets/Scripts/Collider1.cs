using UnityEngine;
using System.Collections;

public class Collider1 : MonoBehaviour
{
    public Transform target;
    private Car_Controller car_controller;
    public GameObject bandera;
    // Use this for initialization
    void Start()
    {

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

    void OnTriggerEnter(Collider other)
    {
        Destroy(bandera);

        


    }
}
    




