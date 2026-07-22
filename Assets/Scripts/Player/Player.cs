using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvader
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        [Header("Health Bar Related")]
        [SerializeField] private Image image;

        private PlayerShipSO shipSO;

        public void Initialize(PlayerShipSO playerShipSO)
        {
            shipSO = playerShipSO;
            spriteRenderer.sprite = playerShipSO.sprite;
        }

        public void Shoot() {
            if (DataAndStates.Instance.canShoot) {
                SFXManager.Instance.PlaySFX(SFX.PlayerShoot);
                Vector3 bulletSpawnPos = Vector3.zero;
                
                switch(shipSO.type)
                {
                    case ShipType.Grape:
                        bulletSpawnPos = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
                        PoolManager.Instance.GetAndSetPositionRotation(BulletType.Player, bulletSpawnPos, Quaternion.identity);
                        break;

                    case ShipType.Peach:
                        bulletSpawnPos = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
                        PoolManager.Instance.GetAndSetPositionRotation(BulletType.Player, bulletSpawnPos, Quaternion.identity);
                        bulletSpawnPos = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
                        PoolManager.Instance.GetAndSetPositionRotation(BulletType.Player, bulletSpawnPos, Quaternion.Euler(0, 0, 15));
                        bulletSpawnPos = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
                        PoolManager.Instance.GetAndSetPositionRotation(BulletType.Player, bulletSpawnPos, Quaternion.Euler(0, 0, -15));
                        break;

                    case ShipType.Dates:
                        bulletSpawnPos = new Vector3(transform.position.x - 0.3f, transform.position.y + 0.3f, transform.position.z);
                        PoolManager.Instance.GetAndSetPositionRotation(BulletType.Player, bulletSpawnPos, Quaternion.identity);
                        bulletSpawnPos = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
                        PoolManager.Instance.GetAndSetPositionRotation(BulletType.Player, bulletSpawnPos, Quaternion.identity);
                        bulletSpawnPos = new Vector3(transform.position.x + 0.3f, transform.position.y + 0.3f, transform.position.z);
                        PoolManager.Instance.GetAndSetPositionRotation(BulletType.Player, bulletSpawnPos, Quaternion.identity);
                        break;

                }
                

                DataAndStates.Instance.canShoot = false;

            }
        }

        public void UpdateHPBar() {
            float fillAmount = DataAndStates.Instance.hp / DataAndStates.Instance.maxHP;
            image.fillAmount = fillAmount;
        }
    }
}
