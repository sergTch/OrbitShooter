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

    public bool freeze, shield;

    public string reward;
    public Animator curtain;

    bool shownSettings = false;

    void Start()
    {
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
    }

    public void LoadPurchases()
    {
        Debug.Log("LoadPurchases");
        string str = PlayerPrefs.GetString("freeze");
        if (str != "")
        {
            TimeSpan timeLeft = DateTime.Parse(str) - DateTime.Now;
            if (timeLeft > TimeSpan.FromSeconds(5))
            {
                freeze = true;
                StartCoroutine(SetTimer(freezeTimer, timeLeft.Seconds + timeLeft.Minutes * 60));
            }
        }
        str = PlayerPrefs.GetString("shield");
        if (str != "")
        {
            TimeSpan timeLeft = DateTime.Parse(str) - DateTime.Now;
            if (timeLeft > TimeSpan.FromSeconds(5))
            {
                shield = true;
                StartCoroutine(SetTimer(shieldTimer, timeLeft.Seconds + timeLeft.Minutes * 60));
            }
        }
    }

    void Update()
    {
        
    }

    public void ShowMenu()
    {
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

    public IEnumerator Activate(float t)
    {
        yield return new WaitForSeconds(t);
        startB.gameObject.active = true;
        yield return null;
    }

    public IEnumerator SetTimer(Text text, int t)
    {
        while (t > 0)
        {
            text.text = ((t / 60) % 100).ToString() + ":" + (t % 60).ToString();
            yield return new WaitForSeconds(1);
            t--;
        }
        text.text = "";
        LoadPurchases();
        yield return null;
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
