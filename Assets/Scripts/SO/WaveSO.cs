using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SpaceInvader
{
    [CreateAssetMenu(fileName = "WaveSO", menuName = "SO/New WaveSO")]
    public class WaveSO : ScriptableObject {

        [SerializeField] public int waveNumber;

        [SerializeField] public GameObject[] paths;
        [SerializeField] public float enemySpeedMultiplier;

        [Serializable]
        public struct EnemyDetails {
            public EnemyType type;
            public int count;
        }

        [SerializeField] private List<EnemyDetails> enemyList;
        public Dictionary<EnemyType, int> enemyDict;

        public void Initialize() {
            enemyDict = new Dictionary<EnemyType, int>();

            foreach (EnemyDetails enemyDetails in enemyList) {
                enemyDict.Add(enemyDetails.type, enemyDetails.count);
            }
        }

        public Transform GetRandomPathTransform() {
            int val = UnityEngine.Random.Range(0, paths.Length);
            return paths[val].transform;
        }
    }
}
