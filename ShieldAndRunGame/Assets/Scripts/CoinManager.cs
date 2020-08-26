using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    [SerializeField] PlayerStats coins;
    [SerializeField] Text displayCoins;
    void Awake()
    {
        displayCoins.text = "0";
    }

    public void UpdateDisplayCoins()
    {
        displayCoins.text = $"{coins.coinsColected}";
        if (coins.coinsColected >= 3)
        {
            ShieldPower();         
            coins.coinsColected = 0;
        }
    }

    void CheckPowers()
    {
        
    }

    void ShieldPower()
    {

        // Shield player by not getting affected by the laser for some time span or time till level finishes, whatever comes first.

    }



}
