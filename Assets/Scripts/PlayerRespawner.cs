using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawner : Respawner
{
    public int original_life;

    private PlatformPool platformPool;

    protected override void Start()
    {
        base.Start();
        original_life = this.GetComponent<PlayerHurt>().life;

        platformPool = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlatformPool>();
    }

    public override void Respawn() {
        original_position = platformPool.getActiveItemPool().Peek().transform.position;

        base.Respawn();

        this.GetComponent<PlayerHurt>().life = original_life;
    }

}
