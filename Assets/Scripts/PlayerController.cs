using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cam;

    private Transform m_transform;
    private Rigidbody rig;
    private Animator anim;

    private Vector3 targetPos;
    private Vector3 movePos;
    private Vector3 camPos;
    
    public float moveSpeed=10f;
    private float blend;

    public float jumpForce = 100;

    private bool canJump = true;

    public float maxHP = 999.0f;

    bool dead =false;

    float hp;



    Weapon weapon;

    private void Start()
    {
        m_transform = this.transform;
        rig = this.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        hp = maxHP;
        //Transform gun = transform.Find("Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/Gun");
        //Debug.Log("get"+gun.gameObject.name);
        weapon = GetComponent<Weapon>();
    }
    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        anim.SetFloat("Blend", blend*3);

        if(!dead){
            Move();
        }

        /*if (Input.GetKey(KeyCode.W))
        {
            blend = Mathf.Lerp(blend, 0.5f, 0.02f);
            targetPos.z = 1;           
            Vector3 targetDir = Vector3.Slerp(m_transform.forward, Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up), 0.02f);
            m_transform.rotation = Quaternion.LookRotation(targetDir, Vector3.up);
        }
        if (Input.GetKey(KeyCode.S))
        {
            blend = Mathf.Lerp(blend, 0.5f, 0.02f);
            targetPos.z = 1;           
            Vector3 targetDir = Vector3.Slerp(m_transform.forward, -Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up), 0.02f);
            m_transform.rotation = Quaternion.LookRotation(targetDir,Vector3.up);
        }
        if (Input.GetKey(KeyCode.A))
        {
            blend = Mathf.Lerp(blend, 0.5f, 0.02f);
            targetPos.z = 1;
           
            Vector3 targetDir = Vector3.Slerp(m_transform.forward, -Vector3.ProjectOnPlane(cam.transform.right, Vector3.up), 0.02f);
            m_transform.rotation = Quaternion.LookRotation(targetDir, Vector3.up);
        }
        if (Input.GetKey(KeyCode.D))
        {
            blend = Mathf.Lerp(blend, 0.5f, 0.02f);
            targetPos.z = 1;
            Vector3 targetDir = Vector3.Slerp(m_transform.forward, Vector3.ProjectOnPlane(cam.transform.right, Vector3.up), 0.02f);
            m_transform.rotation = Quaternion.LookRotation(targetDir, Vector3.up);
        }
        if (!Input.GetKey(KeyCode.W)&&!Input.GetKey(KeyCode.S)&& !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            targetPos.z = 0;
            blend = Mathf.Lerp(blend, 0f, 0.02f);
        }

        movePos = targetPos * Time.deltaTime * moveSpeed* blend;
        transform.Translate(movePos);

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            anim.SetTrigger("jump");
            this.rig.AddForce(Vector3.up * jumpForce);
            //canJump = false;
        }*/

        bool fireKeyDown = Input.GetKeyDown(KeyCode.J);
        bool fireKeyPressed = Input.GetKey(KeyCode.J);
        bool changeWeapon = Input.GetKeyDown(KeyCode.Q);

       /* if (Input.GetKeyDown(KeyCode.J) && Input.GetKey(KeyCode.J))
        {
            anim.SetTrigger("Trigger1");
        }*/

        //StartCoroutine(Magic(fireKeyDown,fireKeyPressed));

        weapon.Fire(fireKeyDown, fireKeyPressed);

        if (changeWeapon){
            ChangeWeapon();
        }
    }

    public void Move(){
        if (Input.GetKey(KeyCode.W))
        {
            blend = Mathf.Lerp(blend, 0.5f, 0.02f);
            targetPos.z = 1;           
            Vector3 targetDir = Vector3.Slerp(m_transform.forward, Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up), 0.02f);
            m_transform.rotation = Quaternion.LookRotation(targetDir, Vector3.up);
        }
        if (Input.GetKey(KeyCode.S))
        {
            blend = Mathf.Lerp(blend, 0.5f, 0.02f);
            targetPos.z = 1;           
            Vector3 targetDir = Vector3.Slerp(m_transform.forward, -Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up), 0.02f);
            m_transform.rotation = Quaternion.LookRotation(targetDir,Vector3.up);
        }
        if (Input.GetKey(KeyCode.A))
        {
            blend = Mathf.Lerp(blend, 0.5f, 0.02f);
            targetPos.z = 1;
           
            Vector3 targetDir = Vector3.Slerp(m_transform.forward, -Vector3.ProjectOnPlane(cam.transform.right, Vector3.up), 0.02f);
            m_transform.rotation = Quaternion.LookRotation(targetDir, Vector3.up);
        }
        if (Input.GetKey(KeyCode.D))
        {
            blend = Mathf.Lerp(blend, 0.5f, 0.02f);
            targetPos.z = 1;
            Vector3 targetDir = Vector3.Slerp(m_transform.forward, Vector3.ProjectOnPlane(cam.transform.right, Vector3.up), 0.02f);
            m_transform.rotation = Quaternion.LookRotation(targetDir, Vector3.up);
        }
        if (!Input.GetKey(KeyCode.W)&&!Input.GetKey(KeyCode.S)&& !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            targetPos.z = 0;
            blend = Mathf.Lerp(blend, 0f, 0.02f);
        }

        movePos = targetPos * Time.deltaTime * moveSpeed* blend;
        transform.Translate(movePos);

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            anim.SetTrigger("jump");
            this.rig.AddForce(Vector3.up * jumpForce);
            //canJump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    private void ChangeWeapon()
    {
        int w = weapon.Change();
    }

    /*iIEnumerator Magic(bool fireKeyDown, bool fireKeyPressed){
        f(fireKeyDown==false || fireKeyPressed ==false){
            yield break;
        }
        yield return new WaitForSeconds(0.5f);
        weapon.Fire(fireKeyDown, fireKeyPressed);
    }*/

    /*void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnemyBullet")){
            //if(hp <= 0){return;}
            Destroy(other.gameObject);
            hp--;
            if(hp <= 0){
                dead = true;
            }

            //加特效或者声音
        }
    }*/

}