using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    protected Vector3 original_position;
    private bool original_active;

    private RespawnManager rm;
    
    protected virtual void Start()
    {
        original_position = this.transform.position;
        original_active = this.gameObject.activeSelf;

        rm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RespawnManager>();
        rm.register(this);
    }

    public virtual void Respawn() {
        this.transform.position = original_position;
        this.gameObject.SetActive(original_active);
    }
}
