using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAI : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float patrolWaitTime =1f;
    public Transform patrolWayPoints;
    private UnityEngine.AI.NavMeshAgent agent;
    private float patrolTimer;
    private int wayPointIndex;

    BoxCollider col;
    public AudioSource music;

    //public float shootRotSpeed = 10f;
    //public float shootFreeTime = 0.2f;
    //private float shootTimer = 0f;
    //private EnemySight enemySight;
    //public Rigidbody bullet;
    private Transform player;


    //private bool chase;
    //public float chaseSpeed = 5f;
    //public float chaseWaitTime = 1f;
    //private float chaseTimer;
    //public float sqrPlayerDist = 3f;

    //private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //Transform sight = transform.Find("Sight");
        //enemySight = sight.GetComponent<EnemySight>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //anim = GetComponent<Animator>();
        //Transform gun = transform.Find("Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_R_Shoulder/J_Bip_R_UpperArm/J_Bip_R_LowerArm/J_Bip_R_Hand/Gun");
        //Transform face = transform.Find("Face");
        col = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Patrolling();
        //anim.SetTrigger("Chase");
    }

    void Patrolling(){
        agent.isStopped =false;
        agent.speed = patrolSpeed;
        if(agent.remainingDistance <= agent.stoppingDistance){
            patrolTimer += Time.deltaTime;

            if(patrolTimer >= patrolWaitTime){
                if(wayPointIndex == patrolWayPoints.childCount -1 ){
                    wayPointIndex = 0;
                }else
                {
                    wayPointIndex++;
                }
                patrolTimer = 0;
            }
        }
        else{
                patrolTimer = 0;
        }
        agent.destination = patrolWayPoints.GetChild(wayPointIndex).position;
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject == player){
        music.Play();
    }
    }
}
