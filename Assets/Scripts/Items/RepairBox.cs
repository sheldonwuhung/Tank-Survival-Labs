using UnityEngine;

public class RepairBox : MonoBehaviour
{
    [SerializeField] public int healthRestore = 25;
    private bool used = false;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!used && other.gameObject.CompareTag("Player"))
        {
            used = true; 
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.health += healthRestore;
            if (playerController.health > playerController.maxHealth) playerController.health = playerController.maxHealth;
            Destroy(gameObject);
        }
    }
}
