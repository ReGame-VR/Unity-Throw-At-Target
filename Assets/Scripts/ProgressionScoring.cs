using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressionScoring : MonoBehaviour
{
    public enum Result { Null, Hit, Miss };
    public Result[] throws;
    public int totalThrows, totalSuccesses;
    public int randomTotal;
    // GameObject reference to levelScaler object
    public GameObject levelScaler;
    // Scene to track current active scene
    private Scene scene;
    // int to get index of next scene to load from calibration
    private int nextSceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        // If on Random progression type, select a random value between 4 and 20 for total throws
        // the participant must complete
        if (GlobalControl.Instance.progression.Equals(GlobalControl.ProgressionType.Random))
        {
            randomTotal = Random.Range(4, 20);
            Debug.Log("Player must complete " + randomTotal + " throws.");
        }
        // If on Performance progression type, establish a Result array sized to match the denominator
        // of the successes fraction (ex: 3/5 throws to progress -> Results[5])
        if (GlobalControl.Instance.progression.Equals(GlobalControl.ProgressionType.Performance))
        {
            Debug.Log("Player must complete " + GlobalControl.Instance.numSuccesses.x + " out of the last " + GlobalControl.Instance.numSuccesses.y + " throws.");
            throws = new Result[GlobalControl.Instance.numSuccesses.y];
        }

        // After this scene, in Choice mode, always return to title menu
        if (GlobalControl.Instance.progression.Equals(GlobalControl.ProgressionType.Choice))
        {
            nextSceneIndex = 0;
        }

        else
        {
            // Next Scene to load will be the one after this one in the build
            scene = SceneManager.GetActiveScene();
            nextSceneIndex = scene.buildIndex;
            if (SceneManager.sceneCount - 1 > nextSceneIndex)
            {
                nextSceneIndex++;
            }
            else
            {
                Debug.Log("Next scene not loaded in build settings. Reloading current scene.");
            }
        }
    }

    // Marks a throw as completed, and logs its result in the array
    public void ThrowComplete(bool success)
    {
        // Add to throw total
        Debug.Log("DEBUG ----- Throw logged.");
        totalThrows += 1;
        // If success, add to success total, and if in Performance progression mode, add a Hit result
        // to the Results array
        if (success)
        {
            Debug.Log("DEBUG ----- Throw was successful.");
            totalSuccesses += 1;
            if (GlobalControl.Instance.progression.Equals(GlobalControl.ProgressionType.Performance))
            {
                AddThrowResult(Result.Hit);
            }
        }
        // If not a success, and in Performance progression mode, add a Miss result to 
        else
        {
            if (GlobalControl.Instance.progression.Equals(GlobalControl.ProgressionType.Performance))
            {
                AddThrowResult(Result.Miss);
            }
        }

        CheckProgression();
    }

    // Adds a Result to the array of ThrowResults, filling in from the top, and removing the bottom one
    // Acts as a queue, first-in-first-out
    public void AddThrowResult(Result throwResult)
    {
        // Result placeholder to hold the previous Result to aid in swapping the array elements
        Result prev = throws[0];
        throws[0] = throwResult;
        Result temp;
        for (int i = 1; i < throws.Length; i++)
        {
            temp = throws[i];
            throws[i] = prev;
            prev = temp;
        }
    }

    private void CheckProgression()
    {
        // If the player throws enough total throws to meet the total requirement, move on to the next scene
        if (GlobalControl.Instance.progression.Equals(GlobalControl.ProgressionType.Random) && totalThrows >= randomTotal)
        {
            LoadNextScene();
        }

        // If the player has thrown enough times that they could have met the performance goal (assuming 100% success rate)
        if (GlobalControl.Instance.progression.Equals(GlobalControl.ProgressionType.Performance) &&
            totalThrows >= GlobalControl.Instance.numSuccesses.x)
        {
            // Int to track the current # of successes within the tracked # of throws
            int currSuccesses = 0;
            // Check every item in throws array
            for (int i = 0; i < throws.Length; i++)
            {
                // If the player has not thrown enough to meet the required condition
                if (throws[i].Equals(Result.Null))
                {
                    Debug.Log("DEBUG ----- Not enough total throws.");
                    break;
                }

                // Tallies up each success
                if (throws[i].Equals(Result.Hit))
                {
                    currSuccesses += 1;
                    Debug.Log("DEBUG ----- Tallied success, current successes is " + currSuccesses + " out of " +
                              GlobalControl.Instance.numSuccesses.x + " needed.");
                }

                // If player has met required # of successes, move on to next scene
                if (currSuccesses >= GlobalControl.Instance.numSuccesses.x)
                {
                    Debug.Log("DEBUG ----- Success threshold met with " + currSuccesses + " out of " +
                              GlobalControl.Instance.numSuccesses.x + " needed.");
                    LoadNextScene();
                }
            }
        }
    }

    // Progress to next scene
    public void LoadNextScene()
    {
        // Sends to levelScaler's load function for fade purposes
        Debug.Log("DEBUG ----- Sending to levelScaler's load function.");
        levelScaler.GetComponent<LevelHeightScale>()?.LoadSceneHelper();
    }
}
