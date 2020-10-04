using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{
    public int hp;
    public int maxHp;
    public int recoveryTime;
    public bool canTakeDamage;
    public MatSwapper matSwap;
    public EnableMeshByIndex meshEnabler;
    public int damagingLayer;

    [SerializeField] float damageDuration = 0.2f;

    public Animator camAnim;

    public AudioSource audioSource;
    public AudioClip hurtClip;

    private void Start()
    {
        canTakeDamage = true;
    }

    public void AddHealth()
    {
        if(hp < maxHp)
        {
            hp++;
            meshEnabler.SetAsHp(hp, false);
        }
    }

    public void SetFullHealth()
    {
        hp = maxHp;
        meshEnabler.SetAll(true);
    }

    public IEnumerator TakeDamage()
    {
        Debug.Log("Planet TakeDamage");
        canTakeDamage = false;

        if (hp > 0)
        {
            camAnim.SetTrigger("Shake");
            audioSource.PlayOneShot(hurtClip);

            hp--;
            meshEnabler.SetAsHp(hp, true);
            StartCoroutine(matSwap.Flash(damageDuration));
        }

        yield return new WaitForSeconds(damageDuration + 0.05f);

        for (int i = 0; i < recoveryTime; i++)
        {
            matSwap.rend.enabled = false;
            yield return new WaitForSeconds(damageDuration);
            matSwap.rend.enabled = true;
            yield return new WaitForSeconds(damageDuration);
        }

        canTakeDamage = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == damagingLayer & canTakeDamage)
        {
            StartCoroutine(TakeDamage());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == damagingLayer & canTakeDamage)
        {
            Debug.Log("ihuqertghuiertgu8ierguih");

            StartCoroutine(TakeDamage());
        }
    }
}
