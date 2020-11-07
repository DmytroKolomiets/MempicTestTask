using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingWrapper : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    public GameSettings Settings => gameSettings;

    public static SettingWrapper Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
