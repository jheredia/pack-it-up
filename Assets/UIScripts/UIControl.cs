using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIControl : MonoBehaviour
{
    //UI TMP_text element that shows the countdown
    public TMP_Text timerUIText;
    public TMP_Text timerUITextOutline;
    // UI TMP_Text element that shows setup and final words of timer
    public TMP_Text timerUIInfoText;
    public TMP_Text timerUIInfoTextOutline;
    //initial text shown before the timer starts it's countdown. Last str will be the message shown when timer ends
    public List<string> timerStartText;
    //speed at which timer initial text changes
    public float initTextTransitionTime = 1;
    //timer countdown time
    public float timerTime;
    private bool startCountdown;
    //temp variable to control timer (in the future another script should handle this)
    public bool startTimer;
    bool timerStarted = false;

    public UnityEvent timerFinished;

    //timer reference
    Timer timer;

    private void OnEnable()
    {
        timer = GetComponent<Timer>();
        timer.timerFinished.AddListener(TimerFinished);
    }


    private void Update()
    {
        if (startTimer)
        {
            if (!timerStarted)
            {
                timerStarted = true;
                UpdateTimerText("");
                StartCoroutine(InitTimer());
            }


            if (!startCountdown) { return; }

            if (timer.targetTime < 0)
            {
                UpdateTimerText("0");
            }
            else
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(timer.targetTime);
                int seconds = (int)timeSpan.TotalSeconds;
                int milliseconds = (int)((timer.targetTime - seconds) * 10);

                UpdateTimerText($"{seconds}.{milliseconds}");
            }
        }
    }

    public void StartTimer()
    {
        startTimer = true;
    }

    public void TogglePause()
    {
        timer.TogglePause();
    }

    private IEnumerator InitTimer()
    {
        timerUIInfoText.transform.parent.gameObject.SetActive(true);
        UpdateTimerInfoText(timerStartText[0]);
        yield return new WaitForSeconds(initTextTransitionTime);
        UpdateTimerInfoText(timerStartText[1]);
        yield return new WaitForSeconds(initTextTransitionTime);
        UpdateTimerInfoText(timerStartText[2]);
        yield return new WaitForSeconds(initTextTransitionTime);
        timerUIInfoText.transform.parent.gameObject.SetActive(false);
        timerUIText.transform.parent.gameObject.SetActive(true);
        timer.StartTimer(timerTime);
        startCountdown = true;
    }
    private void UpdateTimerText(string pNewText)
    {
        timerUIText.text = pNewText;
        timerUITextOutline.text = pNewText;
    }

    private void UpdateTimerInfoText(string pNewText)
    {
        timerUIInfoText.text = pNewText;
        timerUIInfoTextOutline.text = pNewText;
    }

    private void TimerFinished()
    {
        timerUIInfoText.transform.parent.gameObject.SetActive(true);
        startTimer = false;
        startCountdown = false;
        timerStarted = false;
        UpdateTimerInfoText(timerStartText[3]);
        timerFinished?.Invoke();
    }


}
