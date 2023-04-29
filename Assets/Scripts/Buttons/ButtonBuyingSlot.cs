using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Buttons
{
    public class ButtonBuyingSlot : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _priceInt;
        [Range(1, 100f)]
        [SerializeField] private int _price;
        private bool _isBuy = false;

        public Button Buy => _button;
        public int Price => _price;
        public bool IsBuy => _isBuy;

        public event Action<ButtonBuyingSlot> OnBuy;
        public event Action OnBought;

        private void Start()
        {
            _priceInt.text = _price.ToString();
        }

        public void Diactivate()
        {
            _isBuy = true;
            _button.gameObject.SetActive(false);
        }

        public void BuyCell()
        {
            OnBought?.Invoke();
        }

        public void SetPrice(int id)
        {
            _price *= id;
        }
    }
}