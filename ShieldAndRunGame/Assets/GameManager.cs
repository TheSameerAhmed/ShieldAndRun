using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] Animation restartLevel;
    [SerializeField] GameTimeManager gameTime;

    void GameOver()
    {
        // All lives have ran out

    }


    public void LifeLost()
    {

        gameTime.HaltTime();

        LifeTracker.instance.LostLife();

        if (LifeTracker.instance.isGameOver == false)
        {
            Debug.Log("Made it here");
            gameTime.NormalTimeRestore();
        }
        else
        {
            // TODO: Call Game Over 
            Debug.Log("In GAME OVER in GAMEMANAGER");
            gameTime.HaltTime();
        }
    }

    void LevelCompleted()
    {

    }

}
