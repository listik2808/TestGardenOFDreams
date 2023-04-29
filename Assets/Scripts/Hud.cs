using Scripts.Infrastructure.Services.SaveLoade;
using Scripts.Infrastructure.Services;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Scripts.Buttons;
using Scripts.ItemScriptableObject.Abstractitem;

namespace Scripts
{
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
        private Attack _attack;
        private AvailableButtonsPurchase _availableButtonsPurchase;
        private ISaveLoadService _saveLoadService;
        private List<Item> _items = new List<Item>();

        public Inventory Inventory => _inventory;
        public AvailableButtonsPurchase AvailableButtonsPurchase => _availableButtonsPurchase;

        private void Awake()
        {
            _inventory = GetComponentInChildren<Inventory>();
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        public void SetComponent(AmmoDepot ammoDepot, ButtonAddRandomItems randomItems, ButtonDeletItemCell buttonDeletItem
            , Attack attack, AvailableButtonsPurchase availableButtonsPurchase)
        {
            _ammoDepot = ammoDepot;
            _randomItems = randomItems;
            _deletItemCell = buttonDeletItem;
            _attack = attack;
            _availableButtonsPurchase = availableButtonsPurchase;
            _ammoDepot.ButtonAddPatrons.onClick.AddListener(SetAmmo);
            _randomItems.AddRandomItems.onClick.AddListener(GetRandomItems);
            _deletItemCell.DeletItem.onClick.AddListener(DeletItemCell);
            _attack.ButtonAttack.onClick.AddListener(Fire);
        }

        private void Fire()
        {
            _inventory.Attac();
            _saveLoadService.SaveProgress();
        }

        private void DeletItemCell()
        {
            _inventory.DeletItem();
            _saveLoadService.SaveProgress();
        }

        private void SetRandomItems()
        {
            _inventory.SetItems(_items);
            _items.Clear();
            _saveLoadService.SaveProgress();
        }

        private void SetAmmo()
        {
            _inventory.SetAmmo(_ammo);
            _saveLoadService.SaveProgress();
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
            _saveLoadService.SaveProgress();
        }

        private void Activate()
        {
            _randomItems.AddRandomItems.interactable = true;
            _saveLoadService.SaveProgress();
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
}