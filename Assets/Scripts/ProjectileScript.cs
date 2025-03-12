using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{

    private Rigidbody2D rigidbody2d;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update() {

        if (transform.position.magnitude > 30.0f) {

            Destroy(gameObject);

        }
        
    }

    public void Launch(Vector2 direction, float force) { //varijable za smjer i brzinu projektila

        rigidbody2d.AddForce(direction * force); //smjer * sila udarca odnosno brzina projektila
    
    }

    void OnTriggerEnter2D(Collider2D other) { //na sudaru s objektom odnosno neprijateljem aktivira se 
        EnemyController enemy = other.GetComponent<EnemyController>(); //uzima skriptu enemycontroller iz objekta s kojim je doslo do kontakta+

        if (enemy!= null) {

            enemy.Fix(); //metoda fix iz skripte enemycontroller se poziva ako je utvrdjen kontakt projektila s neprijateljem
        
        }

        Destroy(gameObject); //unistava se sprite projektila pri sudaru s neprijateljem
    }

    void OnCollisionEnter2D(Collision2D other) {

        Destroy(gameObject); //unistava projektil kada dojde do kontakta s bilo cime sta nije enemy tip
    
    }


}
