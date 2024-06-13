using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ProfController : MonoBehaviour
{
    //conditions to fix prof movement
    public Transform[] waypoints;
    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;
    private bool moveToNextWaypoint = false;
    private float distanceThreshold = 0.2f;

    //conditions to trigger movement
    public GameObject block;
    public GameObject wall;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToNextWaypoint();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < distanceThreshold && moveToNextWaypoint) {
            moveToNextWaypoint = false;
            MoveToNextWaypoint();
        }
        CheckConditions();

    }

    void MoveToNextWaypoint() {
        if (waypoints.Length == 0) {
            return;
        }
        agent.destination = waypoints[currentWaypointIndex].position;
        currentWaypointIndex++;
        
        if (currentWaypointIndex >= waypoints.Length) {
            moveToNextWaypoint = false;
        }
    }
    
    void CheckConditions() {
        //first condition: check that both players move left or right
        if (currentWaypointIndex == 0 && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))) {
            moveToNextWaypoint = true;
        }
        //second condition: check that both players jump
        else if (currentWaypointIndex == 1 && (Input.GetKeyDown(KeyCode.W)) && Input.GetKeyDown(KeyCode.UpArrow)) {
            moveToNextWaypoint = true;
        }
        //third condition: check that jock breaks block
        else if(currentWaypointIndex == 2 && block.gameObject == null) {
            moveToNextWaypoint = true;
        }
        //fourth condition: check that nerd breaks wall
        else if (currentWaypointIndex == 3 && wall.gameObject == null) {
            moveToNextWaypoint = true;
        }
        //fifth and sixth condition: any key press
        else if ((currentWaypointIndex == 4 || currentWaypointIndex == 5) && Input.anyKey) {
            moveToNextWaypoint = true;
        }
        

    }
}
