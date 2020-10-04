using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] Transform tf;
    [SerializeField] Vector3 startScale;
    public Axis axis;
    Vector3 scaleVec = Vector3.one;

    private void Start()
    {
        ResetScale();
    }

    public void ScaleByPercentage(float max, float amount)
    {
        switch (axis)
        {
            case Axis.X:
                scaleVec.x = amount / max;
                break;
            case Axis.Y:
                scaleVec.y = amount / max;
                break;
            case Axis.Z:
                scaleVec.z = amount / max;
                break;
        }

        tf.localScale = scaleVec;
    }

    public void ResetScale()
    {
        tf.localScale = startScale;
    }
}

public enum Axis
{
    X,
    Y,
    Z
}
