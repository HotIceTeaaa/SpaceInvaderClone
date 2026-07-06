using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceInvader
{
    public class InputReader : MonoBehaviour {

        [Header("Actions")]
        [SerializeField] private InputActionReference _moveAction;
        [SerializeField] private InputActionReference _attackAction;

        [Header("Script Lain")]
        [SerializeField] private PlayerMovement _playerMovementScript;
        [SerializeField] private Player _playerScript;


        private void Update() {
            //wasd pressed
            if (_moveAction.action.IsPressed()) {
                Vector2 movement = _moveAction.action.ReadValue<Vector2>();
                _playerMovementScript.HandleMovement(movement);
            } else {
                _playerMovementScript.HandleMovement(new Vector2(0, 0));
            }

            //left click pressed
            if (_attackAction.action.WasPressedThisFrame()) {
                _playerScript.Shoot();
            }
        }
            

        private void OnEnable() {
            _moveAction.action.Enable();
        }

        private void OnDisable() {
            _moveAction.action.Disable();
        }
    }
}
