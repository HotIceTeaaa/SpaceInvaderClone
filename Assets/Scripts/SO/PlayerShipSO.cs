using UnityEngine;

namespace SpaceInvader
{
    [CreateAssetMenu(fileName = "PlayerShipSO", menuName = "SO/New PlayerShipSO")]
    public class PlayerShipSO : ScriptableObject {
        [SerializeField] public ShipType type;
        [SerializeField] public Sprite sprite;
        [SerializeField] public float maxHP;

        [Header("Bullet Related")]
        [SerializeField] public float shootCooldown;
        [SerializeField] public float speed;
        [SerializeField] public float lifetime;
        [SerializeField] public float damage;
    }
}
