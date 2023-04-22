using Scripts;
using UnityEngine;

public class Hud : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    public Inventory inventory => _inventory;
}
