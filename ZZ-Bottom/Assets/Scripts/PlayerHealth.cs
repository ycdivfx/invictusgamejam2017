using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using zzbottom.helpers;

public class PlayerHealth : MonoBehaviour
{
	public float Amount = 50;

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag.ToLower () == "player") 
		{
			col.gameObject.GetComponent<PlayerController>().Health += Amount;
			Destroy(gameObject);
		}
	}
}
