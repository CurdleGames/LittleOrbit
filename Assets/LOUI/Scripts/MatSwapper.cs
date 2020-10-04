using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatSwapper : MonoBehaviour
{ 
    public MeshRenderer rend;

    [SerializeField] bool useArray;
    [SerializeField] Material flashMat;
    [SerializeField] Material[] initMats;

    public IEnumerator Flash(float duration)
    {
        rend.material = flashMat;

        yield return new WaitForSeconds(duration);

        if (useArray)
            rend.materials = initMats;
        else
            rend.material = initMats[0];
    }
}
