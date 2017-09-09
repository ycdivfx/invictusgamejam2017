using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 0.1f;

    public bool up = true;
    public float maxH = 3f;
    public float minH = -3f;
    public float inc = .002f;

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
    public float pingPong;

    void Start()
    {
        up = true;

        //if player jumps
        maxH = this.gameObject.transform.position.y + maxH;
        minH = this.gameObject.transform.position.y - minH;
    }

    void Update()
    {
        PingPong();
    }
   
    public void Normal()
    {
        transform.position = new Vector3((transform.position.x + inc), transform.position.y, transform.position.z);
    }

    public void PingPong()
    {
        if (transform.position.x <= 3)
        {
            transform.position = new Vector3(transform.position.x + inc, Mathf.PingPong(Time.time*2, 3), transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x + inc, Mathf.PingPong(Time.time*2, 3), transform.position.z);
        }
    }

    public void PingPong2()
    {
        if (transform.position.x <= 1)
        {
            pingPong = Mathf.PingPong(Time.time, 1);
        }
        else if (transform.position.x >= 1)
        {
            pingPong = -Mathf.PingPong(Time.time, 1);
        }
        transform.localPosition = new Vector3(pingPong, Mathf.PingPong(Time.time, 2), transform.position.z);
    }
        



    //update is to fast to see
    public void GravityPull()
    {
        Debug.Log(this.gameObject.transform.position.y);

        if (up)
        {
            if (this.gameObject.transform.position.y < maxH)
                this.gameObject.transform.position += new Vector3(0, speed, 0);
            else
            {
                up = false;
                Debug.Log("I'm ON TOP");
            }
        }
        else
        {
            if (this.gameObject.transform.position.y > minH)
                this.gameObject.transform.position += new Vector3(0, -speed, 0);
            else
                up = true;
        }
    }
}
