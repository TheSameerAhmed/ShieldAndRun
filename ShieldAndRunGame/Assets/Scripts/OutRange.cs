using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutRange : MonoBehaviour
{

    [SerializeField] Shooting shooter;
    [SerializeField] Text displayTarget;
    float fixedDelta;
    void Awake()
    {
        fixedDelta = Time.fixedDeltaTime;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = fixedDelta * Time.timeScale;
            displayTarget.text = "";

            GetComponent<BoxCollider>().enabled = false;
            shooter.StopShooting();
        }
    }



}
