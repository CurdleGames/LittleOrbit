using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMeshByIndex : MonoBehaviour
{
    [SerializeField] MeshRenderer[] rends;

    public void EnableByIndex(int index, bool enable)
    {
        rends[index].enabled = enable;
    }

    public void SetAsHp(int hp, bool isDamage)
    {
        if (isDamage)
            rends[hp].enabled = false;
        else if(hp > 0)
            rends[hp - 1].enabled = true;
    }

    public void SetAll(bool enable)
    {
        foreach (MeshRenderer rend in rends)
            rend.enabled = enable;
    }
}
