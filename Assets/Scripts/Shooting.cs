﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class Shooting : MonoBehaviour
{

    [SerializeField] float range = 100f;    
    [SerializeField] PlayerController target;
    [SerializeField] ParticleSystem flash;
    [SerializeField] GameObject impact;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] LineRenderer bulletLine;
    [SerializeField] GameObject shootingRange;
    [SerializeField] float deviation;
    [SerializeField] Text targetDisplay;

    Vector3 targetPoint;
    Quaternion targetRotation;
    int[] targetIndex = new int[5];
    int index = 0;
    float fixedDelta;

    Vector3 pos;

    float TimeCount = 0.0f;

    float fixedDeltaTime;

    void Awake()
    {
        fixedDelta = Time.fixedDeltaTime;

        for (int i = 0; i < 5; i++)
            targetIndex[i] = Random.Range(5, 10);
    }

    void Start()
    {
        fixedDeltaTime = Time.fixedDeltaTime;                
    }
    //void Update()
    //{

    //    Transform target1 = target.transform.GetChild(Random.Range(5, 10)).transform;
    //    Debug.Log(target.transform.GetChild(Random.Range(5, 10)).name);

    //    targetPoint = new Vector3(target1.position.x, target1.position.y, target1.position.z) - transform.position;
    //    targetRotation = Quaternion.LookRotation(targetPoint, Vector3.up);

    //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, TimeCount);
    //    flash.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, TimeCount);
    //    firePoint.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, TimeCount);

    //    TimeCount += Time.deltaTime * 0.8f;
    //}

    void FixedUpdate()
    {
        Transform target1 = target.transform.GetChild(targetIndex[index]).transform;
        Debug.Log(target.transform.GetChild(targetIndex[index]).name);

        targetPoint = new Vector3(target1.position.x, target1.position.y, target1.position.z) - transform.position;
        targetRotation = Quaternion.LookRotation(targetPoint, Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, TimeCount);
        flash.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, TimeCount);
        firePoint.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, TimeCount);

        TimeCount += Time.deltaTime * 0.8f;
    }



    public IEnumerator Shoot()
    {
        // PreFab shooting
        // Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);        

        yield return StartCoroutine(DisplayTarget());

        Ray ray = new Ray(firePoint.position, firePoint.forward + new Vector3(0, 0, deviation));
        
        Physics.Raycast(ray, out RaycastHit hit);

       // bool hitPlayer = false, shieldCheck = false;

        if (hit.collider != null)
        {
            Debug.Log($"Shooter hit: {hit.collider.gameObject.name}, Shooter name: {gameObject.name}");
            if (hit.collider.gameObject.name == "Player")
            {
                Debug.Log("hit the player bitch!");
                //target.forwardMovement = 0;
                //hitPlayer = true;
                
                bulletLine.SetPosition(0, ray.origin);
                bulletLine.SetPosition(1, hit.point);
                bulletLine.SetPosition(2, (hit.point + Vector3.Reflect(firePoint.forward + new Vector3(0, 0, deviation), hit.normal)));
            }
            else if (hit.collider.gameObject.name == "RightShield" || hit.collider.gameObject.name == "LeftShield")
            {
                Debug.Log("Shield Touch");
                bulletLine.SetPosition(0, ray.origin);
                bulletLine.SetPosition(1, hit.point);
                bulletLine.SetPosition(2, (hit.point + Vector3.Reflect(firePoint.forward + new Vector3(0, 0, deviation), hit.normal)));
                //shieldCheck = true;
            }
            else
            {

                Debug.Log("SOME object is hit");

                bulletLine.SetPosition(0, ray.origin);
                bulletLine.SetPosition(1, hit.point);
                bulletLine.SetPosition(2, (hit.point + Vector3.Reflect(firePoint.forward + new Vector3(0, 0, deviation), hit.normal)));
                if (hit.collider.gameObject.name == "Target1")
                {
                    Debug.Log("Target HIT");
                    //Time.timeScale = 0;
                }
            }
        }
        else
        {
            Debug.Log("NONE of the objects hit");
            bulletLine.SetPosition(0, ray.origin);
            bulletLine.SetPosition(1, ray.origin + (ray.direction * 10));
            bulletLine.SetPosition(2, bulletLine.GetPosition(1));
        }

        bulletLine.enabled = true;
        index++;
        yield return new WaitForSeconds(0.2f);
        bulletLine.enabled = false;

        Time.timeScale = 1;
        Time.fixedDeltaTime = fixedDelta * Time.timeScale;

        //if (hitPlayer) 
        //    Time.timeScale = 0;
        //if (shieldCheck)
        //    Time.timeScale = 0;

        //if (!shieldCheck && !hitPlayer)
        //    Time.timeScale = 1;

    }

    public void StopShooting()
    {
        enabled = false;
    }

    IEnumerator DisplayTarget()
    {
        targetDisplay.text = (targetIndex[index]-4).ToString();
        yield return new WaitForSeconds(0.3f);
    }

    void Shoot1()
    {
        flash.Play();

        RaycastHit hit;       

        if (Physics.Raycast(transform.position, transform.forward, out hit,range))
        {            
            Debug.DrawRay(transform.position, transform.forward * 1000, Color.blue,2f);
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.name == target.name)
            {
                target.forwardMovement = 0;
                GameObject temp = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(temp, 0.3f);
            }

        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 1000, Color.blue, 2f);
            Debug.Log("NoHit");
        }

    }



}