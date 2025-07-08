using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    public GameObject playerScene2;
    public GameObject cameraScene2;
    public GameObject blackScreen;

    void Start()
    {
        if (playerScene2 != null)
            playerScene2.SetActive(true);

        if (cameraScene2 != null)
            cameraScene2.SetActive(true);

        if (blackScreen != null)
            blackScreen.SetActive(false);
    }
}
