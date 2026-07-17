using UnityEngine;
using System.Collections;

namespace SpaceInvader
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _playerBulletLifespan;

        [Header("Health Bar Related")]
        [SerializeField] private RectTransform _hpFrontBarRT;
        private float _hpFrontBarRTInitWidth;

        public void Initialize()
        {
            _hpFrontBarRTInitWidth = _hpFrontBarRT.sizeDelta.x;
        }

        public void Shoot() {
            if (DataAndStates.Instance.canShoot) {
                Vector3 bulletSpawnPos = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
                GameObject playerBullet = PoolManager.Instance.GetAndSetPositionRotation(BulletType.Player, bulletSpawnPos, Quaternion.identity);

                DataAndStates.Instance.canShoot = false;

                StartCoroutine(ReturnAfter(BulletType.Player, playerBullet, _playerBulletLifespan));
            }
        }

        private IEnumerator ReturnAfter(BulletType type, GameObject obj, float lifespan) {
            yield return new WaitForSeconds(lifespan);

            if (obj.activeSelf) {
                PoolManager.Instance.Return(type, obj);
            }
        }

        public void UpdateHPBar() {
            float newWidth = _hpFrontBarRTInitWidth * DataAndStates.Instance.hp / DataAndStates.Instance.maxHP;
            Vector2 temp = new Vector2(newWidth, _hpFrontBarRT.sizeDelta.y);
            _hpFrontBarRT.sizeDelta = temp;
        }
    }
}
