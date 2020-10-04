using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToTarget : MonoBehaviour
{
    public bool lerping;
    public float lerpFactor;
    [SerializeField] Transform tf;
    [SerializeField] Transform target;

    void Update()
    {
        if(lerping)
            tf.position = Vector3.Lerp(tf.position, target.position, lerpFactor * Time.deltaTime);
    }
}
