using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Image fillBarImage; // referencia al Image tipo Filled
    public float detectionRange = 10f;
    public float attackRange = 1.8f;
    public float attackCooldown = 1.5f;
    public float damageAmount = 10f;
    public float health = 100f;
    public GameObject floatingDamagePrefab;
    public Transform damageSpawnPoint;
    public Animator animator;
    public float soundRange = 15f;
    public AudioSource audioSource;
    public AudioClip idleSound;
    public float idleSoundInterval = 15f;
    private Coroutine idleSoundCoroutine;

    private Transform player;
    private NavMeshAgent agent;
    private bool isDead = false;
    private bool isAttacking = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();

        if (idleSound != null && audioSource != null)
            idleSoundCoroutine = StartCoroutine(PlayIdleSound());
    }

    void Update()
    {
        if (isDead) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            agent.SetDestination(player.position);
            animator.SetBool("IsWalking", distance > (attackRange + 0.3f));
            Debug.Log("IsWalking set to: " + (distance > attackRange));

            if (distance <= attackRange)
            {
                agent.isStopped = true;

                Vector3 dir = player.position - transform.position;
                dir.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5f);

                if (!isAttacking)
                {
                    StartCoroutine(RealizarAtaque());
                }
            }
            else
            {
                agent.isStopped = false;
            }
        }
        else
        {
            agent.ResetPath();
            agent.isStopped = true;
            animator.SetBool("IsWalking", false);
        }
    }

    IEnumerator RealizarAtaque()
    {
        isAttacking = true;
        animator.SetTrigger("punch");

        yield return new WaitForSeconds(0.6f); // Tiempo para que llegue el golpe en la animación

        DoDamage();

        yield return new WaitForSeconds(attackCooldown); // Espera antes de volver a atacar

        isAttacking = false;
    }

    public void DoDamage()
    {
        if (isDead) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange + 0.5f)
        {
            // Componente PlayerController aún no implementado, esta línea está comentada para evitar errores:
            // player.GetComponent<PlayerController>()?.TakeDamage(damageAmount);
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        health -= amount;
        animator.SetTrigger("hit");

        if (fillBarImage != null)
            fillBarImage.fillAmount = health / 100f;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("die");
        agent.isStopped = true;
        agent.enabled = false;
        GetComponent<Collider>().enabled = false;

        if (idleSoundCoroutine != null)
            StopCoroutine(idleSoundCoroutine);

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        StartCoroutine(DesactivarLuego());
    }

    IEnumerator DesactivarLuego()
    {
        yield return new WaitForSeconds(5f);
        this.enabled = false;
    }

    IEnumerator PlayIdleSound()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(idleSoundInterval);

            if (!isDead && idleSound != null && audioSource != null && player != null)
            {
                float distToPlayer = Vector3.Distance(transform.position, player.position);
                if (distToPlayer <= soundRange)
                {
                    audioSource.PlayOneShot(idleSound);
                }
            }
        }
    }
}
