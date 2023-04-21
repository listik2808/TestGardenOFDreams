using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.SaveLoade;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private ISaveLoadService _saveLoadService;

    private void Awake()
    {
        _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Save);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Save);
    }

    private void Save()
    {
        _saveLoadService.SaveProgress();
    }
}
