using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvader
{
    public class WaveManager : MonoBehaviour {
        [SerializeField] private float _timeBetweenEnemySpawns;
        [SerializeField] private float _spawnVariance;

        public static WaveManager Instance { get; private set; }

        void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        public IEnumerator BeginWave(WaveSO wave) {
            foreach (KeyValuePair<EnemyType, int> enemyDetail in wave.enemyDict) {
                for(int i = 0; i < enemyDetail.Value; i++) {
                    Transform pathTransform = wave.GetRandomPathTransform();
                    GameObject obj = PoolManager.Instance.GetAndSetPositionRotation(pathTransform.GetChild(0).position, Quaternion.identity);

                    Enemy enemyScript = obj.GetComponent<Enemy>();
                    enemyScript.Initialize(enemyDetail.Key, pathTransform, wave.enemySpeedMultiplier);

                    yield return new WaitForSeconds(GetSpawnCooldown(wave));
                }
            }
        }

        private float GetSpawnCooldown(WaveSO wave)
        {
            float time = Random.Range(_timeBetweenEnemySpawns - _spawnVariance, _timeBetweenEnemySpawns + _spawnVariance);
            time = Mathf.Clamp(time, 0f, float.MaxValue);

            return time * wave.enemySpawnMultiplier;
        }
    }
}
