using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LookAtPlayerNPC : MonoBehaviour
{
    public float lookRadius = 5f;
    public float rotationSpeed = 5f;

    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogueLines;

    public string[] secondDialogueLines;
    private bool secondDialogueStarted = false;

    public GameObject cartaPanel; // UI Image de la carta
    public float cartaWaitTime = 3f;

    private Transform player;
    private int currentLine = 0;
    private bool isTalking = false;
    private bool canCloseCarta = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        dialoguePanel.SetActive(false);
        dialogueLines = new string[]
    {
        "Buen día, querido. \nEl mensajero dejó una carta del rey.",
        "No pudo esperarte, pues debía partir raudo hacia los pueblos vecinos.",
        "¿Qué habrá acontecido?"
    };
        cartaPanel.SetActive(false);
        secondDialogueLines = new string[]
    {
        "Esto no presagia nada bueno...",
        "Quizás deberíamos alistarnos antes de emprender el viaje.",
        "¿Cómo dices...? ¿No deseas que te acompañe?",
        "No partirás sin tus armas. Espera, traeré tu equipo."
    };
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= lookRadius && !isTalking && currentLine == 0)
        {
            LookAtPlayer();
            StartDialogue();
            player.GetComponent<PlayerController>().enabled = false; // Desactiva movimiento
        }

        if (isTalking && Input.GetKeyDown(KeyCode.Space))
        {
            NextLine();
        }

        if (cartaPanel.activeSelf && canCloseCarta && Input.GetKeyDown(KeyCode.Space))
        {
            CloseCarta();
        }
    }

    void LookAtPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0f;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void StartDialogue()
    {
        isTalking = true;
        currentLine = 0;
        dialoguePanel.SetActive(true);
        dialogueText.text = dialogueLines[currentLine];

        // Detener movimiento
        player.GetComponent<PlayerController>().enabled = false;

        // Forzar animación Idle
        Animator playerAnimator = player.GetComponent<Animator>();
        playerAnimator.SetFloat("VelX", 0);
        playerAnimator.SetFloat("VelY", 0);
    }

    void NextLine()
    {
        currentLine++;

        if (!secondDialogueStarted)
        {
            if (currentLine < dialogueLines.Length)
            {
                dialogueText.text = dialogueLines[currentLine];
            }
            else
            {
                EndDialogue(); // termina y muestra carta
            }
        }
        else
        {
            if (currentLine < secondDialogueLines.Length)
            {
                dialogueText.text = secondDialogueLines[currentLine];
            }
            else
            {
                EndSecondDialogue(); // termina todo
            }
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        isTalking = false;

        ShowCarta();
    }

    void ShowCarta()
    {
        cartaPanel.SetActive(true);
        canCloseCarta = false;
        Invoke(nameof(EnableCartaClose), cartaWaitTime); // Esperar 3 segundos
    }

    void EnableCartaClose()
    {
        canCloseCarta = true;
    }

    void CloseCarta()
    {
        cartaPanel.SetActive(false);
        StartSecondDialogue();
    }
    void StartSecondDialogue()
    {
        isTalking = true;
        currentLine = 0;
        secondDialogueStarted = true;

        dialoguePanel.SetActive(true);
        dialogueText.text = secondDialogueLines[currentLine];
    }
    void EndSecondDialogue()
    {
        dialoguePanel.SetActive(false);
        isTalking = false;
        secondDialogueStarted = false;

        // Reactivar movimiento u otro evento
        player.GetComponent<PlayerController>().enabled = true;
    }
}