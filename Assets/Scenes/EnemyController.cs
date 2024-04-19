using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f; // Velocidad de movimiento hacia el jugador
    public float attackDistance = 1.5f; // Radio de detección para ataques
    public float attackDamage = 10f; // Daño infligido por ataque
    public float maxHealth = 100f; // Salud máxima del enemigo
    private float currentHealth; // Salud actual del enemigo
    public Slider healthSlider; // UI slider para mostrar la salud
    private Transform player; // Referencia al jugador
    private float attackCooldown = 1.5f; // Tiempo entre ataques
    private float lastAttackTime; // Cuando fue el último ataque

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        MoveTowardsPlayer();
        AttackCheck();
    }

    void MoveTowardsPlayer()
    {
        if (Vector3.Distance(player.position, transform.position) > attackDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    void AttackCheck()
    {
        // Obtiene todos los colliders en un radio alrededor del enemigo
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackDistance);
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                Debug.Log("Player within attack range."); // Para depuración
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    AttackPlayer(hitCollider);
                    lastAttackTime = Time.time;
                }
            }
        }
    }

    void AttackPlayer(Collider2D player)
    {
        Debug.Log("Attacking player."); // Confirma que el ataque se está ejecutando
        player.GetComponent<PlayerController>().TakeDamage(attackDamage);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Damage taken: " + damage); // Esto mostrará el daño en la consola
        currentHealth -= damage;
        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject); // Destruye el objeto enemigo
    }

    public void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
            healthSlider.maxValue = maxHealth; // Establece el valor máximo aquí si no lo hiciste en el inspector
        }
        else
            Debug.LogError("Health Slider not assigned!");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}