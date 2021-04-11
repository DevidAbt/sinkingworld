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
        BoxCollider2D[] platforms = platformPool.GetActiveItemPool().ToArray();
        original_position = platforms[2].transform.position + new Vector3(0, 3, 0);

        base.Respawn();

        this.GetComponent<PlayerHurt>().life = original_life;
    }

}
