using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public int topScore;
    public int sessions;
    public float delay;
    public bool voice;
    public bool pause;

    public Button settings;
    public Button startB;
    public Text scoreText;
    public Text topScoreText;
    public Text freezeTimer;
    public Text shieldTimer;

    public Button freezeButton, shieldButton;
    public string reward;

    public Animator curtain;
    public AudioSource audioSource;
    public AudioClip flashClip;
    public float volume;

    public int gamesToAd;

    public int freezeTime, shieldTime;
    bool shownSettings = false;

    void Start()
    {
        gamesToAd = 5;
        volume = 1;
        Application.targetFrameRate = 60;
        delay = 0.25f;
        pause = false;
        //curtain.Play("background");
        topScore = PlayerPrefs.GetInt("topScore");
        sessions = PlayerPrefs.GetInt("sessions");
        topScoreText.text = "top: " + topScore.ToString();
        scoreText.text = "0";
        LoadPurchases();
        if (Camera.main.aspect < 1)
            Camera.main.orthographicSize = Camera.main.orthographicSize / Camera.main.aspect;
        StartCoroutine(RunTimers());
    }

    public void LoadPurchases()
    {
        Debug.Log("LoadPurchases");
        string str = PlayerPrefs.GetString("freeze");
        if (str != "")
        {
            TimeSpan timeLeft = DateTime.Parse(str) - DateTime.Now;
            if (timeLeft > TimeSpan.FromSeconds(5))
                freezeTime = timeLeft.Seconds + timeLeft.Minutes * 60;
        }
        str = PlayerPrefs.GetString("shield");
        if (str != "")
        {
            TimeSpan timeLeft = DateTime.Parse(str) - DateTime.Now;
            if (timeLeft > TimeSpan.FromSeconds(5))
                shieldTime = timeLeft.Seconds + timeLeft.Minutes * 60;
        }
    }

    void Update()
    {
        
    }

    public void ShowMenu()
    {
        gamesToAd--;
        if (gamesToAd == 0)
        {
            gamesToAd = 5;
            GetComponent<MobAdsSimple>().ShowAd();
        }
        StartCoroutine(ChangeText(topScoreText, "top: " + topScore.ToString(), delay));
    }
    public void HideMenu()
    {
        startB.gameObject.active = false;
        scoreText.text = Core.game.score.ToString();
    }

    public void Play()
    {
        HideMenu();
        Core.game.Play();
    }

    public void PressSettings()
    {
        if (shownSettings)
            HideSettings();
        else OpenSettings();
    }

    public void OpenSettings()
    {
        shownSettings = true;
    }

    public void getReward(string rewardStr)
    {
        reward = rewardStr;
    }

    public void HideSettings()
    {
        shownSettings = false;
    }

    public void VolumeOn()
    {
        volume = 1;
    }

    public void VolumeOf()
    {
        volume = 0;
    }

    public IEnumerator Activate(float t)
    {
        yield return new WaitForSeconds(t);
        startB.gameObject.active = true;
        yield return null;
    }

    public IEnumerator RunTimers()
    {
        while (true)
        {
            freezeTime--;
            shieldTime--;
            if (freezeTime > 0)
            {
                freezeButton.interactable = false;
                freezeTimer.text = (freezeTime / 60 % 100).ToString() + ":" + (freezeTime % 60).ToString();
            }
            else
            {
                freezeButton.interactable = true;
                freezeTimer.text = "SLOW";
            }
            if (shieldTime > 0)
            {
                shieldButton.interactable = false;
                shieldTimer.text = (shieldTime / 60 % 100).ToString() + ":" + (shieldTime % 60).ToString();
            }
            else
            {
                shieldButton.interactable = true;
                shieldTimer.text = "SHIELD";
            }
            yield return new WaitForSeconds(1);
        }
    }

    public IEnumerator ChangeText(Text text, string s, float t)
    {
        if (text.text != s)
        {
            yield return StartCoroutine(FadeTextToZeroAlpha(t / 2, text));
            text.text = s;
            yield return StartCoroutine(FadeTextToFullAlpha(t / 2, text));
        }
    }

    public IEnumerator HideText(Text text, float t)
    {
        yield return StartCoroutine(FadeTextToZeroAlpha(t / 2, text));
        text.text = "";
    }

    public IEnumerator ShowText(Text text, string s, float t)
    {
        yield return new WaitForSeconds(t);
        text.text = s;
        yield return StartCoroutine(FadeTextToFullAlpha(t / 2, text));
    }

    public IEnumerator FadeTextToFullAlpha(float t, Graphic gr)
    {
        gr.color = new Color(gr.color.r, gr.color.g, gr.color.b, 0);
        for (float i = 0; i < 1; i += Time.deltaTime / t)
        {
            gr.color = new Color(gr.color.r, gr.color.g, gr.color.b, gr.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Graphic gr)
    {
        gr.color = new Color(gr.color.r, gr.color.g, gr.color.b, 1);
        for (float i = 0; i < 1; i += Time.deltaTime / t)
        {
            gr.color = new Color(gr.color.r, gr.color.g, gr.color.b, gr.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
