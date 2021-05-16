using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHurt : MonoBehaviour
{
    public int lives = 3;
    public Transform fallDeathCheck;
    public Text livesText;
    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreManager>();
    }

    void Update()
    {
        if (transform.position.y < fallDeathCheck.position.y)
        {
            hurt();
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<RespawnManager>().reset();
        }

        return;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            hurt();
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<RespawnManager>().reset();
        }
    }

    void hurt()
    {
        AudioManager.instance.PlaySound("PlayerDeath");
        lives--;
        livesText.text = lives.ToString();
        if (lives < 1)
        {
            die();
        }
    }

    public void die()
    {
        AudioManager.instance.StopSound("DarkFactory");
        AudioManager.instance.PlaySound("MenuMusic");
        scoreManager.updateLastScore();
        SceneManager.LoadScene(2);
        CharacterMovement.facingRight = true;
    }

}
