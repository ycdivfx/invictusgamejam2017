using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparks : MonoBehaviour {

    public float randomMin;
    public float randomMax;

    void Start () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(randomMin, randomMax), Random.Range(randomMin, randomMax));
    }
}
