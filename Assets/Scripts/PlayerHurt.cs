using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : MonoBehaviour
{
    public int life = 1;
    public Transform fallDeathCheck;

    void Start()
    {
    }

    void Update()
    {
        if(transform.position.y < fallDeathCheck.position.y) {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<RespawnManager>().reset();
        }

        return;
    }

    // void OnCollisionEnter2D(Collision2D col) {
    //     if(col.gameObject.tag=="Enemy") {
    //         life--;

    //         if(life <= 0) {
    //             GameObject.FindGameObjectWithTag("GameManager").GetComponent<RespawnManager>().reset();
    //         }
    //     }
    // }

}
