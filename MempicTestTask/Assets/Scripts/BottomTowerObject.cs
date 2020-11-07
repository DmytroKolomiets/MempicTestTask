using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomTowerObject : TowerObject
{
    private Vector3 startScale;
    private void OnEnable()
    {
        startScale = transform.localScale;
    }
    protected override void OnDisable()
    {

    }
    public void ToStartSize()
    {
        transform.localScale = startScale;
    }
}
