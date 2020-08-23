using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    bool rightKey;
    bool leftKey;
    [SerializeField] Rigidbody player;
    [SerializeField] public float forwardMovement;
    [SerializeField] float sideForce; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            rightKey = true;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            leftKey = true;
        else if (Input.GetKeyUp(KeyCode.RightArrow))
            rightKey = false;
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
            leftKey = false;

        
    }

    void FixedUpdate()
    {
        player.AddForce(0, 0, forwardMovement * Time.deltaTime / Time.timeScale);
        if (rightKey)
            player.AddForce(sideForce * Time.deltaTime / Time.timeScale, 0, 0, ForceMode.VelocityChange);
        else if (leftKey)
            player.AddForce(-sideForce * Time.deltaTime / Time.timeScale, 0, 0, ForceMode.VelocityChange);

        //player.AddForce(0, 0, forwardMovement * Time.unscaledDeltaTime);
        //if (rightKey)
        //    player.AddForce(sideForce * Time.unscaledDeltaTime, 0, 0, ForceMode.VelocityChange);
        //else if (leftKey)
        //    player.AddForce(-sideForce * Time.unscaledDeltaTime, 0, 0, ForceMode.VelocityChange);


    }
}
