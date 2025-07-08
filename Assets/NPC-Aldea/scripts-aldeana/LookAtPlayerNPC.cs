using UnityEngine;

public class LookAtPlayerNPC : MonoBehaviour
{
    public Transform player;
    public float lookRadius = 5f;
    public float rotationSpeed = 5f;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= lookRadius)
        {
            // Dirección hacia el jugador (manteniendo altura)
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0f; // evita mirar hacia arriba o abajo

            // Rotación suave
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Dibuja el radio de detección en escena
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
