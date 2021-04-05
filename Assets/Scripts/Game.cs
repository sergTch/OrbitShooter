using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public bool inProcess = false;

    public float gameSpeed = 1;
    public float bulletSpeed = 5;
    public float bonusSpeed = 5;
    public int score = 0;
    public int prevScore = 0;

    public float speed = 1;
    public float fi = 0;
    public float time = 0;
    public float spawnSpeed = 0.4f;
    public int coinsN;
    public Bullet bullet;
    public Player player;
    public Coin coin;
    public Coin[] coins;

    public Bullet shield;
    public Bullet freeze;

    public int toBonus;
    public Bullet[] bonuses;

    public float slowTime = 0;
    public static Vector3 startPos;

    public AudioClip flipClip;
    //public cheeze;

    void Start()
    {
        startPos = new Vector3(4, 0);
        player = Instantiate(player, startPos, Quaternion.identity);

        Bullet.colors = new Color32[6];
        Bullet.colors[0] = new Color32(255, 0, 0, 255);
        Bullet.colors[1] = new Color32(0, 0, 255, 255);
        Bullet.colors[2] = new Color32(0, 255, 0, 255);
        Bullet.colors[3] = new Color32(255, 255, 0, 255);
        Bullet.colors[4] = new Color32(0, 255, 255, 255);
        Bullet.colors[5] = new Color32(255, 0, 255, 255);
        score = 0;
        prevScore = 0;
        setConstants();
        Restart();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inProcess)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
            SlowDown();

        if (spawnSpeed < time) {
            if (bonuses.Length > 0 && toBonus == 0)
            {
                SpawnBullet(bonuses[Random.Range(0, bonuses.Length)]);
                toBonus = Random.Range(7, 12) * 2 / bonuses.Length;
            }
            else {
                SpawnBullet(bullet);

                if (bonuses.Length > 0)
                    toBonus--;
            }
            time = 0;
        }

        time += Time.deltaTime * gameSpeed;
        if (Input.GetMouseButtonDown(0))
        {
            speed = -speed;
            Core.menu.audioSource.PlayOneShot(flipClip, 0.2f * Core.menu.volume);
        }
        SetPlayerPosition();
    }

    public void SpawnBullet(Bullet bPref)
    {
        Bullet b = Instantiate(bPref, new Vector3(0, 0), Quaternion.identity);

        float dfi;
        dfi = Random.Range(1f, 1.3f) - Random.Range(0, 2);
        //dfi = Random.Range(-1f, 2f);
        dfi *= Mathf.Sign(speed);
        b.speed = new Vector3(Mathf.Cos(fi + dfi), Mathf.Sin(fi + dfi));
        b.transform.rotation = Quaternion.Euler(Vector3.forward * ((fi + dfi) / Mathf.PI * 180 - 90));
    }

    public void Restart()
    {
        prevScore = score;
        if (score > 0 && Core.menu.topScore < score)
        {
            Core.menu.topScore = score;
            PlayerPrefs.SetInt("topScore", score);
        }

        inProcess = false;
        setConstants();
        SetPlayerPosition();
        makeCoins();

        if (Core.menu != null)
            Core.menu.startB.gameObject.active = true;
    }

    public void LoadBonuses()
    {
        if (Core.menu.freezeTime > 0 && Core.menu.shieldTime > 0)
            bonuses = new Bullet[] { shield, freeze };
        else if (Core.menu.freezeTime > 0)
            bonuses = new Bullet[] { freeze };
        else if (Core.menu.shieldTime > 0)
            bonuses = new Bullet[] { shield };
        else bonuses = new Bullet[] { };
    }

    public void SlowDown()
    {
        if (slowTime == 0)
            StartCoroutine(slowDown());
        else
            slowTime = 4;
    }

    public IEnumerator slowDown()
    {
        bulletSpeed = 5 / 2;
        slowTime = 4;
        while (slowTime > 0)
        {
            slowTime -= Time.deltaTime;
            yield return null;
        }
        slowTime = 0;
        bulletSpeed = 5;
        yield return null;
    }

    public IEnumerator delayRestart(float t)
    {
        prevScore = score;
        inProcess = false;

        if (score > 0 && Core.menu.topScore < score)
        {
            Core.menu.topScore = score;
            PlayerPrefs.SetInt("topScore", score);
        }

        yield return new WaitForSeconds(t);
        Restart();
        yield return null;
    }

    public void makeCoins()
    {
        coinsN = 30;

        if (coins != null)
            for (int i = 0; i < coins.Length; i++)
            {
                if (coins[i] != null)
                    Destroy(coins[i].gameObject);
            }

        coins = new Coin[coinsN];

        for (int i = 0; i < coinsN; i++)
        {
            coins[i] = Instantiate(coin, Quaternion.Euler(0f, 0f, 360 / coinsN * i) * startPos, Quaternion.identity);
        }
    }

    public void SetPlayerPosition()
    {
        float x = startPos.x;
        float y = startPos.y;
        fi += speed * Time.deltaTime * gameSpeed;
        player.transform.position = new Vector3(x * Mathf.Cos(fi) - y * Mathf.Sin(fi), x * Mathf.Sin(fi) + y * Mathf.Cos(fi));
        player.transform.rotation = Quaternion.Euler(Vector3.forward * ((fi + Mathf.PI/2 * Mathf.Sign(speed)) / Mathf.PI * 180 - 90));
    }

    void setConstants()
    {
        startPos = new Vector3(4, 0);
        speed = -1.5f;
        score = 0;
        //gameSpeed = 1;
        bulletSpeed = 5;
        bonusSpeed = 3.5f;
        toBonus = 5;
        time = 0;
        fi = Mathf.PI / 2;
        spawnSpeed = 0.4f;
    }

    public void SetGameSpeed()
    {
        gameSpeed = 2 - 1.2f / (score / 30 / 17.289f + 1);
    }

    public void Play()
    {
        inProcess = true;
        LoadBonuses();
    }
}
