using Scripts.Buttons;
using System;
using UnityEngine;

namespace Scripts
{
    public class Wallet : MonoBehaviour
    {
        [SerializeField] private int _money = 100;

        public int Money => _money;

        public event Action OnCoinsChanged;
        public void Interacteibel(ButtonBuyingSlot buttonBuyingSlot)
        {
            if (buttonBuyingSlot.Price <= _money)
            {
                _money -= buttonBuyingSlot.Price;
                buttonBuyingSlot.BuyCell();
                buttonBuyingSlot.Diactivate();
                OnCoinsChanged?.Invoke();
            }
        }
    }
}