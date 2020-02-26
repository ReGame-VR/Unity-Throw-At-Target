using System.Collections;
using System.Collections.Generic;
using ReadWriteCSV;
using UnityEngine;

/**
 * Writes a line of data to CSV after every trial.
 */
public class DataHandler : MonoBehaviour
{
    // Get the participant ID from Global Control, for actual CSV write purposes
    private string pid = GlobalControl.Instance.participantID;

    // Storing the data for final writing when trial concludes
    List<Data> data = new List<Data>();

    // Cumulative variables to compare how the user did across all environments
    private float cumultaiveAccuracyScore = 0f;

    // Data class to store relevant info for each trial. Each field is public readonly.
    class Data
    {
        public readonly string participantID; // ID of participant
        public readonly bool rightHanded; // If participant is rightHanded
        public readonly GlobalControl.ProgressionType progressionType; // What progression type
        public readonly GlobalControl.Scene environment; // Which environment they were in
        public readonly float height; // Height from calibration
        public readonly float armLength; // ArmLength from calibration
        

        public readonly int trialNum; // current trial
        public readonly float time; // time the trial took place

        public readonly float accuracyScore; // How many target hits relative to total throws within environment
        public readonly float cumulativeAccuracyScore; // Accuracy score averaged across all environments

        // Constructor
        public Data(string participantID, bool rightHanded, float height, float armLength, int trialNum,
            float time, float accuracyScore, float cumulativeAccuracyScore)
        {
            this.participantID = participantID;
            this.rightHanded = rightHanded;
            this.height = height;
            this.armLength = armLength;

            this.trialNum = trialNum;
            this.time = time;
            this.accuracyScore = accuracyScore;
            this.cumulativeAccuracyScore = cumulativeAccuracyScore;
        }
    }


}
