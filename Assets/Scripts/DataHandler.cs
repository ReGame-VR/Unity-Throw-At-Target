using System.Collections;
using System.Collections.Generic;
using ReadWriteCSV;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    // Get the participant ID from Global Control, for actual CSV write purposes
    private string pid = GlobalControl.Instance.participantID;

    // Storing the data for final writing when trial concludes
    List<Data> data = new List<Data>();

    // Cumulative variables to compare how the user did across all environments
    private float cumultaiveAccuracyScore;

    // Data class to store relevant info for each trial. Each field is public readonly.
    class Data
    {
        public readonly string participantID; // ID of participant
        public readonly bool rightHanded; // If participant is rightHanded
        public readonly float height; // Height from calibration
        public readonly float armLength; // ArmLength from calibration

        public readonly int trialNum; // current trial
        public readonly float time; // time the trial took place

        public readonly float accuracyScore; // How many target hits relative to total throws
    }


}
