using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    public Scaler scaler;
    public int collected;
    [SerializeField] int amtToHealth;

    public AudioSource audioSource;
    public AudioClip collectClip;
    public AudioClip fullClip;

    public void Add(int amt)
    {
        collected += amt;
        scaler.ScaleByPercentage(amtToHealth, collected);
    }

    public void ResetCollected()
    {
        collected = 0;
        scaler.ResetScale();
    }

    public bool CheckFull()
    {
        if (collected >= amtToHealth)
        {
            collected = 0;
            audioSource.PlayOneShot(fullClip);
            scaler.ResetScale();
            return true;
        }
        else
        {
            audioSource.PlayOneShot(collectClip);
            return false;
        }
        
    }
}
