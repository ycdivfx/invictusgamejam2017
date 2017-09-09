using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWorksBullet : BaseBullet
{
    public float maxH;

    private void Update()
    {
        //Debug.Log(GetComponent<Rigidbody2D>().position.y);
        if (GetComponent<Rigidbody2D>().position.y >= maxH)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            foreach (Transform child in transform)
            {
                Debug.Log(child.name);
                child.gameObject.SetActive(true);
            }
            Destroy(gameObject, 1f);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, Speed);
        }
    }
}
