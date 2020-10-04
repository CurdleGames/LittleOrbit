using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform nozzle;
    public Swap swapComponent;
    public Projectile[] bluePool;
    public Projectile[] redPool;

    public bool firing;
    public bool canFire;

    public float shotSpeed = 5f;
    public float shotCooldown = 0.4f;
    
    int blueIndex;
    int redIndex;

    float scatter = 0.02f;

    public Animator cannonAnim;

    public AudioSource audioSource;
    public AudioClip shootClip;

    public IEnumerator FireRoutine()
    {
        canFire = false;

        Fire();

        yield return new WaitForSeconds(shotCooldown);
        canFire = true;
    }

    public void Fire()
    {
        //Debug.Log("Fire");

        audioSource.PlayOneShot(shootClip);

        cannonAnim.SetTrigger("Shoot");

        float r = Random.Range(-scatter, scatter);
        float r2 = Random.Range(-scatter, scatter);

        switch (swapComponent.shotType)
        {
            case ShotType.Blue:

                blueIndex = blueIndex >= bluePool.Length - 1 ? 0 : blueIndex + 1;

                bluePool[blueIndex].Activate(nozzle.position, nozzle.rotation.eulerAngles, (nozzle.forward + (Vector3.up * r) + (Vector3.right * r2)) * shotSpeed);

                break;

            case ShotType.Red:

                redIndex = redIndex >= redPool.Length - 1 ? 0 : redIndex + 1;

                redPool[redIndex].Activate(nozzle.position, nozzle.rotation.eulerAngles, (nozzle.forward + (Vector3.up * r) + (Vector3.right * r2)) * shotSpeed);

                break;
        }
    }
}

