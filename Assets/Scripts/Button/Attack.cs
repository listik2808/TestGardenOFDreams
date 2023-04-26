using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    [SerializeField] private Button _button;

    public Button ButtonAttack => _button;
}
