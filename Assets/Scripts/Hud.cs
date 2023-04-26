using Scripts;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Hud : MonoBehaviour
{
    [SerializeField] private List<Ammo> _ammo = new List<Ammo>();
    [SerializeField] private List<Weapon> _weapons = new List<Weapon>();
    [SerializeField] private List<Torso> _torso = new List<Torso>();
    [SerializeField] private List<Head> _head = new List<Head>();

    private Inventory _inventory;
    private AmmoDepot _ammoDepot;
    private ButtonAddRandomItems _randomItems;
    private ButtonDeletItemCell _deletItemCell;
    private List<Item> _items = new List<Item>();

    public Inventory Inventory => _inventory;

    private void Awake()
    {
        _inventory = GetComponentInChildren<Inventory>();
    }

    public void SetComponent (AmmoDepot ammoDepot, ButtonAddRandomItems randomItems,ButtonDeletItemCell buttonDeletItem)
    {
        _ammoDepot = ammoDepot;
        _randomItems = randomItems;
        _deletItemCell = buttonDeletItem;
        _ammoDepot.ButtonAddPatrons.onClick.AddListener(SetAmmo);
        _randomItems.AddRandomItems.onClick.AddListener(GetRandomItems);
        _deletItemCell.DeletItem.onClick.AddListener(DeletItemCell);
    }

    private void DeletItemCell()
    {
        _inventory.DeletItem();
    }

    private void SetRandomItems()
    {
        _inventory.SetItems(_items);
        _items.Clear();
    }

    private void SetAmmo()
    {
        _inventory.SetAmmo(_ammo);
    }

    private void GetRandomItems()
    {
        _inventory.IsActivChanged += Activate;
        _randomItems.AddRandomItems.interactable = false;
        int random = 0;
        RandomWeapon(random);
        RandomTorso(random);
        RandomHead(random);
        SetRandomItems();
    }

    private void Activate()
    {
        _randomItems.AddRandomItems.interactable = true;
    }

    private void RandomHead(int random)
    {
        random = Random.Range(0, _head.Count);
        _items.Add(_head[random]);
    }

    private void RandomTorso(int random)
    {
        random = Random.Range(0, _torso.Count);
        _items.Add(_torso[random]);
    }

    private void RandomWeapon(int random)
    {
        random = Random.Range(0, _weapons.Count);
        _items.Add(_weapons[random]);
    }
}
