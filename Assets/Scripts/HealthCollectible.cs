using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthCollectible : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other) {

        PlayerController controller = other.GetComponent<PlayerController>(); //sluzi da se aktivira skripta samo kada u koliziju dodje drugi game object koji ima playercontroller scriptu znaci nece npc i enemii

        /*if (controller != null) {
            if (controller.currentHealth < controller.maxHealth)
            {
                controller.ChangeHealth(1);
                Destroy(GameObject);


            }
        }*/ //nested ifovi, ne bas uvijek nice za koristit


        if(controller != null && controller.health < controller.maxHealth)
        {
            controller.ChangeHealth(1);
            Destroy(gameObject);

        }


    
    
    }



    
}
