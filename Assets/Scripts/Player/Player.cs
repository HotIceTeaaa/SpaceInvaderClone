using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvader
{
    public class Player : MonoBehaviour
    {

        [Header("Health Bar Related")]
        [SerializeField] private Image image;


        public void Shoot() {
            if (DataAndStates.Instance.canShoot) {
                Vector3 bulletSpawnPos = Vector3.zero;
                
                switch(PlayerPreferences.Instance.GetInt("shipType", 0))
                {
                    case 0:
                        bulletSpawnPos = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
                        PoolManager.Instance.GetAndSetPositionRotation(BulletType.Player, bulletSpawnPos, Quaternion.identity);
                        break;

                    case 1:
                        bulletSpawnPos = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
                        PoolManager.Instance.GetAndSetPositionRotation(BulletType.Player, bulletSpawnPos, Quaternion.identity);
                        bulletSpawnPos = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
                        PoolManager.Instance.GetAndSetPositionRotation(BulletType.Player, bulletSpawnPos, Quaternion.Euler(0, 0, 30));
                        bulletSpawnPos = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
                        PoolManager.Instance.GetAndSetPositionRotation(BulletType.Player, bulletSpawnPos, Quaternion.Euler(0, 0, -30));
                        break;

                    case 2:
                        bulletSpawnPos = new Vector3(transform.position.x - 0.1f, transform.position.y + 0.3f, transform.position.z);
                        PoolManager.Instance.GetAndSetPositionRotation(BulletType.Player, bulletSpawnPos, Quaternion.identity);
                        bulletSpawnPos = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
                        PoolManager.Instance.GetAndSetPositionRotation(BulletType.Player, bulletSpawnPos, Quaternion.identity);
                        bulletSpawnPos = new Vector3(transform.position.x + 0.1f, transform.position.y + 0.3f, transform.position.z);
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
