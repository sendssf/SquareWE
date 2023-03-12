using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expolosion : MonoBehaviour
{
    // Start is called before the first frame update
    public float radius = 10.0F;
    public float power = 400.0F;
    public Transform explosionPos;

    void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(explosionPos.position, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power,explosionPos.position, radius, 2.0F);
        }
    }
}
