using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeTracker : MonoBehaviour
{
    public static LifeTracker instance;

    public List<Tuple<GameObject, bool>> lifeStatus;

    public bool isGameOver;

    [SerializeField] GameObject lives;
    [SerializeField] GameTimeManager gameTime;

    int numberOfLives = 3;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        lifeStatus = new List<Tuple<GameObject, bool>>();

        for (int i = 0; i < numberOfLives; i++)
        {
            GameObject temp = lives.transform.GetChild(i).gameObject;
            lifeStatus.Add(new Tuple<GameObject, bool>(temp, false));
        }
    }

    public void LostLife()
    {
        int i;
        for (i = 0; i < numberOfLives; i++)
        {
            if (instance.lifeStatus[i].Item2 == false)
            {
                instance.lifeStatus[i] = new Tuple<GameObject, bool>(lifeStatus[i].Item1, true);
                instance.lifeStatus[i].Item1.GetComponent<Image>().color = Color.red;
                RestartLevel();
                return;
            }
        }
        Debug.Log($"Value of i = {i}");

        if (i == numberOfLives)
        {
            isGameOver = true;
            Debug.Log("GAME OVER");
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}

