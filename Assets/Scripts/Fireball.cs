using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Spike")
        {
            Destroy(other.gameObject);
        }
    }
}
