using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    Animator animator;
    public float EnemySpeed = 5.0f;
    Rigidbody2D rigidbody2d;
    public bool vertical;

    public float changeTime = 3.0f;
    float timer; //vrijeme koje odbrojava do iduce promjene smjera
    int direction = 1; //varijabla koja odredjuje smjer kretanja, pozitivno je naprijed po osi, negativno je unazad

    public float promjenaOsi = 5.0f;
    float timerOsi;

    bool broken = true;

    //BoxCollider2D boxCollider;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime; //pocetak timera ce biti change Time varijabla jer je pocetak i nije se jos nista promjenilo
        timerOsi = promjenaOsi;
        //boxCollider = GetComponent<BoxCollider2D>();
          
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timerOsi -= Time.deltaTime;

        
        if(timer <= 0)
        {
            direction = -direction; //ako je timer manji ili jednak 0, promjeni smjer, dva - daje pozitivno, jedan ce napravit negativno
            timer = changeTime; //resetira timer

        }

        if (timerOsi <= 0) {

            vertical = !vertical;
            timerOsi = promjenaOsi;
        }

        
    }

    void FixedUpdate()
    {
        if (!broken) {


            return;
        }


        Vector2 position = rigidbody2d.position; //postion sprema poziciju rigidbodya
      
            if (vertical) //ako je vertical true
            {
                position.y = position.y + EnemySpeed * direction * Time.deltaTime; //pomak po y osi
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);

            }
            else
            {
                position.x = position.x + EnemySpeed * direction * Time.deltaTime; //pomak po x osi
                animator.SetFloat("Move X", direction);
                animator.SetFloat("Move Y", 0);

        }

            rigidbody2d.MovePosition(position); //pomak rigidbodya na novu poziciju
        }   



    void OnTriggerEnter2D(Collider2D other)
    {

        PlayerController player = other.gameObject.GetComponent<PlayerController>(); //uzmi skriptu plyercontroler iz objekta s kojim je doslo do kontakta
        if (player != null) {

            player.ChangeHealth(-10);
        
        }
    }

    public void Fix() {

        broken = false; //enemy je popravljen
        rigidbody2d.simulated = false; //gasi fiziku rigidbodya neprijatelja kako vise nebi mogao bit pogodjen ili sudarat se s igracem
        //boxCollider.enabled = true;
        animator.SetTrigger("Fixed");
  
    
    }


}
