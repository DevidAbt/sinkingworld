using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformPool : MonoBehaviour
{
    public BoxCollider2D itemProto = null;

    private Vector3 startPosition;
    public Vector3 bias = new Vector3(0, 0, 0);

    public int itemPoolSize = 1;
    private Stack<BoxCollider2D> activeItemPool;
    private Stack<BoxCollider2D> passiveItemPool;

    private float nextActionTime = 0.0f;
    public float period = 1f;
    public int newRowTick = 3;
    private int tick = 0;

    public float ascendingDist = 3;
    private float platformWidth;
    private System.Random random;

    // Start is called before the first frame update
    void Start()
    {
        GameObject platformStart = GameObject.FindGameObjectWithTag("PlatformStart");
        startPosition = platformStart.transform.position;
        startPosition += bias;

        Quaternion startRotation = new Quaternion();

        platformWidth = itemProto.GetComponent<SpriteRenderer>().bounds.size.x * 3 + 1;

        random = new System.Random();

        activeItemPool = new Stack<BoxCollider2D>();
        passiveItemPool = new Stack<BoxCollider2D>();
        for (int i = 0; i < itemPoolSize; i++)
        {
            // Vector3 position = new Vector3(startTransform.position.x, startTransform.position.y, startTransform.position.z);
            BoxCollider2D boxCollider = Instantiate(itemProto, startPosition, startRotation);
            boxCollider.gameObject.SetActive(false);
            passiveItemPool.Push(boxCollider);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            tick++;

            Debug.Log($"Tick: {tick}");
            if (tick % 3 == 1)
            {
                BoxCollider2D newCollider = passiveItemPool.Pop();
                if (newCollider)
                {
                    newCollider.gameObject.SetActive(true);
                    int randomPos = this.random.Next(0, 2);
                    newCollider.transform.position = new Vector3(startPosition.x + platformWidth * randomPos, startPosition.y, startPosition.z);
                    activeItemPool.Push(newCollider);
                    Debug.Log($"Added platform ({tick})");
                }
            }

            foreach (BoxCollider2D item in activeItemPool)
            {
                item.transform.position += new Vector3(0, -ascendingDist, 0);
            }

        }
    }

    public Stack<BoxCollider2D> getActiveItemPool() {
        return activeItemPool;
    }
}
