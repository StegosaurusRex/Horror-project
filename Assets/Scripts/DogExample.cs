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
    public AIStatsKind AIStats;
    public AudioSource awakeDogSound;
     public bool isChaseing;
     public static bool playerIsDead=false;
    GameManager pause;
    public AudioSource dogBark;
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
        playerIsDead = false;
        Target.isDead=false;
        pause = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        
        awakeDogSound = GetComponent<AudioSource>();
        enemy = GetComponent<NavMeshAgent>();
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        pause.PauseGameOver();
        

        // Code to execute after the delay
    }
    void Update()
    {
        
        
        if (AIStats == AIStatsKind.idle )
        {
            float distance = Vector3.Distance(target.position, transform.position);
            RaycastHit hit;
            Physics.Raycast(fpscamera.transform.position, fpscamera.transform.forward, out hit, 10000);
                if (distance <= LookRadius|| hit.transform.name == "zombieDog")
            {
                DogAni.SetBool("awake", true);
                Invoke("_SlowWalk",0.5f);
                AIStats = AIStatsKind.Active;
                enemy.speed = 0.5f;
            }

        }
        
        if (AIStats == AIStatsKind.Chaseing)
        {
            
            enemy.SetDestination(target.position);
            DogAni.SetFloat("speed", enemy.velocity.magnitude);
            Debug.Log(enemy.velocity.magnitude);
            if (true)
            {
                float distance = Vector3.Distance(target.position, transform.position);
                RaycastHit hit;
                Physics.Raycast(fpscamera.transform.position, fpscamera.transform.forward, out hit, 10000);
                if (distance <= stopRadis )
                {
                    playerIsDead = true;
                    
                    StartCoroutine(ExecuteAfterTime(1f));

                }
            }
        }
        if (Target.isDead==true)
        {
            Invoke("DeadDog", 0f);
            dogBark.Stop();
            Destroy(enemy);
            
        }
        if (GameManager.GameIsPaused == false)
        {
            if (AIStats == AIStatsKind.Chaseing && dogBark.isPlaying == false && Target.isDead == false)
            {
                dogBark.Play();
            }
            else if (AIStats != AIStatsKind.Chaseing && dogBark.isPlaying == true && Target.isDead == true)
            {
                dogBark.Stop();
            }
        }
        else if (GameManager.GameIsPaused==true)
        {
            dogBark.Stop();
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

        
        DogAni.SetFloat("speed",1); 
        AIStats = AIStatsKind.Chaseing;
        enemy.speed = MaxSpeed;

    }
    void DeadDog()
    {


        DogAni.SetFloat("speed", 0);
        DogAni.SetBool("isDead", true);
        dogBark.Stop();
        Destroy(enemy);


    }
}