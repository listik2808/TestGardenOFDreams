using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Buttons
{
    public class ButtonDeletItemCell : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public Button DeletItem => _button;
    }
}