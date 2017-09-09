using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 2f;

    public bool up = true;
    public float maxH = 10;
    public float minH = -10;
    public float inc = 2f;

    ///Velocity: o speed aplica uma velocidade constante (aparenta ignorar o valor de 'speed') ao objeto e ignora a massa
    public void FowardSpeed()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
    }
    public void DiagonalSpeed()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, speed);
    }
    ///AddForce: por causa da massa do objeto, o X cresce mais muito mais depressa do que o Y
    public void Diagonal()
    {
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(.001f, .001f));
    }

    void Start()
    {
        up = true;

        //if player jumps
        maxH = this.gameObject.transform.position.y + maxH;
        minH = this.gameObject.transform.position.y - minH;
    }

    void Update()
    {
        Debug.Log(this.gameObject.transform.position.y);
        GravityPull();
    }


    public void GravityPull()
    {
        if (up)
        {
            if (this.gameObject.transform.position.y < maxH)
                this.gameObject.transform.position += new Vector3(0, speed, 0);
            else
                up = !up;
        }
        else
        {

        }
    }
}
