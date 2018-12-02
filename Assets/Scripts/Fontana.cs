using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fontana : MonoBehaviour {

  
    public GameObject player;
    private float hps=25f;

	
	void FixedUpdate () {

        if (player)
            player.GetComponent<Health>().healHealth(hps * Time.deltaTime);

    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player") 
        {
           
            player = collider.gameObject;
         
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            player = null;
         
        }
    }

}
