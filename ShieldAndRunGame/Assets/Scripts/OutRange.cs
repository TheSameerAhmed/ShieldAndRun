using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutRange : MonoBehaviour
{

    [SerializeField] Shooting shooter;
    [SerializeField] Text displayTarget;
    [SerializeField] GameTimeManager gameTimeManager;

    
    float fixedDelta;
    bool inSlowTimePowerUp;

    void Awake()
    {
        fixedDelta = Time.fixedDeltaTime;
        inSlowTimePowerUp = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") && inSlowTimePowerUp == false)
        {
            
            displayTarget.text = "";

            GetComponent<BoxCollider>().enabled = false;
            shooter.StopShooting();
        }
    }



}
