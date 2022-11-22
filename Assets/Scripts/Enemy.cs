using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject prefabBoomEffect;

    public float speed = 2;
    public float fireTime = 0.1f;
    public float maxHp = 1;

    Vector3 input;

    Transform player;
    float hp;
    bool dead = false;

    Weapon weapon;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Transform gun = transform.Find("Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_R_Shoulder/J_Bip_R_UpperArm/J_Bip_R_LowerArm/J_Bip_R_Hand/Gun");
        Debug.Log("get"+gun.gameObject.name);
        weapon = gun.GetComponent<Weapon>();
    }

    void Update(){
        Move();
        Fire();
    }

    void Move(){
        input = player.position - transform.position;
        input = input.normalized;
        transform.position += input * speed * Time.deltaTime;
        if (input.magnitude > 0.1f)
        {
            transform.forward = input;
        }
    }
    void Fire(){
        weapon.Fire(true, true);
    }


    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("PlayerBullet")){
            Destroy(other.gameObject);
            hp--;
            if (hp <= 0){
               dead = true;
               Destroy(gameObject);
            }
        }
    }
}

