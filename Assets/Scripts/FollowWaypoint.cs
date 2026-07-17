using UnityEngine;

namespace SpaceInvader {
    public class FollowWaypoint : MonoBehaviour {
        [SerializeField] private float _speed;

        private Transform[] _waypoints;
        private int _waypointIndex = 0;
        private float _speedMultiplier;

        public void Initialize(EnemyType type, Transform pathTransform, float speedMultiplier) {
            //init speed
            switch (type) {
                case EnemyType.Shooter:
                    _speed = 1f;
                    break;

                case EnemyType.Bomber:
                    _speed = 0.5f;
                    break;

                case EnemyType.Laserer:
                    _speed = 2f;
                    break;
            }

            //init speed multiplier 
            _speedMultiplier = speedMultiplier;

            //init waypoints 
            _waypoints = new Transform[pathTransform.childCount];
            for (int i = 0; i < pathTransform.childCount; i++) {
                _waypoints[i] = pathTransform.GetChild(i).transform;
            }
        }

        void Update() {
            if (DataAndStates.Instance.isGameStart) {
                FollowPath();
            }
        }

        void FollowPath() {
            if (_waypointIndex < _waypoints.Length) {
                Vector3 targetPosition = _waypoints[_waypointIndex].position;
                Debug.Log(targetPosition);
                float moveDelta = _speed * _speedMultiplier * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveDelta);

                if (transform.position == targetPosition) {
                    _waypointIndex++;
                }
            } 
            else 
            {
                PoolManager.Instance.Return(gameObject);
            }
        }
    }
}