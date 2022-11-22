using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 8.0f;

    public float lifeTime = 10;

    float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {

        transform.position += speed * transform.forward * Time.deltaTime;

        if (startTime + lifeTime < Time.time)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {

    }
}
