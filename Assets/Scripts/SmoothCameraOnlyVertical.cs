using UnityEngine;

public class SmoothCameraOnlyVertical : MonoBehaviour
{
    public float speed = 0.01f;
    public Transform target;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = target.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 anchorPos = transform.position + offset;
            // Vector3 movement = target.position - anchorPos;
            Vector3 movement = new Vector3(0,
                                           target.position.y - anchorPos.y,
                                           0);

            Vector3 newCamPoint = transform.position + movement * speed;
            transform.position = newCamPoint;
        }
    }
}
