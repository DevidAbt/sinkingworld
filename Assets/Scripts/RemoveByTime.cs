using UnityEngine;

public class RemoveByTime : MonoBehaviour
{
    public float timeLimit = 2.0f;
    public float timeElapsed = 0.0f;

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > timeLimit)
        {
            gameObject.SetActive(false);
            timeElapsed = 0;
        }
    }
}