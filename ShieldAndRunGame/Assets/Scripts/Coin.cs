using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] CoinManager coinManager;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] GameObject coinCollectEffect;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player")) 
            CollectCoin();
    }

    void CollectCoin()
    {
        GameObject effect = Instantiate(coinCollectEffect, transform.position, transform.rotation);

        playerStats.coinsColected++;
        coinManager.UpdateDisplayCoins();
        Debug.Log($"collected coin {playerStats.coinsColected}");
        
        Destroy(gameObject);
        Destroy(effect, 1.0f);
    }
}
