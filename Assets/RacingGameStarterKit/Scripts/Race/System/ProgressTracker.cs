using UnityEngine;
using System;
using System.Collections;

    public class ProgressTracker : MonoBehaviour
    {
        [HideInInspector][SerializeField]private WaypointCircuit circuit; // A reference to the waypoint-based route we should follow
        [HideInInspector][SerializeField]private float lookAheadForTargetOffset = 20f;
        [HideInInspector][SerializeField]private float lookAheadForTargetFactor = 0.1f;
    	[HideInInspector][SerializeField]private float lookAheadForSpeedOffset = 20;
    	[HideInInspector][SerializeField]private float lookAheadForSpeedFactor = 0.5f;
        [HideInInspector]public Transform target;
        [HideInInspector]public float progressDistance;
        public float raceCompletion;
        private Vector3 lastPosition; // Used to calculate current speed (since we may not have a rigidbody component)
        private float speed; // current speed of this object (calculated from delta since last frame)
		public WaypointCircuit.RoutePoint targetPoint { get; private set; }
        public WaypointCircuit.RoutePoint speedPoint { get; private set; }
        public WaypointCircuit.RoutePoint progressPoint { get; private set; }
        
        
        private void Awake(){
        	if(!RaceManager.instance)
    			return;
        
        	target = new GameObject("New Progress Tracker").transform;
    		circuit = GameObject.FindObjectOfType(typeof(WaypointCircuit)) as WaypointCircuit;
        	GetComponent<Statistics>().target = target;
        	progressDistance = -Vector3.Distance(transform.position,GetComponent<Statistics>().path[0].position);
        }
        
        private void Start(){
        	target.name = name + "_ProgressTracker";
        }
        
        private void Update(){

                if (Time.deltaTime > 0){
                    speed = Mathf.Lerp(speed, (lastPosition - transform.position).magnitude/Time.deltaTime,Time.deltaTime);
                }
                
                target.position = circuit.GetRoutePoint(progressDistance + lookAheadForTargetOffset + lookAheadForTargetFactor*speed).position;
               
                target.rotation = Quaternion.LookRotation(circuit.GetRoutePoint(progressDistance + lookAheadForSpeedOffset + lookAheadForSpeedFactor*speed).direction);

                // get our current progress along the route
                progressPoint = circuit.GetRoutePoint(progressDistance);
                
                Vector3 progressDelta = progressPoint.position - transform.position;
               
                if (Vector3.Dot(progressDelta, progressPoint.direction) < 0){
                    progressDistance += progressDelta.magnitude*0.5f;
                }
               
            	if(Vector3.Dot(progressDelta, progressPoint.direction) > 1){
            		progressDistance -= progressDelta.magnitude*0.5f;
            	}

                lastPosition = transform.position;
                
                raceCompletion = ((progressDistance / RaceManager.instance.raceDistance) * 100) / RaceManager.instance.totalLaps;
                raceCompletion = Mathf.Clamp(raceCompletion, -Mathf.Infinity , 100);
                raceCompletion = Mathf.Round(raceCompletion * 100) / 100;
        }

        private void OnDestroy(){
        	if(target)
    			Destroy(target.gameObject);
        }
        
        #if UNITY_EDITOR
        private void OnDrawGizmos(){
            if (Application.isPlaying && circuit != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, target.position);
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(target.position, target.position + target.forward);
            }
        }
		#endif
    }
