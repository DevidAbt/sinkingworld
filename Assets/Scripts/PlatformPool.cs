using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformPool : MonoBehaviour
{
    public BoxCollider2D platformProto = null;
    public bool paused = false;

    private Vector3 startPosition;
    private Vector3 endPosition;
    public Vector3 bias = new Vector3(0, 0, 0);

    public int platformPoolSize = 1;
    private Queue<BoxCollider2D> activeItemPool;
    private Queue<BoxCollider2D> passiveItemPool;

    private float nextActionTime = 0.0f;
    public float period = 1f;
    public int newRowTick = 3;
    private int tick = 0;
    public int initTicks = 10;

    public float ascendingDist = 3;
    private float platformWidth;
    private System.Random random;
    private int lastRandomPosition = 1;
    private bool startPlatformDestroyed = false;

    public CapsuleCollider2D zombieProto;
    public int zombiePoolSize;
    private List<CapsuleCollider2D> activeZombiePool;
    private Queue<CapsuleCollider2D> passiveZombiePool;
    public float zombieChance;

    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject platformStart = GameObject.FindGameObjectWithTag("PlatformStart");
        startPosition = platformStart.transform.position;
        startPosition += bias;

        GameObject platformEnd = GameObject.FindGameObjectWithTag("PlatformEnd");
        endPosition = platformEnd.transform.position;

        Quaternion startRotation = new Quaternion();

        platformWidth = platformProto.GetComponent<SpriteRenderer>().bounds.size.x * 3 + 1;

        random = new System.Random();

        activeItemPool = new Queue<BoxCollider2D>();
        passiveItemPool = new Queue<BoxCollider2D>();
        for (int i = 0; i < platformPoolSize; i++)
        {
            // Vector3 position = new Vector3(startTransform.position.x, startTransform.position.y, startTransform.position.z);
            BoxCollider2D boxCollider = Instantiate(platformProto, startPosition, startRotation);
            boxCollider.gameObject.SetActive(false);
            passiveItemPool.Enqueue(boxCollider);
        }

        activeZombiePool = new List<CapsuleCollider2D>();
        passiveZombiePool = new Queue<CapsuleCollider2D>();
        for (int i = 0; i < zombiePoolSize; i++)
        {
            CapsuleCollider2D zombie = Instantiate(zombieProto, startPosition, startRotation);
            zombie.gameObject.SetActive(false);
            passiveZombiePool.Enqueue(zombie);
        }

        scoreManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreManager>();

        AudioManager.instance.StopSound("MenuMusic");
        AudioManager.instance.PlaySound("DarkFactory");
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused && (Time.time > nextActionTime || tick <= initTicks))
        {

            // Debug.Log($"Tick: {tick}");
            if (tick % newRowTick == 1)
            {
                BoxCollider2D newCollider = passiveItemPool.Dequeue();
                if (newCollider)
                {
                    newCollider.gameObject.SetActive(true);
                    int randomPos;
                    do
                    {
                        randomPos = this.random.Next(0, 3);
                    }
                    while (randomPos == lastRandomPosition);
                    lastRandomPosition = randomPos;
                    Vector3 platformPostition = new Vector3(startPosition.x + platformWidth * randomPos, startPosition.y, startPosition.z);
                    newCollider.transform.position = platformPostition;
                    activeItemPool.Enqueue(newCollider);
                    // Debug.Log($"Added platform ({tick})");

                    if (random.NextDouble() < zombieChance && tick > initTicks)
                    {
                        CapsuleCollider2D zombie = passiveZombiePool.Dequeue();
                        if (zombie)
                        {
                            zombie.isTrigger = false;
                            zombie.transform.position = platformPostition + new Vector3(0, 3, 0);
                            zombie.gameObject.SetActive(true);
                            activeZombiePool.Add(zombie);
                        }
                    }
                }
            }

            bool removeOne = false;
            foreach (BoxCollider2D item in activeItemPool)
            {
                item.transform.position += new Vector3(0, -ascendingDist, 0);

                if (item.transform.position.y < endPosition.y)
                {
                    removeOne = true;
                }
            }
            if (removeOne)
            {
                BoxCollider2D removedCollider = activeItemPool.Dequeue();
                removedCollider.gameObject.SetActive(false);
                passiveItemPool.Enqueue(removedCollider);

                if (!startPlatformDestroyed)
                {
                    Destroy(GameObject.FindGameObjectWithTag("StartPlatform"));
                    startPlatformDestroyed = true;
                }
            }

            tick++;
            if (tick > initTicks)
            {
                nextActionTime = Time.time + period;
                if (tick % 3 == 0) {
                    scoreManager.addScore(1);
                }
            }
        }
    }

    public Queue<BoxCollider2D> GetActiveItemPool()
    {
        return activeItemPool;
    }

    public void ZombieDie(CapsuleCollider2D capsuleCollider)
    {
        activeZombiePool.Remove(capsuleCollider);
        capsuleCollider.gameObject.SetActive(false);
        passiveZombiePool.Enqueue(capsuleCollider);
    }
}
