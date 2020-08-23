using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceCovered : MonoBehaviour
{

    [SerializeField] Transform track;
    [SerializeField] Transform player;

    int totalDistance;
    int currentDistanceCovered;

    // Start is called before the first frame update
    void Start()
    {
        currentDistanceCovered = 0;
        totalDistance = Convert.ToInt32(track.transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDistanceCovered >= totalDistance)
            return;
        GetComponent<Text>().text = $"{currentDistanceCovered}/{totalDistance}";
        currentDistanceCovered = Convert.ToInt32(player.position.z) + 10;
    }
}
