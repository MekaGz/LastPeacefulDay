using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager2 : MonoBehaviour
{
    public GameObject loadingScreen;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Persiste entre escenas
    }

    public void LoadSceneWithTransition(string sceneName)
    {
        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    IEnumerator LoadSceneRoutine(string sceneName)
    {
        if (loadingScreen != null)
            loadingScreen.SetActive(true);

        yield return new WaitForSeconds(1f); // Puedes animar fade

        SceneManager.LoadScene(sceneName);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Busca personaje y cámara de la nueva escena
        GameObject newPlayer = GameObject.Find("Paladin WProp J Nordstrom_2"); // ponle ese nombre al nuevo
        GameObject newCamera = GameObject.Find("Main Camera_2");

        if (newPlayer != null)
            newPlayer.SetActive(true);

        if (newCamera != null)
            newCamera.SetActive(true);

        if (loadingScreen != null)
            loadingScreen.SetActive(false);

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}