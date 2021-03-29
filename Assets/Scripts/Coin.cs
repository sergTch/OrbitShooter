using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Animator animator;
    bool used;


    // Start is called before the first frame update
    void Start()
    {
        used = false;
        //animator.Play("Point");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (used)
            return;
        used = true;
        Core.game.coinsN--;
        if (Core.game.coinsN == 0)
        {
            Core.menu.curtain.Play("background");
            Core.game.makeCoins();
        }
        if (Core.game.inProcess)
        {
            Core.game.score++;
            Core.menu.scoreText.text = Core.game.score.ToString();
            Core.game.SetGameSpeed();
            Destroy(gameObject, 0.5f);
            animator.Play("Point");
        }
        else Destroy(gameObject);
    }
}
