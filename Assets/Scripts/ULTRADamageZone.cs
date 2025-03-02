using UnityEngine;

public class ULTRADamageZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {

        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.ChangeHealth(-69);
            Debug.Log(controller.currentHealth);

        }
    
    }
}
