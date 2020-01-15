using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressionScoring : MonoBehaviour
{
    public enum Result { Null, Hit, Miss };
    public Result[] throws;
    public int totalThrows, totalSucesses;
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
        if (GlobalControl.Instance.progression.Equals(GlobalControl.ProgressionType.Random))
        {
            randomTotal = Random.Range(4, 20);
            Debug.Log("Player must complete " + randomTotal + " throws.");
        }
        if (GlobalControl.Instance.progression.Equals(GlobalControl.ProgressionType.Performance))
        {
            throws = new Result[GlobalControl.Instance.numSuccesses.y];
        }
        scene = SceneManager.GetActiveScene();
        nextSceneIndex = scene.buildIndex + 1;
    }

    // Update is called once per frame
    void Update()
    {
        // If the player throws enough total throws to meet the total requirement, move on to the next scene
        if (GlobalControl.Instance.progression.Equals(GlobalControl.ProgressionType.Random) && totalThrows >= randomTotal)
        {
            LoadNextScene();
        }
        // If the player has thrown enough times that they could have met the performance goal (assuming 100% success rate)
        if (GlobalControl.Instance.progression.Equals(GlobalControl.ProgressionType.Performance) && totalThrows >= GlobalControl.Instance.numSuccesses.x)
        {
            // Int to track the current # of successes within the tracked # of throws
            int currSuccesses = 0;
            for (int i = 0; i < throws.Length; i++)
            {
                // If the player has not thrown enough to meet the required condition
                if (throws[i].Equals(Result.Null))
                {
                    break;
                }
                // Tallies up each success
                if (throws[i].Equals(Result.Hit))
                {
                    currSuccesses += 1;
                }
            }
            // If player has met required # of successes, move on to next scene
            if (currSuccesses >= GlobalControl.Instance.numSuccesses.x)
            {
                LoadNextScene();
            }
        }
    }

    // Marks a throw as completed, and logs its result in the array
    public void ThrowComplete(bool success)
    {
        totalThrows += 1;
        if (success)
        {
            totalSucesses += 1;
            if (GlobalControl.Instance.progression.Equals(GlobalControl.ProgressionType.Performance))
            {
                AddThrowResult(Result.Hit);
            }
        }
        else
        {
            if (GlobalControl.Instance.progression.Equals(GlobalControl.ProgressionType.Performance))
            {
                AddThrowResult(Result.Miss);
            }
        }
    }

    // Adds a Result to the array of ThrowResults, filling in from the top, and removing the bottom one
    public void AddThrowResult(Result throwResult)
    {
        // Result placeholder to hold the previous Result to aid in swapping the array elements
        Result prev = throws[0];
        throws[0] = throwResult;
        for (int i = 1; i < throws.Length; i++)
        {
            Result temp = throws[i];
            throws[i] = prev;
            prev = temp;
        }
    }

    // Progress to next scene
    public void LoadNextScene()
    {
        levelScaler.GetComponent<LevelHeightScale>().LoadSceneHelper();
    }
}
