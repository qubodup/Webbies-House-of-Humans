using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class SensorAI : MonoBehaviour
{

    public float speedWalk = .3f;
    public float speedRun = .5f;

    private bool sensePlayer = false;
    private bool seePlayer = false;
    private float seeTimer = 0f;
    public float seeTimerLimit = 5f;
    public float sightDistance = 5f;
    public GameObject player;

    // raycast
    private RaycastHit[] hits;

    // waypoint parents
    public GameObject waypointParent;
    // waypoint array
    private Transform[] waypoints;

    // human "parent" object
    private GameObject human;

    // how close do I have to be to target to reach it?
    public float waypointDistance = 2f;

    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        human = this.transform.parent.gameObject;

        // set up waypoints
        waypoints = waypointParent.transform.GetComponentsInChildren<Transform>();

        // get random waypoint target
        SetRandomTarget(human.GetComponent<AICharacterControl>());
    }

    // Update is called once per frame
    void Update()
    {
        // waypoint hunt
        // when targeting waypoint (not player) and close enough
        if (
            !seePlayer &&
            Vector3.Distance(
                human.transform.position,
                human.GetComponent<AICharacterControl>().target.transform.position)
            < waypointDistance
            )
        {
            // get new target
            SetRandomTarget(human.GetComponent<AICharacterControl>());
        }

        // player hunt
        if (sensePlayer)
        {
            hits = Physics.RaycastAll(
                human.transform.position + Vector3.up*.3f,
                player.transform.position - human.transform.position,
                Vector3.Distance(human.transform.position, player.transform.position)
                );
            Debug.DrawRay(human.transform.position + Vector3.up * .3f, player.transform.position - human.transform.position);

            foreach ( RaycastHit hit in hits)
            {
                Debug.Log(hit.collider.gameObject.name);
                seePlayer = true;
                if (!(hit.collider.gameObject == human) && !(hit.collider.gameObject == player))
                {
                    seePlayer = false;
                }
                if (seePlayer)
                {
                    SetPlayerTarget(human.GetComponent<AICharacterControl>());
                    seeTimer = seeTimerLimit;
                    human.GetComponent<NavMeshAgent>().speed = speedRun;
                }
                Debug.Log("seePlayer " + seePlayer);
            }
        }
        if (seeTimer > 0)
        {
            seeTimer -= Time.deltaTime;
        } else
        {
            seePlayer = false;
            human.GetComponent<NavMeshAgent>().speed = speedWalk;
        }
        //Debug.Log("seeplayer: " + seePlayer +" dist: " + Vector3.Distance(human.transform.position, player.transform.position));
        if (seePlayer && Vector3.Distance(human.transform.position,player.transform.position) < 1.5f)
        {
            gameManager.GetComponent<SpiderGameManager>().GameOver();
        }
    }

    // Spider AI Extention
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            sensePlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            sensePlayer = false;
        }
    }
    void SetRandomTarget(AICharacterControl aiScript)
    {
        aiScript.target = waypoints[Random.Range(0, waypoints.Length)].transform;
    }
    void SetPlayerTarget(AICharacterControl aiScript)
    {
        aiScript.target = player.transform;
    }
}
