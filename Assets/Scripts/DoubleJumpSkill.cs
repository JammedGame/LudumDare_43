using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleJumpSkill : MonoBehaviour {

        public GameObject player;
        public GameObject skill;

    void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.tag == "Player")
            {
                 player = collider.gameObject;
                 player.GetComponent<PlayerMovement>().jumpsAllowed =2;

           
                 var image = skill.GetComponent<Image>();
                 var tempColor = image.color;
                 tempColor.a = 1f;
                 image.color = tempColor;

                 Destroy(gameObject);
            }
        }
}

    




