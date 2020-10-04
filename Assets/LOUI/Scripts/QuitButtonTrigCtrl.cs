using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButtonTrigCtrl : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;
    [SerializeField] MatSwapper matSwap;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 12 && LevelManager.levelState == LevelState.None)
        {
            levelManager.StartCoroutine(levelManager.QuitRoutine());
            StartCoroutine(matSwap.Flash(0.25f));
        }
    }
}
