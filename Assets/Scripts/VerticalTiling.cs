using UnityEngine;

public class VerticalTiling : MonoBehaviour
{
    enum Side
    {
        Top,
        Bottom
    }

    public int offsetY = 2;
    public int paddingY = 2;
    public int bgPrefabHeight = 8;

    public bool hasATopBuddy = false;
    public bool hasABottonBuddy = false;

    public bool reverseScale = false;

    private float spriteHeight = 0f;
    private Camera cam;
    private Transform myTransform;

    void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }

    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteHeight = Mathf.Abs(sRenderer.sprite.bounds.size.y * myTransform.localScale.y);
    }

    void Update()
    {
        if (!hasATopBuddy || !hasABottonBuddy)
        {
            float camVerticalExtend = cam.orthographicSize * Screen.height / Screen.width;

            float edgeVisiblePositionTop = (myTransform.position.y - spriteHeight / 2) + camVerticalExtend;
            float edgeVisiblePositionBottom = (myTransform.position.y + spriteHeight / 2) - camVerticalExtend;

            if (cam.transform.position.y <= edgeVisiblePositionTop - offsetY + paddingY && !hasATopBuddy)
            {
                makeNewBuddy(Side.Top);
                hasATopBuddy = true;
            }
            else if (cam.transform.position.y >= edgeVisiblePositionBottom - offsetY - paddingY && !hasABottonBuddy)
            {
                makeNewBuddy(Side.Bottom);
                hasABottonBuddy = true;
            }
        }
    }

    void makeNewBuddy(Side side)
    {
        Vector3 newPosition = new Vector3(myTransform.position.x,
                                          myTransform.position.y + spriteHeight * bgPrefabHeight * (side == Side.Top ? -1 : +1),
                                          myTransform.position.z);

        Transform newBuddy = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;

        if (reverseScale)
        {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x,
                                              newBuddy.localScale.y * -1,
                                              newBuddy.localScale.z);
        }

        newBuddy.parent = myTransform.parent;
        switch (side)
        {
            case Side.Top:
                newBuddy.GetComponent<VerticalTiling>().hasABottonBuddy = true;
                break;
            case Side.Bottom:
                newBuddy.GetComponent<VerticalTiling>().hasATopBuddy = true;
                break;
        }
    }
}