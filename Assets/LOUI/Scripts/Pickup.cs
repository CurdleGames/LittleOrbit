using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public LerpToTarget lerpToTarget;
    public MeshRenderer rend;
    public Collider col;
    [SerializeField] int playerLayer;

    public void Activate(Vector3 pos)
    {
        transform.position = pos;
        rend.enabled = true;
        col.enabled = true;
        lerpToTarget.lerping = true;
    }

    public void Kill()
    {
        rend.enabled = false;
        col.enabled = false;
        lerpToTarget.lerping = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == playerLayer)
        {
            Kill();
        }
    }
}
