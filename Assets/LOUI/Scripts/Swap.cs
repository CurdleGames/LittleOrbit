using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swap : MonoBehaviour
{
    public ShotType shotType;
    public SkinnedMeshRenderer rend;

    [SerializeField] Material[] redMats;
    [SerializeField] Material[] blueMats;

    public void SwapType()
    {
        Debug.Log("SwapType()");

        rend.materials = shotType == ShotType.Blue ? redMats : blueMats;
        shotType = shotType == ShotType.Blue ? ShotType.Red : ShotType.Blue;
    }
}

public enum ShotType
{
    Blue,
    Red
}
