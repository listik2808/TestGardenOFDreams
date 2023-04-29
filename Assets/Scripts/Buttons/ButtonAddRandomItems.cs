using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Buttons
{
    public class ButtonAddRandomItems : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public Button AddRandomItems => _button;
    }
}