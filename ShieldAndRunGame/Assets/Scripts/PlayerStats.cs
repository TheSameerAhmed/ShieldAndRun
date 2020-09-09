using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public int coinsColected { get; set; } = 0;

    public List<Tuple<GameObject, bool>> lifeStatus { get; set; } = new List<Tuple<GameObject, bool>>();

    //public int[] three = new int[3];

    //public int uno, des, tres;

    //public GameObject rand;


}
