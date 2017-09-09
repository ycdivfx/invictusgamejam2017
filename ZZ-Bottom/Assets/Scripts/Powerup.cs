using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private int m_numberOfShoots = 2;

    public UnityEvent OnOutOfShoots = new UnityEvent(); 

    public int NumberOfShoots
    {
        get { return m_numberOfShoots; }
        set
        {
            m_numberOfShoots = value;
            if(m_numberOfShoots <= 0)
                OnOutOfShoots.Invoke();
        }
    }
}
