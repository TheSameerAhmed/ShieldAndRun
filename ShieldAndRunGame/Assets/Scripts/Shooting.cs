﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class Shooting : MonoBehaviour
{   
    [SerializeField] PlayerController target;
    [SerializeField] ParticleSystem flash;
    [SerializeField] GameObject impact;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] LineRenderer bulletLine;
    [SerializeField] GameObject shootingRange;
    [SerializeField] float deviation;
    [SerializeField] Text targetDisplay;
    [SerializeField] int testValue;
    [SerializeField] GameTimeManager gameTimeManager;
    [SerializeField] CoinManager coinManager;
    [SerializeField] Material material;

    [SerializeField] LaserBeam laserBeam;

    Vector3 targetPoint;
    Quaternion targetRotation;
    int[] targetIndex = new int[5];
    int index = 0;
    float fixedDelta;
    int reflectionDepth = 5;
    bool inShoot;
    Ray ray;
    bool inShieldAfterShot = false;

    public int maxReflectionCount = 7;
    public float maxDistance = 200;

    List<Vector3> vertices = new List<Vector3>();

    Vector3 pos;

    float TimeCount = 0.0f;

    void Awake()
    {
        //for (int i = 0; i < 5; i++)
        //    targetIndex[i] = UnityEngine.Random.Range(5, 10);

        for (int i = 0; i < 5; i++)
            targetIndex[i] = testValue;
    }

    void FixedUpdate()
    {
        //Debug.Log("FixedUPdate");
        Transform target1 = target.transform.GetChild(targetIndex[index]).transform;
        //Debug.Log(target.transform.GetChild(targetIndex[index]).name);

        targetPoint = new Vector3(target1.position.x, target1.position.y, target1.position.z) - transform.position;
        targetRotation = Quaternion.LookRotation(targetPoint, Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, TimeCount);
        flash.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, TimeCount);
        firePoint.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, TimeCount);

        if (inShoot)
        {
            //Debug.Log("INSHOOT");
            laserBeam.DrawPredictions(firePoint.position, firePoint.forward, 7);
            laserBeam.DisplayLines();
            laserBeam.ClearList();

            if (inShieldAfterShot)
            {
                bulletLine.material = material;
                Debug.Log("Changing material");
            }
        }

        TimeCount += Time.deltaTime * 0.8f;

    }

    public IEnumerator Shoot()
    {
        // PreFab shooting
        // Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);        

        yield return StartCoroutine(DisplayTarget());

        inShoot = true;

        laserBeam.DrawInitialPredictions(firePoint.position, firePoint.forward, 7, 7, bulletLine);
        laserBeam.DisplayLines();
        laserBeam.ClearList();

        //ray = new Ray(firePoint.position, firePoint.forward + new Vector3(0, 0, deviation));
        
        //Physics.Raycast(ray, out RaycastHit hit);       

        //if (hit.collider != null)
        //{            
        //    Debug.Log($"Shooter hit: {hit.collider.gameObject.name}, Shooter name: {gameObject.name}");

        //    if (coinManager.inShieldPower)
        //    {
        //        inShoot = true;
        //        Debug.Log("In Shoot when shield is ON");
        //        bulletLine.SetPosition(0, ray.origin);
        //        bulletLine.SetPosition(1, hit.point);
        //        inShieldAfterShot = true;
        //        bulletLine.endColor = Color.red;
        //    }

        //    else if (hit.collider.gameObject.name == "Player")
        //    {
        //        Debug.Log("hit the player!");

        //        gameTimeManager.HaltTime();

        //        bulletLine.SetPosition(0, ray.origin);
        //        bulletLine.SetPosition(1, hit.point);
        //        bulletLine.SetPosition(2, (hit.point + (reflectionDepth * Vector3.Reflect(firePoint.forward + new Vector3(0, 0, deviation), hit.normal))));
        //    }
        //    else if (hit.collider.gameObject.name == "RightShield" || hit.collider.gameObject.name == "LeftShield")
        //    {
        //        //Debug.Log("Shield Touch");
        //        inShoot = true;

        //        bulletLine.SetPosition(0, ray.origin);
        //        bulletLine.SetPosition(1, hit.point);
        //        bulletLine.SetPosition(2, (hit.point + (reflectionDepth * Vector3.Reflect(firePoint.forward + new Vector3(0, 0, deviation), hit.normal))));
        //    }
        //    else
        //    {

        //        Debug.Log("SOME target is hit");
        //        if((Convert.ToInt32(hit.collider.gameObject.tag) != targetIndex[index] - 4) && (Convert.ToInt32(hit.collider.gameObject.tag) != targetIndex[index] - 5))
        //            gameTimeManager.HaltTime();

        //        bulletLine.SetPosition(0, ray.origin);
        //        bulletLine.SetPosition(1, hit.point);
        //        bulletLine.SetPosition(2, (hit.point + (reflectionDepth * Vector3.Reflect(firePoint.forward + new Vector3(0, 0, deviation), hit.normal))));

                
        //    }
        //}
        //else
        //{
        //    Debug.Log("NONE of the objects hit");
        //    bulletLine.SetPosition(0, ray.origin);
        //    bulletLine.SetPosition(1, ray.origin + (ray.direction * 10));
        //    bulletLine.SetPosition(2, bulletLine.GetPosition(1));
        //}        

        bulletLine.enabled = true;
        index++;
        yield return new WaitForSeconds(0.1f);
        bulletLine.enabled = false;


        gameTimeManager.NormalTimeRestore();
        inShoot = false;
        inShieldAfterShot = false;

    }

    public void StopShooting()
    {
        enabled = false;
    }

    IEnumerator DisplayTarget()
    {
        targetDisplay.text = (targetIndex[index]-4).ToString();
        yield return new WaitForSeconds(.3f);
    }

    //void Shoot1()
    //{
    //    flash.Play();

    //    RaycastHit hit;       

    //    if (Physics.Raycast(transform.position, transform.forward, out hit,range))
    //    {            
    //        Debug.DrawRay(transform.position, transform.forward * 1000, Color.blue,2f);
    //        Debug.Log(hit.collider.gameObject.name);
    //        if (hit.collider.gameObject.name == target.name)
    //        {
    //            target.forwardMovement = 0;
    //            GameObject temp = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
    //            Destroy(temp, 0.3f);
    //        }

    //    }
    //    else
    //    {
    //        Debug.DrawRay(transform.position, transform.forward * 1000, Color.blue, 2f);
    //        Debug.Log("NoHit");
    //    }

    //}



}
