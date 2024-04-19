using UnityEngine;

public class BreakableCube : MonoBehaviour
{
    public float health = 50f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DestroySquare();
        }
    }

    void DestroySquare()
    {
        // Encuentra al jugador y aumenta su salud
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerController>().IncreaseHealth(20);
        }
        Destroy(gameObject); // Destruye este objeto
    }
}
