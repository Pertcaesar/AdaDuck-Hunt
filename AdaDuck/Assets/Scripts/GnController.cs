using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class GunController : MonoBehaviour
{
    SerialPort serial = new SerialPort("COM16", 9600); // Replace COM3 with your port
    public Transform gun; // Reference to gun transform
    public GameObject bulletPrefab;
    public Transform firePoint;

    void Start()
    {
        serial.Open();
    }

    void Update()
    {
        if (serial.IsOpen)
        {
            try
            {
                string data = serial.ReadLine();
                Debug.Log(data);

                if (data.StartsWith("X:"))
                {
                    // Parse accelerometer data
                    string[] parts = data.Split(',');
                    float x = float.Parse(parts[0].Substring(2));
                    float y = float.Parse(parts[1].Substring(2));

                    // Aim gun using accelerometer data
                    gun.rotation = Quaternion.Euler(y * -10, x * 10, 0);
                }
                else if (data == "SHOOT")
                {
                    Shoot();
                }
            }
            catch (System.Exception) { }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * 20f; // Adjust speed if needed
    }

    void OnApplicationQuit()
    {
        serial.Close();
    }
}
