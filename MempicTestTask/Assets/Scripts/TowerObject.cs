using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using System;
using UnityEngine.Events;

public class TowerObject : ObjectForPool
{
    [SerializeField] private Renderer render;
    [SerializeField] private DetectTouch detectTouch;
    [SerializeField] private TowerObject defaultTowerObject;
    private TowerObject parentObject;
    private float threshholdForPerfectMove = 0.05f;
    private Vector3 ParentVector = new Vector3(1, 0.25f, 1);
    private bool isPerfect;
    private UnityAction onLose;
    private UnityAction onRestartAllowed;

    public TowerObject SetParent(TowerObject towerObject)
    {
        parentObject = towerObject;
        if (parentObject != null)
        {
            ParentVector = parentObject.gameObject.transform.localScale;
        }
        return this;
    }
    public void SubscribeOnLose(UnityAction onLose, UnityAction onRestartAllowd) 
    {
        this.onLose += onLose;
        this.onRestartAllowed += onRestartAllowd;
    }
    private void OnEnable()
    {
        Scale();
        detectTouch.SubscribeOnPointerUp(StopScale);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        transform.localScale = new Vector3(0, 0.25f, 0);
        ParentVector = new Vector3(1, 0.25f, 1);
        render.material.color = Color.white;
        parentObject = null;
        onLose = null;
        onRestartAllowed = null;
        isPerfect = false;
    }
    private void Scale()
    {
        transform.DOScale(new Vector3(ParentVector.x * 1.1f, 0.25f, ParentVector.z * 1.1f), 
            SettingWrapper.Instance.Settings.ScaleSpeed)
        .OnKill(() => { GetResult(); detectTouch.UnsubscribeOnPointerUp(StopScale); });
    }
    private void StopScale()
    {
        transform.DOKill();
    }
    private void GetResult()
    {
        if (transform.localScale.x > ParentVector.x)
        {
            StartCoroutine(Lose());
        }
        else
        {
            CheckPerfectMove();
        }
    }
    private void CheckPerfectMove()
    {
        if (transform.localScale.x + threshholdForPerfectMove >= ParentVector.x)
        {
            isPerfect = true;
            OnPerfectMove();
        }
    }
    private void OnPerfectMove()
    {
        transform.DOScale(GeScalceAmount(transform.localScale.x, 0.4f), 0.1f)
            .OnComplete(() =>
            {
                transform.DOScale
                (
                    new Vector3(transform.localScale.x - 0.2f,
                    0.25f,
                    transform.localScale.z - 0.2f),
                    0.1f
                ).OnComplete(() => { GetParentObject()?.DoWave(); });
            });
    }
    private void DoWave()
    {
        transform.DOScale(GeScalceAmount(transform.localScale.x, 0.3f), 0.1f)
            .OnComplete(() =>
            {
                transform.DOScale
                 (
                    GetUnscaleAmount(transform.localScale.x), 0.1f
                 ).OnComplete(() => { GetParentObject()?.DoWave(); });
            });
    }
    private Vector3 GeScalceAmount(float size, float scaleAmount)
    {
        float result = size + scaleAmount > 1 ? 1 : size + scaleAmount;
        return new Vector3(result, 0.25f, result);
    }
    private Vector3 GetUnscaleAmount(float size)
    {
        if (isPerfect)
        {
            return new Vector3(size - 0.3f, 0.25f, size - 0.3f);
        }
        float result = size * 0.8f > 0.1 ? size * 0.8f : 0.1f;
        return new Vector3(result, 0.25f, result);
    }
    IEnumerator Lose()
    {
        render.material.color = Color.red;
        onLose?.Invoke();
        yield return new WaitForSeconds(1f);
        onRestartAllowed?.Invoke();
        gameObject.SetActive(false);
    }
    private TowerObject GetParentObject()
    {
        if (parentObject != null)
        {
            return parentObject;
        }
        else if(defaultTowerObject != null)
        {
            return defaultTowerObject;
        }
        return null;
    }
}
