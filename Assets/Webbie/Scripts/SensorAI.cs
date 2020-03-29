using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class SensorAI : MonoBehaviour
{

    private AudioSource source;
    public AudioClip sndSpot;
    public AudioClip sndWarn;
    public AudioClip sndGiveup;
    private float spotTimer = 0;
    private float spotTimerLimit = 1;

    public float speedWalk = .3f;
    public float speedRun = .5f;

    private bool sensePlayer = false;
    private bool seePlayer = false;
    private bool seenPlayer = false;
    private float seeTimer = 0f;
    public float seeTimerLimit = 5f;
    private bool timerRunning = false;
    private bool warned = false;
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
        human.GetComponent<NavMeshAgent>().speed = speedWalk;

        source = human.GetComponent<AudioSource>();

        // set up waypoints
        waypoints = waypointParent.transform.GetComponentsInChildren<Transform>();

        // get random waypoint target
        SetRandomTarget();
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
            SetRandomTarget();
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
                seenPlayer = seePlayer;
                seePlayer = true;
                if (!(hit.collider.gameObject == human) && !(hit.collider.gameObject == player))
                {
                    seePlayer = false;
                }
                if (seePlayer)
                {
                    seeTimer = seeTimerLimit;
                    timerRunning = true;
                    warned = false;
                    if (!seenPlayer && spotTimer <= 0)
                    {
                        human.GetComponent<NavMeshAgent>().speed = speedRun;
                        SetPlayerTarget(human.GetComponent<AICharacterControl>());
                        source.PlayOneShot(sndSpot);
                        spotTimer = spotTimerLimit;
                    }
                }
            }
        }
        // follow countdown
        if (seeTimer > 0)
        {
            seeTimer -= Time.deltaTime;
            spotTimer -= Time.deltaTime;
            // warn sounds (indicator of enemy losing track)
            if (seeTimer < seeTimerLimit/2 && !warned)
            {
                source.PlayOneShot(sndWarn);
                warned = true;
            }
        // timer ran out
        } else if (timerRunning)
        {
            timerRunning = false;
            seePlayer = false;
            sensePlayer = false;
            human.GetComponent<NavMeshAgent>().speed = speedWalk;
            SetRandomTarget();
            source.PlayOneShot(sndGiveup);
        }
        // Game Over check
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
    void SetRandomTarget()
    {
        human.GetComponent<AICharacterControl>().target = waypoints[Random.Range(0, waypoints.Length)].transform;
    }
    void SetPlayerTarget(AICharacterControl aiScript)
    {
        aiScript.target = player.transform;
    }
}
