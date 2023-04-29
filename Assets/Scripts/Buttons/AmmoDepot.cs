using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Buttons
{
    public class AmmoDepot : MonoBehaviour
    {
        [SerializeField] private Button _buttonAddPatrons;

        public Button ButtonAddPatrons => _buttonAddPatrons;
    }
}