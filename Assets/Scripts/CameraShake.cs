using System.Collections;
using UnityEngine;

namespace SpaceInvader
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private float _shakeDuration;
        [SerializeField] private float _shakeMagnitude;

        private Vector3 initPosition;

        public void Initialize()
        {
            initPosition = transform.position;
        }
        
        public void Play()
        {
            StartCoroutine(ShakeCamera());
        }

        IEnumerator ShakeCamera()
        {
            float currentDuration = 0f;

            while (currentDuration < _shakeDuration)
            {
                Vector3 direction = Random.insideUnitCircle * _shakeMagnitude;
                transform.position += direction;

                yield return new WaitForEndOfFrame();
                currentDuration += Time.deltaTime;
            }

            transform.position = initPosition;
        }
    }
}
