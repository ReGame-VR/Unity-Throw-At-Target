using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/**
 * Stores data to be used between scenes (chiefly, calibration details).
 */ 
public class GlobalControl : MonoBehaviour
{
    // bool to prevent more than one calibration
    public bool hasCalibrated = false;

    // float data for calibration
    public float height, armLength, platformOffset, multiplier, targetOffset;

    // boolean data collected in RecalibrateHeight.cs and the Calibration scene
    public bool isRightHanded;

    // participant ID to differentiate logs
    public string participantID;

    // enum type(and instance) to differentiate different progression 
    public enum ProgressionType {Performance, Random, Choice};
    public ProgressionType progression;

    // Vector2 to store the number of successes, the player must land a success in (x) of the last (y) throws 
    // to pass the level in Performance progression type
    public Vector2Int numSuccesses;

    // Enum for scene selection, as well as specific Scene object to load
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

    // Method for swapping scenes during Choice progression mode
    public void NextScene()
    {
        switch(nextScene)
        {
            case Scene.TitleScreen:
                SceneManager.LoadScene("TitleScreen");
                break;
            case Scene.Classroom:
                SceneManager.LoadScene("Classroom");
                break;
            case Scene.Park:
                SceneManager.LoadScene("Park");
                break;
            case Scene.Moon:
                SceneManager.LoadScene("Moon");
                break;
        }
    }
 }
