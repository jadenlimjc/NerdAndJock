using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockManager : MonoBehaviour
{
    public static ClockManager clockInstance;
    public Text clockText;
    private float timeTaken = 0f;
    private bool isRunning = false;

    void Awake() 
    {
        if (clockInstance == null)
        {
            clockInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        startClock();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning) 
        {
            timeTaken += Time.deltaTime;
            updateClockText();
        }
    }

    // Start timer when stage starts
    public void startClock()
    {
        isRunning = true;
    }

    // Stop timer when stage ends
    public void stopClock()
    {
        isRunning = false;
    }

    private void updateClockText()
    {
        int min = Mathf.FloorToInt(timeTaken / 60F);
        int sec = Mathf.FloorToInt(timeTaken % 60F);
        clockText.text = string.Format("{0:0}:{1:00}", min, sec);
    }
}
