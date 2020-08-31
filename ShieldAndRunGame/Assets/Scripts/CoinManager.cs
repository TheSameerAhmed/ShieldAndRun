using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    [SerializeField] PlayerStats coins;
    [SerializeField] Text displayCoins;
    [SerializeField] Text slowTimeText;
    [SerializeField] Text shieldPowerText;
    [SerializeField] GameObject shieldPowerObject;
    [SerializeField] GameObject player;
    [SerializeField] GameObject dial;
    [SerializeField] Canvas canvas;
    [SerializeField] GameTimeManager gameTimeManager;

    public bool inSlowTimePower;
    
    bool shieldPowerUp;
    bool slowTimePowerUp;
    float shieldTime = 1.5f;
 
    float fixedDelta;
    float slowness = 0.1f;

    public float slowTime = 8f;

    void Awake()
    {
        displayCoins.text = "0";
        fixedDelta = Time.fixedDeltaTime;
        shieldPowerUp = false;
        slowTimePowerUp = false;
        inSlowTimePower = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G) && shieldPowerUp)
        {
            ShieldPower();
        }
        else if(Input.GetKeyDown(KeyCode.J) && slowTimePowerUp)
        {
            Debug.Log("Starting coroutine");
            StartCoroutine(SlowTime());
        }
        else if(Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log(slowTimePowerUp);
        }

    }

    public void UpdateDisplayCoins()
    {
        displayCoins.text = $"{coins.coinsColected}";
        if(inSlowTimePower == false)
            CheckPowers();
    }

    void CheckPowers()
    {
        if (coins.coinsColected >= 3)
        {
            slowTimeText.color = Color.red;
            slowTimePowerUp = true;
        }
        else
        {
            slowTimeText.color = Color.white;
            slowTimePowerUp = false;
        }

        if (coins.coinsColected >= 5)
        {
            shieldPowerText.color = Color.red;
            shieldPowerUp = true;
        }
        else
        {
            shieldPowerText.color = Color.white;
            shieldPowerUp = false;
        }
    }

    void ShieldPower()
    {
        coins.coinsColected -= 5;
        UpdateDisplayCoins();

        var shield = Instantiate(shieldPowerObject, player.transform, false);
        Destroy(shield, shieldTime); 

        // Shield player by not getting affected by the laser for some time span or time till level finishes, whatever comes first.

    }

    IEnumerator SlowTime()
    {
        Debug.Log("In slow time");
        coins.coinsColected -= 3;
        inSlowTimePower = true;

        HaltAllPowerUps();
        displayCoins.text = $"{coins.coinsColected}";

        var dialCreated = Instantiate(dial, canvas.transform, false);

        dialCreated.GetComponent<Dial>().startTimer = true;

        Debug.Log("Slowing time down");

        gameTimeManager.SlowTimeDown(slowness);

        yield return new WaitForSecondsRealtime(slowTime);        
        
        Debug.Log("Done Slowing time down");

        UpdateDisplayCoins();               // Check new status of power ups and their validation state.

        CheckPowers();

        Destroy(dialCreated, 0.1f);
        inSlowTimePower = false;
        gameTimeManager.NormalTimeRestore();  // FIX when player is within in and out then dont restore normal time!      
        
        yield break;
    }

    void HaltAllPowerUps()
    {
        // Grey out the powerups and ensure the player cant turn on a power up during a powerup.

        shieldPowerUp = false;
        slowTimePowerUp = false;
        slowTimeText.color = Color.gray;
        shieldPowerText.color = Color.gray;


    }

}
