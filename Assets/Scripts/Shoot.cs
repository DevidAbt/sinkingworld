using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Rigidbody2D itemProto = null;
    public float speed = 30.0f;
    public float vertivalSpeed = 3.0f;
    public Transform firePoint;

    public int itemPoolSize = 10;
    public List<Rigidbody2D> itemPool;

    // Start is called before the first frame update
    void Start()
    {
        itemPool = new List<Rigidbody2D>();
        for (int i = 0; i < itemPoolSize; i++)
        {
            Rigidbody2D itemClone = Instantiate(itemProto);
            itemClone.gameObject.SetActive(false);
            itemPool.Add(itemClone);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            throwItem();
        }
    }

    void throwItem()
    {
        Rigidbody2D itemClone = getItemFromPool();
        if (itemClone)
        {
            itemClone.transform.position = firePoint.position;
            itemClone.gameObject.SetActive(true);

            int direction = CharacterMovement.facingRight ? 1 : -1;

            itemClone.velocity = transform.right * speed * direction + transform.up * vertivalSpeed;
        }
    }

    private Rigidbody2D getItemFromPool() {
        return itemPool.FirstOrDefault(x => !x.gameObject.activeSelf);
    }
}
