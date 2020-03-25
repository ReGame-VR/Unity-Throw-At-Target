 using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Script for Menu scene, when player info is being entered
public class MenuController : MonoBehaviour
{
    public TextMeshProUGUI valueText, timeText, environmentText1, environmentText2;
    public Toggle rightHandToggle;
    public TMP_Dropdown chooseMode, choiceTimeSelect, environmentSelect, sessionSelect;
    public Slider successSlider, totalSlider;
    // Start is called before the first frame update
    void Start()
    {
        // disable VR settings for menu scene
        UnityEngine.XR.XRSettings.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Changes the menu UI based on which Progression Type is currently selected
        if (ProgressionConvert(chooseMode.value).Equals(GlobalControl.ProgressionType.Performance))
        {
            valueText.gameObject.SetActive(true);
            successSlider.gameObject.SetActive(true);
            totalSlider.gameObject.SetActive(true);
            valueText.text = "Player must complete " + successSlider.value + "/" + totalSlider.value + " attempts.";
            if (totalSlider.value < successSlider.value)
            {
                totalSlider.value = successSlider.value;
            }
        }
        else
        {
            valueText.gameObject.SetActive(false);
            successSlider.gameObject.SetActive(false);
            totalSlider.gameObject.SetActive(false);
        }

        if (ProgressionConvert(chooseMode.value).Equals(GlobalControl.ProgressionType.Choice))
        {
            choiceTimeSelect.gameObject.SetActive(true);
            timeText.gameObject.SetActive(true);
            environmentSelect.gameObject.SetActive(true);
            environmentText1.gameObject.SetActive(true);
            environmentText2.gameObject.SetActive(true);
        }
        else
        {
            choiceTimeSelect.gameObject.SetActive(false);
            timeText.gameObject.SetActive(false);
            environmentSelect.gameObject.SetActive(false);
            environmentText1.gameObject.SetActive(false);
            environmentText2.gameObject.SetActive(false);
        }
    }

    // Converts the value of ModeSelect to a Progression type
    GlobalControl.ProgressionType ProgressionConvert(int value)
    {
        if (value == 0) {
            //Debug.Log("Progression mode Performance selected");
            return GlobalControl.ProgressionType.Performance;
        }
        if (value == 1) {
            //Debug.Log("Progression mode Random selected");
            return GlobalControl.ProgressionType.Random;
        }
        else {
            //Debug.Log("Progression mode Choice selected");
            return GlobalControl.ProgressionType.Choice;
        }
    }

    // Converts the value of EnvironmentSelect to a Progression type
    GlobalControl.Scene EnvironmentConvert(int value)
    {
        if (value == 0)
        {
            return GlobalControl.Scene.Classroom;
        }
        if (value == 1)
        {
            return GlobalControl.Scene.Park;
        }
        else
        {
            return GlobalControl.Scene.Moon;
        }
    }

    // Progresses to next scene, setting values in GlobalControl
    public void NextScene()
    {
        GlobalControl.Instance.progression = ProgressionConvert(chooseMode.value);
        // Sets success trackers if operator selected Performance progression mode
        if (GlobalControl.Instance.progression.Equals(GlobalControl.ProgressionType.Performance))
        {
            GlobalControl.Instance.numSuccesses.x = (int) successSlider.value;
            GlobalControl.Instance.numSuccesses.y = (int) totalSlider.value;
        }
        // Sets nextScene in GlobalControl to the proper scene select if operator selected
        // Choice progression mode
        if (GlobalControl.Instance.progression.Equals(GlobalControl.ProgressionType.Choice))
        {
            // Sets nextScene to be the selected environment
            GlobalControl.Instance.nextScene = EnvironmentConvert(environmentSelect.value);
        }
        // In the event the player has not calibrated during Choice progression (this always
        // applies to other two modes), loads Calibration settings and scene
        if (!GlobalControl.Instance.hasCalibrated)
        {
            GlobalControl.Instance.isRightHanded = rightHandToggle.enabled;
            // Specifically loads Calibration scene here so as to not overwrite Choice's nextScene
            Debug.Log("DEBUG ----- Loading Calibration Scene");
            SceneManager.LoadScene("Calibration");
        }
        // If player has already calibrated and is still undergoing Choice progression,
        // loads new selected environment here
        //GlobalControl.Instance.NextScene();
    }
}
