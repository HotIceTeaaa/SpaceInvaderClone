using UnityEngine;

namespace SpaceInvader {
    public class PlayerMovement : MonoBehaviour {
        [Header("Player Related")]
        [SerializeField] private Rigidbody2D _rigidBody;
        [SerializeField] private float _speed = 4f;

        [Header("Paddings")]
        [SerializeField] private float _rightPadding;
        [SerializeField] private float _leftPadding;
        [SerializeField] private float _topPadding;
        [SerializeField] private float _bottomPadding;

        private new Camera camera;
        private Vector3 _minClamp;
        private Vector3 _maxClamp;

        private void Start() {
            camera = Camera.main;

            _minClamp = camera.ViewportToWorldPoint(new Vector2(0, 0));
            _maxClamp = camera.ViewportToWorldPoint(new Vector2(1, 1));
        }

        public void HandleMovement(Vector2 movementVector) {
            _rigidBody.linearVelocity = movementVector.normalized * _speed;
            Vector2 newPlayerPos = _rigidBody.position;

            newPlayerPos.x = Mathf.Clamp(newPlayerPos.x, _minClamp.x + _leftPadding, _maxClamp.x - _rightPadding);
            newPlayerPos.y = Mathf.Clamp(newPlayerPos.y, _minClamp.y + _bottomPadding, _maxClamp.y - _topPadding);

            _rigidBody.position = newPlayerPos;
        }
    }

}
