using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public List<Respawner> respawnableObjects;

    // Start is called before the first frame update
    void Awake()
    {
        respawnableObjects = new List<Respawner>();
    }

    public void reset() {
        Debug.Log("Resetting Game");
        foreach(Respawner resp in this.respawnableObjects) {
            resp.Respawn();
        }
    }

    public void register(Respawner resp) {
        this.respawnableObjects.Add(resp);
    }
}
