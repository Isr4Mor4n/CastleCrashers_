using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance

    private int enemyCount; // N�mero de enemigos activos
    public Text enemyCountText; // Texto UI para mostrar el n�mero de enemigos restantes

    void Awake()
    {
        // Asegurar que solo haya una instancia de GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateEnemyCount(); // Inicializa el contador con el n�mero de enemigos actuales
    }

    void Update()
    {
        UpdateEnemyCount(); // Actualiza el contador de enemigos en cada frame
    }

    void UpdateEnemyCount()
    {
        int currentEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount != currentEnemyCount)
        {
            enemyCount = currentEnemyCount;
            UpdateEnemyCountUI();
            CheckGameOver();
        }
    }

    void UpdateEnemyCountUI()
    {
        if (enemyCountText != null)
        {
            enemyCountText.text = "ENEMIES REMAINING: " + enemyCount;
        }
    }

    void CheckGameOver()
    {
        if (enemyCount <= 0)
        {
            // L�gica para cuando todos los enemigos son derrotados
            Debug.Log("All enemies defeated!");
            // Puedes agregar m�s l�gica aqu�, como mostrar un mensaje de victoria, cambiar de escena, etc.
        }
    }
}
