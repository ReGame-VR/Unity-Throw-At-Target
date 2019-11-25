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
            return GlobalControl.ProgressionType.Performance;
        }
        if (value == 1) {
            return GlobalControl.ProgressionType.Random;
        }
        else { 
            return GlobalControl.ProgressionType.Choice;
        }
    }

    // 

    // Progresses to next scene, setting values in GlobalControl
    public void NextScene()
    {
        if (ProgressionConvert(chooseMode.value).Equals(GlobalControl.ProgressionType.Performance))
        {
            if (totalSlider.value < successSlider.value)
            {
                Debug.Log("Total cannot be fewer than successes required!");
                return;
            }
            else
            {
                GlobalControl.Instance.numSuccesses.x = (int) successSlider.value;
                GlobalControl.Instance.numSuccesses.y = (int) totalSlider.value;
            }
        }
        if (ProgressionConvert(chooseMode.value).Equals(GlobalControl.ProgressionType.Choice))
        {

        }
        GlobalControl.Instance.progression = ProgressionConvert(chooseMode.value);
        GlobalControl.Instance.isRightHanded = rightHandToggle.enabled;
        SceneManager.LoadScene("Calibration");
    }
}
