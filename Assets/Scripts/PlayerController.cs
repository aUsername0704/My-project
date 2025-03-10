using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    //public InputAction LeftAction;
    //sustav za kretanje
    public InputAction MoveAction;
    Rigidbody2D rigidbody2d;
    public float movementSpeed = 6.5f;
    Vector2 move;
    //health sustav
    public int health { get { return currentHealth;  } }
    public int maxHealth = 5;
    public int currentHealth;
    //INVINCIBLE after damage
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float damageCooldown;

    Animator animator;
    Vector2 moveDirection = new Vector2(1,0);


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //QualitySettings.vSyncCount = 0; //gasi vsync?
        //Application.targetFrameRate = 10; //limitira FPS
        
        
        currentHealth = maxHealth;

        //LeftAction.Enable();
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*float horizontal = 0.0f;
        float vertical = 0.0f;

        if (LeftAction.IsPressed()) {

            horizontal = -1.0f;
        }
        else if (Keyboard.current.rightArrowKey.isPressed)
        {

            horizontal = 1.0f;
        }

        Debug.Log(horizontal);
        
        if (Keyboard.current.upArrowKey.isPressed)
        {

            vertical = 1.0f;
        }
        else if (Keyboard.current.downArrowKey.isPressed)
        {

            vertical = -1.0f;
        }
        Debug.Log(vertical);

        Vector2 position = transform.position;
        position.x = position.x + 0.1f * horizontal;
        position.y = position.y + 0.1f * vertical;
        transform.position = position;*/

        //Vector2 position = (Vector2)transform.position + move * 7.5f * Time.deltaTime;
        //transform.position = position;
        //Debug.Log(move);


        move = MoveAction.ReadValue<Vector2>();

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))//ako su trenutne vrijednosti ovih varijabli razlicite od 0, odnosno ako se lik krece
        {
            moveDirection.Set(move.x, move.y); //postavi vrijednosti diretiona na trenutne vrijednosti movea tjst. kretanja
            moveDirection.Normalize();//noramliztira vrijednosti na 1 kako bi se podjednako kretao u svim smjerovima
        }

        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);


        if (isInvincible) //ako je u prolsom frameu igrac primio udarac sada mu je postavljen status invincible 
        {
            damageCooldown -= Time.deltaTime; //smanjuje cooldown
            if (damageCooldown < 0) //kada timer zavrsi, kada je cooldown gotov
            {
                isInvincible = false; //gasi invincibility
            }
        }
    }

    void FixedUpdate() {//zaustavlja lika da ulazi u rigidbody drugog lika tako da nema cudnog treskanja pri kontaktu


        Vector2 position = (Vector2)rigidbody2d.position + move * movementSpeed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    
    }

    public void ChangeHealth(int amount) {

        if (amount < 0) //1.ako je kolicina promjene hpa manja od 0, odnosno ako se smanjuje pri udarcu lika u nesto negativno
        {
            if (isInvincible) //3. kada je vec invincible izadji jer se ceka da prodje damageCooldown period prije iduce provjere je li damage negativan
            {
                return;
            }
            isInvincible = true; //2. postavi da lik nemoze primat damage
            damageCooldown = timeInvincible;
            animator.SetTrigger("Hit");

        }


        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth); //mathf.clamp funkcija za odredjivanje raspona hpa u ovom slucaju -> prvi parametar - vrijednost koja se mora ograniciti, drugi parametar - minimum, treci je maximum
        //Debug.Log(currentHealth + "/" + maxHealth);//printa hp u log umjesto na ui jer nemam UI sad
        UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth); //omjer trenutnog hp i max hp za healthbar
    }


    void Launch() {

        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        //tri paramaetra za Instantiate naredbu - projectilePrefab stvara kopiju gameObjecta u poziciji definiranoj u drugom parametru s rotacijom definiranom u trecem parametru
        //Quaternion.identity je defaultna rotacija, tj. nema rotacije - matematicka operacija

        ProjectileScript projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(moveDireciton, 300);
        animator.SetTrigger("Launch");

    }


}
