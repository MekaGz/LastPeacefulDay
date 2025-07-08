using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerOld;               // El personaje actual en escena
    public GameObject playerNewPrefab;         // El prefab del nuevo personaje
    public Transform spawnPoint;               // (Opcional) punto donde aparece el nuevo
    public GameObject playerNewObject;

    public void ReplacePlayer()
    {
        // Activa al nuevo personaje (si estaba desactivado)
        playerNewObject.SetActive(true);
        playerNewObject.tag = "Player";

        // Mueve la cámara para que lo siga
        Camera.main.GetComponent<SimpleCameraFollow>().target = playerNewObject.transform;

        // Opcional: destruir al viejo personaje si aún está activo
        GameObject oldPlayer = GameObject.FindWithTag("Player");
        if (oldPlayer != null && oldPlayer != playerNewObject)
        {
            Destroy(oldPlayer);
        }
    }
}