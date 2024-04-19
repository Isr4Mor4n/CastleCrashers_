using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f; // Velocidad de movimiento hacia el jugador
    public float attackDistance = 1.5f; // Radio de detecci�n para ataques
    public float attackDamage = 10f; // Da�o infligido por ataque
    public float maxHealth = 100f; // Salud m�xima del enemigo
    private float currentHealth; // Salud actual del enemigo
    public Slider healthSlider; // UI slider para mostrar la salud
    private Transform player; // Referencia al jugador
    private float attackCooldown = 1.5f; // Tiempo entre ataques
    private float lastAttackTime; // Cuando fue el �ltimo ataque

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
                Debug.Log("Player within attack range."); // Para depuraci�n
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
        Debug.Log("Attacking player."); // Confirma que el ataque se est� ejecutando
        player.GetComponent<PlayerController>().TakeDamage(attackDamage);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Damage taken: " + damage); // Esto mostrar� el da�o en la consola
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
            healthSlider.maxValue = maxHealth; // Establece el valor m�ximo aqu� si no lo hiciste en el inspector
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