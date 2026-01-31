using System;
using System.Collections.Generic;
using UnityEngine;

public class PresetPositionManager : MonoBehaviour
{
    [Header("Position spawn points")]
    [SerializeField] private Vector2[] presetPositions;

    //temp serialized
    private Vector2[] selectedPositions = new Vector2[5];

    private void Start()
    {
        int _selection = UnityEngine.Random.Range(0, 9);
        print("Selection is: " + _selection + " * 5 is: " + (_selection * 5));

        for (int i = 0; i < 5; i++) 
        {
            int j = i + (_selection * 5);
            selectedPositions[i] = presetPositions[j];
        }
    }

    public Vector2[] GetChosenPositions() 
    {
        return selectedPositions;
    }
}
