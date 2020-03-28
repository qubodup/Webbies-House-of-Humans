using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class HumanAI : MonoBehaviour
{
    // waypoint array
    public GameObject[] targets;
    // drag human scripts here...
    public GameObject[] humans;
    
    // how close do I have to be to target to reach it?
    public float waypointDistance = 2f;
    private bool targetIsPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject human in humans)
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
        aiScript.target = targets[Random.Range(0, targets.Length - 1)].transform;
    }

}
