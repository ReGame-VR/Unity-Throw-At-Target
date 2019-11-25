using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Stores data to be used between scenes (chiefly, calibration details).
 */ 
public class GlobalControl : MonoBehaviour
{
    // float data for calibration
    public float height, armLength, platformOffset, multiplier, targetOffset;

    // boolean data collected in RecalibrateHeight.cs and the Calibration scene
    public bool isRightHanded;

    // participant ID to differentiate logs
    public string participantID;

    // enum type(and instance) to differentiate different progression 
    public enum ProgressionType {Performance, Random, Choice}; // NumThrows ??
    public ProgressionType progression;

    // Vector2 to store the number of successes, the player must land a success in (x) of the last (y) throws to pass the level
    public Vector2Int numSuccesses;

    public enum Scene { TitleScreen, Classroom, Park, Moon };
    public Scene nextScene;

    // Single instance of this class
    public static GlobalControl Instance;

    // Runs on startup
    private void Awake()
    {
        // If there is no Instance, makes this the Instance
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        // If an instance already exists, destroy this
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
 }
