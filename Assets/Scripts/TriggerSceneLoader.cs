using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TriggerSceneLoader : MonoBehaviour
{
    public string targetSceneName = "campoBatalla";
    public GameObject blackScreen;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LoadSceneWithBlackScreen(other.gameObject));
        }
    }

    IEnumerator LoadSceneWithBlackScreen(GameObject player)
    {
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(1f); // puedes poner un fade

        Destroy(player); // Destruye el personaje viejo
        SceneManager.LoadScene(targetSceneName);
    }
}