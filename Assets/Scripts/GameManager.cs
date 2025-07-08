using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject playerPrefab;
    private GameObject playerInstance;

    void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        if (playerInstance == null)
        {
            Vector3 spawnPos = Vector3.zero;

            GameObject spawn = GameObject.Find("SpawnPoint");
            if (spawn != null)
                spawnPos = spawn.transform.position;

            playerInstance = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
        }
        else
        {
            // Si ya existe, moverlo al punto de spawn
            GameObject spawn = GameObject.Find("SpawnPoint");
            if (spawn != null)
                playerInstance.transform.position = spawn.transform.position;
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
