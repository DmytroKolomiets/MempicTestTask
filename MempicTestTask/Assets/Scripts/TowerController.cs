using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TowerController : PoolingSystem
{
    [SerializeField] private DetectTouch detectTouch;
    [SerializeField] private float step;
    private Vector3 spawnPosition = new Vector3(0, 0, 0);
    private GameObject tempObject;
    private TowerObject previosObject;
    private UnityAction onLose;
    private UnityAction onRestartAllowed;
    [SerializeField] private TowerObject prototype;
    [SerializeField] private BottomTowerObject bottomTowerObject;

    private void Start()
    {
        detectTouch.SubscribeOnPointerDown(SpawnTowerObject);
    }
    public void Restart()
    {
        spawnPosition = Vector3.zero;
        previosObject = null;
        bottomTowerObject.ToStartSize();
        DeactivateAll();
    }
    public void SubscribeOnLose(UnityAction onLose, UnityAction onRestartAllowed)
    {
        this.onLose += onLose;
        this.onRestartAllowed += onRestartAllowed;
    }
    protected override void SetPrototypeForPool()
    {
        prototypeForPool = prototype;
    }
    private void SpawnTowerObject()
    {
        tempObject = GetFromPool();
        spawnPosition.y += step;
        tempObject.transform.position = spawnPosition;
        previosObject = tempObject.GetComponent<TowerObject>().SetParent(previosObject);
        previosObject.SubscribeOnLose(onLose, onRestartAllowed);
        tempObject.SetActive(true);
    }
}
