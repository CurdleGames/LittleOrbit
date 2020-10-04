using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    readonly Vector3 rot = new Vector3(0f, 0f, 175f);

    public void Move(float r, float l) 
    {
        transform.Rotate(rot * (Mathf.Abs(r) > Mathf.Abs(l) ? 1 : -1) * Time.deltaTime);
    }
}
