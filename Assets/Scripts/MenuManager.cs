using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private PlatformPool platformPool;
    private EnemyAI enemyAI;
    private Rigidbody2D sawRigidBody;
    private float lastSpeed = 250;
    private GameObject resumeButton;

    void Start()
    {
        platformPool = GetComponent<PlatformPool>();
        GameObject saw = GameObject.Find("Saw");
        enemyAI = saw.GetComponent<EnemyAI>();
        sawRigidBody = saw.GetComponent<Rigidbody2D>();
        resumeButton = GameObject.Find("resume");
        resumeButton.SetActive(false);
    }

    public void switchToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void switchToGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void pauseGame()
    {
        platformPool.paused = true;
        lastSpeed = enemyAI.speed;
        enemyAI.speed = 0;
        sawRigidBody.velocity = Vector2.zero;
        sawRigidBody.isKinematic = true;
        resumeButton.SetActive(transform);
    }

    public void resumeGame()
    {
        platformPool.paused = false;
        enemyAI.speed = lastSpeed;
        sawRigidBody.isKinematic = false;
        resumeButton.SetActive(false);
    }

    public void quit()
    {
        Application.Quit();
    }

}
