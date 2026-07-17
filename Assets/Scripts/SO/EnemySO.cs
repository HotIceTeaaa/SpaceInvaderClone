using UnityEngine;

namespace SpaceInvader
{
    [CreateAssetMenu(fileName = "EnemySO", menuName = "SO/New EnemySO")]
    public class EnemySO : ScriptableObject {

        [SerializeField] public Sprite sprite;

        [Header("Shooting Related")]
        [SerializeField] public float enemyBulletLifespan;
        [SerializeField] public float shootCooldown;
        [SerializeField] public float shootThreshold;

        [Header("Other")]
        [SerializeField] public float scoreWorth;
        [SerializeField] public float hp;
        [SerializeField] public float speed;
    }
}
