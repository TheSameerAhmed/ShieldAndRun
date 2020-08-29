using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeManager : MonoBehaviour
{
    [SerializeField] CoinManager coinManager;

    float fixedDelta;
    

    void Awake()
    {        
        fixedDelta = Time.fixedDeltaTime;        
    }

    public void SlowTimeDown(float slowness)
    {
        Time.timeScale = slowness;
        Time.fixedDeltaTime = fixedDelta * Time.timeScale;
    }

    public void NormalTimeRestore()
    {
        if (coinManager.inSlowTimePower == false)
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = fixedDelta * Time.timeScale;
        }
    }

    public void HaltTime()
    {
        Time.timeScale = 0;
    }
}
