using Scripts.Buttons;
using Scripts.Data;
using Scripts.Infrastructure.Services.PersistenProgress;
using Scripts.Infrastructure.Services.SaveLoade;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts
{
    public class AvailableButtonsPurchase : MonoBehaviour, ISavedProgress
    {
        private Wallet _wallet;

        private List<ButtonBuyingSlot> _buttonList = new List<ButtonBuyingSlot>();
        private ButtonBuyingSlot _buyingSlot;
        private int _multiplicationFactor = 0;
        public void SetBuyButton(List<ButtonBuyingSlot> buttonBuyingSlot)
        {
            _buttonList = buttonBuyingSlot;
            for (int i = 0; i < _buttonList.Count; i++)
            {
                if (_buttonList[i].IsBuy == false)
                {
                    _multiplicationFactor++;
                    _buttonList[i].SetPrice(_multiplicationFactor);
                }
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.buttonBuySlote.SetMultiplicationFactor(_multiplicationFactor);
            string json = JsonUtility.ToJson(progress.CellInventory);
            SaveLoad.Save(json);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            string json = SaveLoad.Load();
            if (json != null)
            {
                progress = JsonUtility.FromJson<PlayerProgress>(json);
                _multiplicationFactor = progress.buttonBuySlote._multiplicationFactor;
            }
        }

        public void SetWallet(Wallet wallet)
        {
            _wallet = wallet;
            Subscribe();
            _wallet.OnCoinsChanged += Subscribe;
        }

        private void Subscribe()
        {
            foreach (var item in _buttonList)
            {
                item.Buy.interactable = false;
            }
            _buyingSlot = _buttonList.First(x => x.IsBuy == false);
            if (_buyingSlot.Price <= _wallet.Money)
            {
                _buyingSlot.Buy.interactable = true;
            }
            _buyingSlot.Buy.onClick.AddListener(ClicButtonBuy);
        }

        private void ClicButtonBuy()
        {
            _wallet.Interacteibel(_buyingSlot);
        }
    }
}