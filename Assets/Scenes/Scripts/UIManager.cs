using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject timer;
    public TextMeshProUGUI timerText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = timer.GetComponent<Timer>().currentTime.ToString("F2");
    }
}
