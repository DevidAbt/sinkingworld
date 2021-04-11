using UnityEngine;

public class RemoveByTime : MonoBehaviour
{
    public float timeLimit = 2.0f;
    public float timeElapsed = 0.0f;
    private CircleCollider2D circleCollider;

    void Start()
    {
        circleCollider = this.GetComponent<CircleCollider2D>();
    }
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > timeLimit)
        {
            gameObject.SetActive(false);
            circleCollider.isTrigger = false;
            timeElapsed = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            circleCollider.isTrigger = true;
        }
    }
}