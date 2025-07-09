using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public FadeController fadeController;
    public GameObject deathUI;
    private bool isDead = false;
    private Animator animator;
    private CharacterController controller;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        deathUI.SetActive(false);
    }

    void Update()
    {
        if (isDead && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    public void Die()
    {
        if (isDead) return;
        deathUI.SetActive(true);
        isDead = true;
        controller.enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        animator.SetTrigger("Die");
        // Muestra UI luego de 2 segundos

        fadeController.StartDeathSequence(deathUI);
        
    }

    public void Restart()
    {
        // Volver a punto de respawn
        Transform spawn = GameObject.Find("SpawnPoint").transform;
        transform.position = spawn.position;

        // Restaurar movimiento y estado
        GetComponent<PlayerMovement>().enabled = true;
        controller.enabled = true;
        isDead = false;
        animator.SetTrigger("Stand");

        // Desvanece la pantalla de nuevo
        fadeController.FadeOut();
        deathUI.SetActive(false);
    }
}
