using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public static float rAxis;
    public static float lAxis;

    public bool swapDown;
    
    const float axisThresh = 0.2f;

    //
    public Movement movementComponent;
    public Shoot shootComponent;
    public Swap swapComponent;
    public Collect collectComponent;
    public Hp planetHp;

    [SerializeField] int pickupLayer = 11;

    private void Start()
    {
        StartCoroutine(CursorRoutine());
    }

    private void Update()
    {
        rAxis = Input.GetAxis("RT");
        lAxis = Input.GetAxis("LT");

        swapDown = Input.GetButtonDown("A");
        shootComponent.firing = Input.GetButton("X");
        
        if (Mathf.Abs(rAxis) > axisThresh || Mathf.Abs(lAxis) > axisThresh)
            movementComponent.Move(rAxis, lAxis);

        if (swapDown)
        {
            swapComponent.SwapType();    
        }

        if(shootComponent.firing && shootComponent.canFire)
        {
            shootComponent.StartCoroutine(shootComponent.FireRoutine());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == pickupLayer)
        {
            collectComponent.Add(1);

            if (collectComponent.CheckFull())
            {
                planetHp.AddHealth();
            }
        }
    }

    public IEnumerator CursorRoutine()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        yield return new WaitForSeconds(4f);

        StartCoroutine(CursorRoutine());
    }
}

public enum EntityType
{
    Player,
    Enemy
}