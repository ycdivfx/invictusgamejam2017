using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killswitch : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D colObj) 
	{
		if (colObj.gameObject.tag.ToLower() == "enemy")
			Destroy(colObj.gameObject);
	}
}
