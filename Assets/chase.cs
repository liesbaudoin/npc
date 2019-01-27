using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class chase : MonoBehaviour
{
    public Transform player;
    Animator anim; //reference to animator component
    NavMeshAgent agent; //reference to navmeshagent
    string state = "patrol";
    public float aggroRange = 10;// afstand tussen npc en fps wanneer actie moet komen
    public float patroltime = 15;// tijd in seconden tot de npc een nieuwe destination gaat zoeken
    public GameObject[] waypoints;

    float speed = 1.5f;

    int index; //current waypoint index in waypoints array
    // Start is called before the first frame update

    void Start()
    {
        anim = GetComponent<Animator>();//altijd nodig, anders is anim leeg
        agent = GetComponent<NavMeshAgent>();
        waypoints = GameObject.FindGameObjectsWithTag("waypoint"); //zet de waypoints in de array

        index = Random.Range(0, waypoints.Length);

        InvokeRepeating("Tick", 0, 0.5f);//roep de methode Tick aan vanaf tijd is 0 elke 0,5 seconde
        if (waypoints.Length > 0)
        {
            InvokeRepeating("Patrol", Random.Range(0, patroltime), patroltime);//roep methode patrol aan vanaf een random tijd elke patroltijd(zodat verschillende npc niet op hetzelfde moment van richting verandert
        }
    }


    // Update is called once per frame
    void Update()

    {
        anim.SetBool("isWalking", agent.velocity.magnitude > 0.1f);//als er beweging is, wordt de animatie walk geactiveerd

    }

    //methode om de huidige index te veranderen
    void Patrol()
    {
        index = index == waypoints.Length - 1 ? 0 : index + 1 ;// andere manier voor if statement als eerste condition true is maak je index 0 anders tel je er 1 bij op

    }

    //methode om de npc te sturen naar de player moet of naar een waypoint 
    void Tick()
    {
        if (Vector3.Distance(transform.position, player.position) < aggroRange)
        {
            agent.destination = player.position;
        }
        else
        {
            agent.destination = waypoints[index].transform.position;//vraag de positie op van het waypoint inde transform van de sphere
        }
    }
}
