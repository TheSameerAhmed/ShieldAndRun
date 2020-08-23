using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody bullet;
    [SerializeField] float destroyLimit = -20.0f;

    void Start()
    {
        bullet.velocity = (1.0f * transform.forward) * speed;
    }

    void Update()
    {
        if (transform.position.x < destroyLimit)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.name);
        if (collider.name == "Cylinder" || collider.name == "Track")
            return;        
        Destroy(gameObject);
    }
}
