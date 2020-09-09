using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{

    public int maxReflectionCount = 7;
    public float maxDistance = 200;

    LineRenderer beam;
    
    [SerializeField] GameTimeManager gameTime;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject shieldEffect;
    [SerializeField] GameObject gameOverEffect;

    [SerializeField] Transform rightShield;
    [SerializeField] Transform leftShield;
    [SerializeField] Transform player;

    public List<Tuple<GameObject, bool>> lifeStatus;

    List<Vector3> tangents = new List<Vector3>();

    public void DrawPredictions(Vector3 position, Vector3 direction, int reflectionsRemaining)
    {

        if (reflectionsRemaining == 0)
            return;

        Vector3 startingPosition = position;

        Ray ray = new Ray(position, direction);
        //Debug.DrawRay(position, direction, Color.red, 3f);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            //Debug.Log("In reflection hit");
            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;
        }
        else
        {
            position += direction * maxDistance;
        }

        tangents.Add(startingPosition);
        //tangents.Add(position);

        DrawPredictions(position, direction, reflectionsRemaining - 1);
    }

    public void DrawInitialPredictions(Vector3 position, Vector3 direction, int reflectionsRemaining, int originalNumber, LineRenderer laserLine)
    {
        bool hitPlayer = false;
        beam = laserLine;

        Debug.Log($"number: {reflectionsRemaining}");
        
        Ray initialRay = new Ray(position, direction);

        Physics.Raycast(initialRay, out RaycastHit initialHit);

        if (initialHit.collider != null)
        {
            Debug.Log($"Shooter hit: {initialHit.collider.gameObject.tag}, Shooter name: {gameObject.name}");

            if (initialHit.collider.gameObject.CompareTag("Player"))
            {
                Instantiate(gameOverEffect, player);
                Debug.Log("hit the player!");
                hitPlayer = true;

                gameManager.LifeLost();                
            }
            else if (initialHit.collider.gameObject.CompareTag("Shield"))
            {
                Debug.Log("INSHIELD");
               
                if (initialHit.collider.gameObject.name == "RightShield")
                {
                    GameObject temp = Instantiate(shieldEffect, rightShield);
                    Destroy(temp, 2f);
                }
                else
                {
                    GameObject temp = Instantiate(shieldEffect, leftShield);
                    Destroy(temp, 2f);
                }

            }
        }


        Vector3 startingPosition = position;

        Ray ray = new Ray(position, direction);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            Debug.Log("In reflection hit");
            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;
        }
        else
        {
            position += direction * maxDistance;
        }

        tangents.Add(startingPosition);
        if (hitPlayer)
        {
            tangents.Add(position);
            Debug.Log("In Hit player");
            return;
        }
        DrawPredictions(position, direction, reflectionsRemaining - 1);
    }

    public void DisplayLines()
    {
        beam.positionCount = tangents.Count;

        Vector3[] inp = new Vector3[tangents.Count];

        int i = 0;

        foreach (Vector3 v in tangents)
        {
            inp[i] = v;
            i++;
        }

        beam.SetPositions(inp);
    }

    public void ClearList()
    {
        tangents.Clear();
    }
}
