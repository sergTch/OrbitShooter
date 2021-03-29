using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : Bullet
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Time.deltaTime * Core.game.gameSpeed * Core.game.bonusSpeed;
    }
    public override void Activate()
    {
        Core.game.SlowDown();
    }
}
