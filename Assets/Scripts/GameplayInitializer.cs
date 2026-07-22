using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

namespace SpaceInvader
{
    public class GameplayInitializer : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] public CameraShake cameraShakeScript;

        [Header("UI")]
        [SerializeField] public UI_EndScreen ui_endScreen;

        [Header("Core")]
        [SerializeField] public GameObject player;
        [SerializeField] public Player playerScript;
        [SerializeField] public PlayerBullet playerBulletScript;
        
        [Header("Managers")]
        [SerializeField] public GameManager gameManager;

        [Header("Others")]

        public static GameplayInitializer Instance { get; private set; }

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            //DontDestroyOnLoad(gameObject);
        }

        private void Start() {
            gameManager.Initialize();
            cameraShakeScript.Initialize();
        }
    }
}
