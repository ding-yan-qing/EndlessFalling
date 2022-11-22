using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject prefabBullet;


    public float pistolFireCD = 0.2f;
    public float shotgunFireCD = 0.5f;
    public float rifleCD = 0.1f;


    float lastFireTime;

    //public Animator anim;


    public int curGun { get; private set; }  


    public void Fire(bool keyDown, bool keyPressed){

        switch (curGun)
        {
            case 0:
                if (keyDown){
                    //anim.SetTrigger("Trigger2");
                    PistolFire();
                }
                break;
            case 1:
                if (keyDown){
                    //anim.SetTrigger("Trigger2");
                    shotgunFire();
                }
                break;
            case 2:
                if (keyPressed){
                    //anim.SetTrigger("Trigger2");
                    RifleFire();
                }
                break;
        }
    }


    public int Change(){
        curGun += 1;
        if (curGun == 3)
        {
            curGun = 0;
        }
        return curGun;
    }


    public void PistolFire(){
        if (lastFireTime + pistolFireCD > Time.time)
        {
            return;
        }
        lastFireTime = Time.time;
        GameObject bullet = Instantiate(prefabBullet, null);
        bullet.transform.position = transform.position + transform.forward * 1.0f;
        bullet.transform.forward = transform.forward;
    }


    public void RifleFire(){
        if (lastFireTime + rifleCD > Time.time){
            return;
        }
        lastFireTime = Time.time;
        GameObject bullet = Instantiate(prefabBullet, null);
        bullet.transform.position = transform.position + transform.forward * 1.0f;
        bullet.transform.forward = transform.forward;
    }


    public void shotgunFire(){
        if (lastFireTime + shotgunFireCD > Time.time){
            return;
        }
        lastFireTime = Time.time;


        for (int i=-2; i<=2; i++){
            GameObject bullet = Instantiate(prefabBullet, null);
            Vector3 dir = Quaternion.Euler(0, i * 10, 0) * transform.forward;

            bullet.transform.position = transform.position + dir * 1.0f;
            bullet.transform.forward = dir;


            Bullet b = bullet.GetComponent<Bullet>();
            b.lifeTime = 0.3f;
        }
    }
}
