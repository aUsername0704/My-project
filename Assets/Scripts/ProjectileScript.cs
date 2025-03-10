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

    public void Launch(Vector2 direction, float force) { //varijable za smjer i brzinu projektila

        rigidbody2d.AddForce(direction * force); //smjer * sila udarca odnosno brzina projektila
    
    }

    void OnTriggerEnter2D(Collider2D other) { //na sudaru s objektom odnosno neprijateljem aktivira se 
        Debug.Log("Projektil se sudario s " + other.gameObject);
        Destroy(gameObject); //unistava se sprite projektila pri sudaru s neprijateljem
    
    }

}
