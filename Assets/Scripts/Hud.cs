using Scripts;
using System.Collections.Generic;
using UnityEngine;

public class Hud : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    private AmmoDepot _ammoDepot;

    public Inventory Inventory => _inventory;
    public AmmoDepot AmmoDepot => _ammoDepot;

    public  void SetAmmoDepot (AmmoDepot ammoDepot)
    {
        _ammoDepot = ammoDepot;
        _ammoDepot.ButtonAddPatrons.onClick.AddListener(SetAmmo);
    }

    public void SetAmmo()
    {
        _inventory.SetItem(_ammoDepot.Items);
    }
}
