﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dial : MonoBehaviour
{
    [SerializeField] Image timerDial;
    [SerializeField] CoinManager coinManager;

    public bool startTimer;
    
    public float timeLeft;

    public float totalTime;

    // Start is called before the first frame update
    void Awake()
    {
        startTimer = false;
        //timeLeft = coinManager.slowTime;        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(startTimer);
        //Debug.Log(timeLeft);
        if (startTimer)
        {
            if (timeLeft > 0 && startTimer)
            {
                timeLeft -= Time.unscaledDeltaTime;
                timerDial.fillAmount = timeLeft / totalTime;
                //Debug.Log(timerDial.fillAmount);
                //Debug.Log($"tl {timerDial.fillAmount}");
                //Debug.Log($"cs {coinManager.slowTime}");
            }
            if (timeLeft < 0 && startTimer)
                startTimer = false;

        }
    }
}
