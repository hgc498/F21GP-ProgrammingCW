using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//unity will seriaslise a custom class 
//the fileds will shown up im the inpesctor 
[System.Serializable]
public class Stats 
{
    [SerializeField]
    private int baseValue;

    public int GetValue()
    {
        return baseValue;
    }

}
