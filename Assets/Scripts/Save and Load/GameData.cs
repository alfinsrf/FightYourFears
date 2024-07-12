using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int currency;

    public SerializableDictionary<string, bool> skillTree;

    public SerializableDictionary<string, float> volumeSettings;

    public GameData()
    {       
        this.currency = 0;

        skillTree = new SerializableDictionary<string, bool>();

        volumeSettings = new SerializableDictionary<string, float>();

    }
}