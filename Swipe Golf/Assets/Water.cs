using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public ParticleSystem particle;

    public void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Ball")
        {
            if (other.GetComponent<Rigidbody2D>().velocity.magnitude > 4)
            {

                particle.transform.position = other.transform.position;
                particle.Play(true);
            }
        }
    }
}
