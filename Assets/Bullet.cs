using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    float moveSpeed = 7f;

    Rigidbody rb;

    PLAYER target;
    Vector3 moveDirection;
    public float spawnTime = 30;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);

    }

    void Spawn()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindObjectOfType<PLAYER>();
        moveDirection = (target.transform.position - target.transform.position).normalized * moveSpeed;
        rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);
        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.Equals(target))
        {
            Debug.Log("Hit!");
        }
        Destroy(target);
    }
}

