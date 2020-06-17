using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTimer : MonoBehaviour
{
    public float targetTime = 60.0f;

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;

        if (targetTime <= 0) { TimerEnded(); }
    }

    void TimerEnded()
    {
        // Finish
    }
}
