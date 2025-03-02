using UnityEngine;

public class SuperSnackCollectable : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {

        PlayerController controller = other.GetComponent < PlayerController>();

        if (controller != null && controller.health < controller.maxHealth)
        {
            controller.ChangeHealth(35);
            Destroy(gameObject);

        }
    
    }
}
