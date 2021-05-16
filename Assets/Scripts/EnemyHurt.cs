using UnityEngine;

public class EnemyHurt : MonoBehaviour
{
    private Animator animator;
    private Transform fallDeathCheck;
    private ParticleSystem particleSystem;
    private ScoreManager scoreManager;


    void Start()
    {
        animator = this.GetComponent<Animator>();
        fallDeathCheck = GameObject.FindGameObjectWithTag("PlatformEnd").transform;
        particleSystem = GetComponent<ParticleSystem>();
        scoreManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreManager>();
    }
    void Update()
    {
        if (transform.position.y < fallDeathCheck.position.y)
        {
            CapsuleCollider2D capsuleCollider = this.GetComponent<CapsuleCollider2D>();
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlatformPool>().ZombieDie(capsuleCollider);
        }

        return;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Weapon" || col.gameObject.name == "Saw")
        {
            AudioManager.instance.PlaySound("ZombieDeath");
            scoreManager.addScore(5);
            particleSystem.Play();
            animator.SetTrigger("Fall");
            this.GetComponent<CapsuleCollider2D>().isTrigger = true;
        }
    }

}
