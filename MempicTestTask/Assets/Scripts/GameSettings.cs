using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField, Range(0.3f, 3)] private float scaleSpeed;
    [SerializeField, Range(0.1f, 1)] private float cameraMovementSpeed;
    private float cylinderHeight = 0.25f;

    public float ScaleSpeed => scaleSpeed;
    public float CameraMovementSpeed => cameraMovementSpeed;
    public float Step => cylinderHeight * 2;
}
