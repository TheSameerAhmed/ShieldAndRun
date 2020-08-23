using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRange : MonoBehaviour
{

    [SerializeField] Shooting shooter;
    [SerializeField] int numberOfShots = 5;
    float fixedDelta;
    

    void Awake()
    {
        fixedDelta = Time.fixedDeltaTime;
    }

    void OnTriggerEnter(Collider collider)
    {
        Time.timeScale = 0.2f;
        Time.fixedDeltaTime = fixedDelta * Time.timeScale;

        Debug.Log($"Shoot in {shooter.gameObject.name}");
        Debug.Log($"Collider in {collider.gameObject.name}");
        GetComponent<BoxCollider>().enabled = false;

        StartCoroutine(shooter.Shoot());
        //for (int i = 0; i < numberOfShots; i++)
        //{
        //    StartCoroutine(shooter.Shoot());
        //    yield return new WaitForSeconds(.4f);        
        //}
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(1f);
    }
}
