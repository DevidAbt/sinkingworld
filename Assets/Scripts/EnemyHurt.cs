using UnityEngine;

public class EnemyHurt : MonoBehaviour
{
    private Animator animator;
    private Transform fallDeathCheck;
    private ParticleSystem particleSystem;


    void Start()
    {
        animator = this.GetComponent<Animator>();
        fallDeathCheck = GameObject.FindGameObjectWithTag("PlatformEnd").transform;
        particleSystem = GetComponent<ParticleSystem>();
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
        if (col.gameObject.tag == "Weapon")
        {
            particleSystem.Play();
            animator.SetTrigger("Fall");
            this.GetComponent<CapsuleCollider2D>().isTrigger = true;
        }
    }

}
