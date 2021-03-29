using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 speed;
    public static Color32[] colors;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = colors[Random.Range(0, 6)];
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Time.deltaTime * Core.game.gameSpeed * Core.game.bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(gameObject);
        //Activate();
        //Core.game.SetPlayerPosition();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        Activate();
    }

    public virtual void Activate()
    {
        //Core.game.Restart();
        if (!Core.game.player.shield.active)
        {
            Core.game.StartCoroutine(Core.game.delayRestart(1));
            Core.menu.ShowMenu();
        } else
        {
            Core.game.player.shield.Destroy();
        }
    }
}
