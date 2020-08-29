using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRange : MonoBehaviour
{

    [SerializeField] Shooting shooter;
    [SerializeField] GameTimeManager gameTimeManager;
    [SerializeField] CoinManager coinManager;
    

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") && coinManager.inSlowTimePower == false)
        {
            gameTimeManager.SlowTimeDown(0.2f);

            GetComponent<BoxCollider>().enabled = false;

            StartCoroutine(shooter.Shoot());
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(1f);
    }
}
