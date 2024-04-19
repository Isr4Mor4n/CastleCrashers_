using UnityEngine;

public class BreakableSquare : MonoBehaviour
{
    public int health = 50;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DestroySquare();
        }
    }

    void DestroySquare()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerController>().IncreaseHealth(20);
        }
        Destroy(gameObject); // Destruye este objeto
    }
}
