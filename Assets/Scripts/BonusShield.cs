using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusShield : Bullet
{
    private void Start()
    {
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * Core.game.gameSpeed * Core.game.bonusSpeed;
    }

    public override void Activate()
    {
        Core.game.player.shield.Appear();
    }
}
