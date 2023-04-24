using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDepot : MonoBehaviour
{
    [SerializeField] private Button _buttonAddPatrons;
    [SerializeField] private List<Item> _items;

    public Button ButtonAddPatrons => _buttonAddPatrons;
    public List<Item> Items => _items;
}