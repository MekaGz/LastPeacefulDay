using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public Transform teleportDestination;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = teleportDestination.position;
        }
    }
}
