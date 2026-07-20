using System.Collections;
using UnityEngine;

namespace SpaceInvader
{
    public class ParticleHandler : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private float _lifespan;

        public void OnEnable()
        {
            StartCoroutine(ReturnAfter(gameObject));
            _particleSystem.Play();
        }

        private IEnumerator ReturnAfter(GameObject obj)
        {
            yield return new WaitForSeconds(_lifespan);

            if (obj.activeSelf)
            {
                PoolManager.Instance.ReturnParticleSystem(obj);
            }
        }

    }
}



