using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tumble : MonoBehaviour
{
    public bool tumbling;
    public Vector3 rot;

    private void Update()
    {
        if(tumbling)
        {
            transform.Rotate(rot * Time.deltaTime);
        }
    }
}
