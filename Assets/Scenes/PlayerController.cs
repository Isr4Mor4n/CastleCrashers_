using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // Velocidad de movimiento del jugador
    public float maxHealth = 100f; // Vida máxima del jugador
    private float currentHealth; // Vida actual del jugador
    public Slider healthSlider; // Referencia al Slider de la vida
    public float attackDistance = 1.5f; // Distancia de ataque del jugador
    public float attackDamage = 10f; // Daño del ataque del jugador

    void Start()
    {
        currentHealth = maxHealth; // Inicializar la vida actual con la vida máxima
        UpdateHealthUI(); // Actualizar el Slider de la vida al inicio
    }

    void Update()
    {
        // Obtener el movimiento horizontal (izquierda/derecha)
        float horizontalInput = Input.GetAxis("Horizontal");
        // Obtener el movimiento vertical (arriba/abajo)
        float verticalInput = Input.GetAxis("Vertical");

        // Mover el jugador
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * speed * Time.deltaTime);

        // Atacar si se presiona la tecla de espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Encontrar todos los enemigos cercanos al jugador
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();

        // Iterar sobre los enemigos encontrados
        foreach (EnemyController enemy in enemies)
        {
            // Calcular la distancia al enemigo
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            // Si el enemigo está dentro de la distancia de ataque, infligir daño
            if (distanceToEnemy <= attackDistance)
            {
                enemy.TakeDamage(attackDamage);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Health before damage: " + currentHealth);
        currentHealth -= damage;
        Debug.Log("Health after damage: " + currentHealth);
        UpdateHealthUI();

        // Solo llama a Die() si la salud es 0 o menos
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth; // Asegúrate de que esto se actualice correctamente
            healthSlider.maxValue = maxHealth; // Establece el valor máximo aquí si no lo hiciste en el inspector
        }
        else
            Debug.LogError("Health Slider not assigned!");
    }

    public void IncreaseHealth(int amount)
    {
        currentHealth += amount;

        // Asegura que la salud no exceda la salud máxima
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        // Actualiza la interfaz de usuario de la salud
        UpdateHealthUI();
    }
}