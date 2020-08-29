using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dial : MonoBehaviour
{
    [SerializeField] Image timerDial;
    [SerializeField] CoinManager coinManager;

    public bool startTimer;
    
    float timeLeft;

    // Start is called before the first frame update
    void Awake()
    {
        startTimer = false;
        timeLeft = coinManager.slowTime;        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(startTimer);
        //Debug.Log(timeLeft);

        if (timeLeft > 0 && startTimer)
        {
            timeLeft -= Time.unscaledDeltaTime;
            timerDial.fillAmount = timeLeft / coinManager.slowTime;
        }
        if (timeLeft < 0 && startTimer)
            startTimer = false;
    }
}
