using UnityEngine;

public class EnemyHurt : MonoBehaviour
{
    private Animator animator;


    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Weapon")
        {
            animator.SetTrigger("Fall");
        }
    }

}
