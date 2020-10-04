using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public EntityType entityType;
    public ShotType shotType;

    [SerializeField] Transform tf;
    [SerializeField] Rigidbody rb;
    [SerializeField] MeshRenderer rend;
    [SerializeField] BoxCollider col;

    readonly int enemyLayer = 10;
    readonly int playerLayer = 9;

    public AudioSource source;
    public AudioClip hitClip;

    public void Activate(Vector3 spawnPoint, Vector3 eulerRot, Vector3 initVelocity)
    {
        rb.isKinematic = false;
        tf.position = spawnPoint;
        tf.rotation = Quaternion.Euler(eulerRot);
        rb.velocity = initVelocity;
        rend.enabled = true;
        col.enabled = true;
    }

    public void Kill()
    {
        rb.isKinematic = true;
        rend.enabled = false;
        col.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (entityType)
        {
            case EntityType.Player:
                if(other.gameObject.layer == enemyLayer)
                {
                    source.PlayOneShot(hitClip);
                    Kill();
                }
                break;
            case EntityType.Enemy:
                if(other.gameObject.layer == playerLayer)
                {
                    Kill();
                }
                break;
        }

        if(other.gameObject.layer == 5)
        {
            Kill();
            source.PlayOneShot(hitClip);
        }
        else if(other.gameObject.layer == 15)
        {
            Kill();
        }
    }

}


