using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float patrolWaitTime =1f;
    public Transform patrolWayPoints;
    private UnityEngine.AI.NavMeshAgent agent;
    private float patrolTimer;
    private int wayPointIndex;

    public float shootRotSpeed = 10f;
    public float shootFreeTime = 0.2f;
    private float shootTimer = 0f;
    private EnemySight enemySight;
    public Rigidbody bullet;
    private Transform player;


    private bool chase;
    public float chaseSpeed = 5f;
    public float chaseWaitTime = 1f;
    private float chaseTimer;
    public float sqrPlayerDist = 3f;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Transform sight = transform.Find("Sight");
        enemySight = sight.GetComponent<EnemySight>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        //Transform gun = transform.Find("Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_R_Shoulder/J_Bip_R_UpperArm/J_Bip_R_LowerArm/J_Bip_R_Hand/Gun");
        //Transform face = transform.Find("Face");
    }

    // Update is called once per frame
    void Update()
    {
        if(enemySight.playerInSight){
            Shooting();
            chase = true;
            anim.SetTrigger("Attack");
        }else if(chase)
        {
            Chasing();
            anim.SetTrigger("Chase");
        }else{
            Patrolling();
            anim.SetTrigger("Chase");
        }
    }

    void Shooting(){
         Transform gun = transform.Find("Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_R_Shoulder/J_Bip_R_UpperArm/J_Bip_R_LowerArm/J_Bip_R_Hand/Gun");
        Transform face = transform.Find("Face");
        Vector3 lookPos = player.position;
        lookPos.y = transform.position.y;
        Vector3 targetDir = lookPos - face.transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(targetDir),Mathf.Min(1,Time.deltaTime * shootRotSpeed));
        agent.isStopped = true;
        if(Vector3.Angle(face.transform.forward,targetDir)<10){
            if(shootTimer > shootFreeTime){
                Instantiate(bullet,gun.transform.position,Quaternion.LookRotation(player.position-face.transform.position));
                shootTimer = 0;
            }
            shootTimer += Time.deltaTime;
        }
    }

    void Chasing(){
        agent.isStopped = false;
        Vector3 sightingDeltaPos = enemySight.personalLastSight - transform.position;
        if(sightingDeltaPos.sqrMagnitude > sqrPlayerDist){
            agent.destination = enemySight.personalLastSight;
        }
        agent.speed = chaseSpeed;
        if(agent.remainingDistance <= agent.stoppingDistance){
            chaseTimer += Time.deltaTime;
            if(chaseTimer >= chaseWaitTime){
                chase = false;
                chaseTimer = 0;
            }
        }else{
            chaseTimer = 0;
        }
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
}
