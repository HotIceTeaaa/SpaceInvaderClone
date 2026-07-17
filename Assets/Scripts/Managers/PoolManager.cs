using UnityEngine;
using System.Collections.Generic;

namespace SpaceInvader
{
    public class PoolManager : MonoBehaviour {
        [Header("Player Bullet Pool")]
        [SerializeField] private GameObject _playerBulletPrefab;
        [SerializeField] private int _playerBulletCount = 100;
        private Queue<GameObject> _playerBulletShelf = new Queue<GameObject>();

        [Header("Enemy Pool")]
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private int _enemyCount = 30;
        private Queue<GameObject> _enemyShelf = new Queue<GameObject>();

        [Header("Enemy Bullet Pool")]
        [SerializeField] private GameObject _enemyBulletPrefab;
        [SerializeField] private int _enemyBulletCount = 100;
        private Queue<GameObject> _enemyBulletShelf = new Queue<GameObject>();

        public static PoolManager Instance { get; private set; }

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            //DontDestroyOnLoad(gameObject);

            // prewarm shelf
            for (int i = 0; i < _playerBulletCount; i++) {
                _playerBulletShelf.Enqueue(CreateNew(_playerBulletPrefab));
            }

            for (int i = 0; i < _enemyBulletCount; i++) {
                _enemyBulletShelf.Enqueue(CreateNew(_enemyBulletPrefab));
            }

            for (int i = 0; i < _enemyCount; i++) {
                _enemyShelf.Enqueue(CreateNew(_enemyPrefab));
            }
        }

        public GameObject GetAndSetPositionRotation(BulletType type, Vector3 position, Quaternion rotation) {
            GameObject obj = null;

            switch (type) {
                case BulletType.Player:
                    obj = _playerBulletShelf.Dequeue();
                    break;
                case BulletType.Enemy:
                    obj = _enemyBulletShelf.Dequeue();
                    break;
            }

            obj.transform.SetPositionAndRotation(position, rotation);
            obj.SetActive(true);

            return obj;
        }

        public GameObject GetAndSetPositionRotation(Vector3 position, Quaternion rotation) {
            GameObject obj = _enemyShelf.Dequeue();

            obj.transform.SetPositionAndRotation(position, rotation);
            obj.SetActive(true);

            return obj;
        }

        public void Return(BulletType type, GameObject obj) {
            obj.SetActive(false);

            switch (type) {
                case BulletType.Player:
                    _playerBulletShelf.Enqueue(obj);
                    break;
                case BulletType.Enemy:
                    _enemyBulletShelf.Enqueue(obj);
                    break;
            }
        }

        public void Return(GameObject obj) {
            obj.SetActive(false);
            _enemyShelf.Enqueue(obj);
        }

        private GameObject CreateNew(GameObject prefab) {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            return obj;
        }
    }
}
