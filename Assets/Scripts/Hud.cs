using Scripts;
using UnityEngine;

public class Hud : MonoBehaviour
{
    [SerializeField] private SpawnerInventory _spawnerInventory;

    public SpawnerInventory SpawnerInventory => _spawnerInventory;
}
