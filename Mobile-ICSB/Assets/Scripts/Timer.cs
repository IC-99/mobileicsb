using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Image timerImage;
    public Image powerUpIcon;

    public PlayerPowerUp powerUpManager;
    private string tipoPowerUp;

    public float duration;
    private float remainingTime;

    void Start()
    {
        this.timerImage.enabled = false;
        this.powerUpIcon.enabled = false;
    }

    public void startTimer(float seconds, string tipo)
    {
        this.duration = seconds;
        this.tipoPowerUp = tipo;
        this.onStart(duration);
    }

    private void onStart(float seconds)
    {
        this.timerImage.enabled = true;
        this.powerUpIcon.enabled = true;
        this.remainingTime = seconds;
        StartCoroutine(updateTimer());
    }

    private IEnumerator updateTimer()
    {
        while(remainingTime > 0)
        {
            this.timerImage.fillAmount = Mathf.InverseLerp(0, duration, remainingTime);
            this.remainingTime = this.remainingTime - 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        onEnd();
    }

    private void onEnd()
    {
        this.timerImage.enabled = false;
        this.powerUpIcon.enabled = false;
        if (this.tipoPowerUp.Equals("DannoAumentato")) this.powerUpManager.setDannoAumentato(false);
        if (this.tipoPowerUp.Equals("FreqAumentata")) this.powerUpManager.setFreqAumentata(false);
        if (this.tipoPowerUp.Equals("TriploProiettile")) this.powerUpManager.setTriploProiettile(false);
    }
}
