using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JogarButton : MonoBehaviour
{

    public void onClick()
    {
        SceneManager.LoadScene("level_01");
    }
}
