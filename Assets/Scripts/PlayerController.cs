using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //QualitySettings.vSyncCount = 0; //gasi vsync?
        //Application.targetFrameRate = 10; //limitira FPS
        
        
        currentHealth = maxHealth;

        //LeftAction.Enable();
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        

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

        }


        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth); //mathf.clamp funkcija za odredjivanje raspona hpa u ovom slucaju -> prvi parametar - vrijednost koja se mora ograniciti, drugi parametar - minimum, treci je maximum
        Debug.Log(currentHealth + "/" + maxHealth);//printa hp u log umjesto na ui jer nemam UI sad
        

    
    }



}
