using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private DetectTouch detectTouch;
    [SerializeField] private Camera startCamera;
    [SerializeField] private Vector3 startPosition;
    private bool isLose;
    public Vector3 startAngle;
    public float step;
    private void Start()
    {
        detectTouch.SubscribeOnPointerUp(MoveUp);
        step = startPosition.y;
        startAngle = startCamera.transform.eulerAngles;
    }
    [Button]
    public void MoveUp()
    {
        if (isLose) return;
        step += SettingWrapper.Instance.Settings.Step;
        startCamera.transform.DOMoveY(step, SettingWrapper.Instance.Settings.CameraMovementSpeed);
    }
    [Button]
    public void ShowTower()
    {
        isLose = true;
        startCamera.transform.DOKill();
        startCamera.transform.DORotate(
            new Vector3(0,
            startCamera.transform.eulerAngles.y,
            startCamera.transform.eulerAngles.z), 1f).SetEase(Ease.InQuad);
            startCamera.transform.DOMoveX((step + startPosition.x) * 2, 1).SetEase(Ease.InQuad);
    }
    public void ToStartPosition()
    {
        isLose = false;
        startCamera.transform.DOKill();
        step = startPosition.y;
        startCamera.transform.DORotate(startAngle, 0.2f);
        startCamera.transform.DOMove(startPosition, 0.2f);
    }
}
