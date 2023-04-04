using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float currentTime;
    bool timing;
   

    // Update is called once per frame


    public void StartTimer()
    {
        timing = true;
    }
    public void StopTimer()
    {
        timing = false;
    }
    public float GetTime()
    {
        return currentTime;
    }
    void Update()
    {
        if(timing)
            currentTime += Time.deltaTime;
    }

}
