using UnityEngine;

public class Target : MonoBehaviour
{
    public delegate void TargetDestroyed();
    public event TargetDestroyed OnTargetDestroyed;

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is a bullet
        if (other.CompareTag("Bullet"))
        {
            OnTargetDestroyed?.Invoke(); // Notify that this target is destroyed
            Destroy(gameObject); // Destroy the target
            Destroy(other.gameObject); // Destroy the bullet
        }
    }
}

