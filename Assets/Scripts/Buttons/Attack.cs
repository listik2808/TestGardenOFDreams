using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Buttons
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public Button ButtonAttack => _button;
    }
}