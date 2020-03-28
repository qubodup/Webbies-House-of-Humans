using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class HumanAI : MonoBehaviour
{
    // drag human scripts here...
    public GameObject[] humans;

    // waypoint parents
    public GameObject waypointParent;
    // waypoint array
    private Transform[] waypoints;

    // how close do I have to be to target to reach it?
    public float waypointDistance = 2f;
    private bool targetIsPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        // set up waypoints
        waypoints = waypointParent.transform.GetComponentsInChildren<Transform>();

        foreach (GameObject human in humans)
        {
            SetRandomTarget(human.GetComponent<AICharacterControl>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject human in humans)
        {
            // when targeting waypoint and close enough
            if (
                !targetIsPlayer &&
                Vector3.Distance(
                    human.transform.position,
                    human.GetComponent<AICharacterControl>().target.transform.position)
                < waypointDistance
                )
            {
                // get new target
                SetRandomTarget(human.GetComponent<AICharacterControl>());
            }
        }
    }

    void SetRandomTarget(AICharacterControl aiScript)
    {
        aiScript.target = waypoints[Random.Range(0, waypoints.Length)].transform;
    }

}
