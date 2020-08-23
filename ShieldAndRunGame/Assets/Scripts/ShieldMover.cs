using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class ShieldMover : MonoBehaviour
{

    float xPosition;
    //float movementSpeed = 5f;
    bool rightKeyPressed, leftKeyPressed;
    GameObject selectedShield;
    List<float> leftShieldPlacement = new List<float>();
    List<float> rightShieldPlacement = new List<float>();

    [SerializeField] GameObject rightShield;
    [SerializeField] GameObject leftShield;
    [SerializeField] Material selectedMaterial;
    [SerializeField] Material unselectedMaterial;

    GameObject leftParent;
    GameObject rightParent;
    int leftIndex = 1;
    int rightIndex = 1;

    void Start()
    {
        rightKeyPressed = false;
        leftKeyPressed = false;

        leftParent = leftShield.transform.parent.gameObject;
        rightParent = rightShield.transform.parent.gameObject;

        rightShieldPlacement.Add(0.0234f);
        rightShieldPlacement.Add(rightParent.transform.position.x);
        rightShieldPlacement.Add(0.1234f);
        leftShieldPlacement.Add(-0.125f);
        leftShieldPlacement.Add(leftParent.transform.position.x);
        leftShieldPlacement.Add(-0.025f);
    }

    // Update is called once per frame
    void Update()
    {

        rightShieldPlacement[0] = rightParent.transform.position.x - 0.05f;
        rightShieldPlacement[1] = rightParent.transform.position.x;              
        rightShieldPlacement[2] = rightParent.transform.position.x + 0.05f;

        leftShieldPlacement[0] = leftParent.transform.position.x - 0.05f;
        leftShieldPlacement[1] = leftParent.transform.position.x;
        leftShieldPlacement[2] = leftParent.transform.position.x + 0.05f;

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (selectedShield == null && Input.GetKeyUp(KeyCode.UpArrow))
            {
                selectedShield = rightShield;
                UpdateSelectedShield();
            }
            else if (selectedShield == null && Input.GetKeyUp(KeyCode.DownArrow))
            {
                selectedShield = leftShield;
                UpdateSelectedShield();
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow) && selectedShield == leftShield)
            {
                ClearSelectedShield();
                selectedShield = rightShield;                
                UpdateSelectedShield();
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow) && selectedShield == rightShield)
            {
                ClearSelectedShield();
                selectedShield = leftShield;                
                UpdateSelectedShield();
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow) && selectedShield == leftShield)
            {
                ClearSelectedShield();
                selectedShield = rightShield;               
                UpdateSelectedShield();
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow) && selectedShield == rightShield)
            {
                ClearSelectedShield();
                selectedShield = leftShield;                
                UpdateSelectedShield();
            }
        }

        if(selectedShield != null)
            xPosition = selectedShield.transform.parent.gameObject.transform.position.x;

        if (Input.GetKeyUp(KeyCode.D))
        {
            rightKeyPressed = true;
            Debug.Log("Right pressed");
            Debug.Log(xPosition);
        }
        else if (Input.GetKeyUp(KeyCode.S))
            leftKeyPressed = true;

        if (Input.GetKeyUp(KeyCode.Q))
            Debug.Log(selectedShield.transform.parent.gameObject.transform.position.x);
        else if(Input.GetKeyUp(KeyCode.W))
            Debug.Log(selectedShield.transform.parent.gameObject.transform.position.x + 0.05f);
    }

    void FixedUpdate()
    {
        

        if (rightKeyPressed && leftIndex != 2 && selectedShield == leftShield)
        {

            if (rightIndex != 0 || leftIndex == 0)
            {                
                leftIndex++;
                float temp = leftParent.transform.position.x + 0.05f;
                leftParent.transform.position = new UnityEngine.Vector3(temp, leftParent.transform.position.y, leftParent.transform.position.z);              

            }
        }
        else if(leftKeyPressed && leftIndex != 0 && selectedShield == leftShield)
        {            
            leftIndex--;
            float temp = leftParent.transform.position.x - 0.05f;
            leftParent.transform.position = new UnityEngine.Vector3(temp, leftParent.transform.position.y, leftParent.transform.position.z);                        
          
        }
        else if(rightKeyPressed && rightIndex != 2 && selectedShield == rightShield)
        {            
            rightIndex++;
            float temp = rightParent.transform.position.x + 0.05f;
            rightParent.transform.position = new UnityEngine.Vector3(temp, rightParent.transform.position.y, rightParent.transform.position.z);            

        }
        else if(leftKeyPressed && rightIndex != 0 && selectedShield == rightShield)
        {            
            if (leftIndex != 2 || rightIndex == 2)
            {                
                rightIndex--;
                float temp = rightParent.transform.position.x - 0.05f;
                rightParent.transform.position = new UnityEngine.Vector3(temp, rightParent.transform.position.y, rightParent.transform.position.z);                   
            }
        }
        

        rightKeyPressed = leftKeyPressed = false;
    }

    void UpdateSelectedShield()
    {
        if (selectedShield != null)
        {
            selectedShield.GetComponent<MeshRenderer>().material = selectedMaterial;
        }
    }

    void ClearSelectedShield()
    {
        if (selectedShield != null)
            selectedShield.GetComponent<MeshRenderer>().material = unselectedMaterial;
    }
}
