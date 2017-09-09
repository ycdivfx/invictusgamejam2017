using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWorksBullet : BaseBullet
{
    public float maxH;

    protected override void OnShoot()
    {
        GetComponent<Rigidbody2D>().angularVelocity = 5f;
    }

    private void Update()
    {
        if (GetComponent<Rigidbody2D>().position.y >= maxH)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<SpriteRenderer>().enabled = false ; 
            foreach (Transform child in transform)
                child.gameObject.SetActive(true);
            Destroy(gameObject, 1f);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, Speed);
        }
    }
}
