using Scripts.Infrastructure.AssetManagement;
using System;
using UnityEngine;

namespace Scripts
{
    public class Inventory : MonoBehaviour
    {
        [Range(1f, 30f)]
        [SerializeField] private int _countSlots;
        [SerializeField] private Transform _content;
    }
}
