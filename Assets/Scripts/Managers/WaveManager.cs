using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvader
{
    public class WaveManager : MonoBehaviour {
        [SerializeField] private float _timeBetweenEnemySpawns;

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
                    GameObject obj = PoolManager.Instance.GetAndSetPositionRotation(enemyDetail.Key, pathTransform.GetChild(0).position, Quaternion.identity);

                    Enemy enemyScript = obj.GetComponent<Enemy>();
                    FollowWaypoint followWaypointScript = obj.GetComponent<FollowWaypoint>();

                    enemyScript.Initialize(enemyDetail.Key);
                    followWaypointScript.Initialize(enemyDetail.Key, pathTransform, wave.enemySpeedMultiplier);

                    yield return new WaitForSeconds(_timeBetweenEnemySpawns);
                }
            }
        }
    }
}
