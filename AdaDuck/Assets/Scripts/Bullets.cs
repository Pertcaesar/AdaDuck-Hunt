using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f; // Bullet speed
    public float lifetime = 5f; // Time before the bullet destroys itself

    void Start()
    {
        // Destroy the bullet after a set time to prevent clutter
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}

