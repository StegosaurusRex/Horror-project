using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public enum AIStatsKind { idle,Active,Chaseing}

[RequireComponent(typeof(NavMeshAgent))]
  
public class DogExample : MonoBehaviour
{
    public Camera fpscamera;
    public float LookRadius = 10f;
    public float stopRadis = 3f;
    public float MaxSpeed = 3;
    public Transform target;
    private NavMeshAgent enemy;
    public Animator DogAni;
    private AIStatsKind _AIStats;
    public AudioSource awakeDogSound;
     public bool isChaseing;
    GameManager pause;
    private void OnDrawGizmos()
    {
 
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, LookRadius);
            Gizmos.color = Color.blue;
       
    }
    private void Awake()
    {
         
    }
    void Start()
    {

        pause = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        awakeDogSound = GetComponent<AudioSource>();
        enemy = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_AIStats == AIStatsKind.idle )
        {
            float distance = Vector3.Distance(target.position, transform.position);
            RaycastHit hit;
            Physics.Raycast(fpscamera.transform.position, fpscamera.transform.forward, out hit, 10000);
                if (distance <= LookRadius|| hit.transform.name == "zombieDog")
            {
                DogAni.SetBool("awake", true);
                Invoke("_SlowWalk",0.5f);
                _AIStats = AIStatsKind.Active;
                enemy.speed = 0.5f;
            }

        }

        if (_AIStats == AIStatsKind.Chaseing)
        {
            
            enemy.SetDestination(target.position);
            DogAni.SetFloat("speed", enemy.velocity.magnitude);
            Debug.Log(enemy.velocity.magnitude);
            if (true)
            {
                float distance = Vector3.Distance(target.position, transform.position);
                if (distance <=stopRadis)
                {
                    pause.PauseGameOver();

                }
            }
        }
    }
 
    void _SlowWalk()
    {

        enemy.SetDestination(target.position);
        DogAni.SetFloat("speed",0.5f);
        Invoke("_ChasePlayer", 3.0f);


    }

    void _ChasePlayer()
    {
        awakeDogSound.Play();
        DogAni.SetFloat("speed",1); 
        _AIStats = AIStatsKind.Chaseing;
        enemy.speed = MaxSpeed;

    }
}
