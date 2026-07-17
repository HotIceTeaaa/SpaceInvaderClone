using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

namespace SpaceInvader
{
    public class GameplayInitializer : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] new public Camera camera;
        [SerializeField] public CameraShake cameraShakeScript;
        [SerializeField] public Light2D globalLight;

        [Header("UI")]
        [SerializeField] public Canvas canvas;
        [SerializeField] public GameObject background;
        [SerializeField] public GameObject fog;
        [SerializeField] public UI_Gameplay ui_gameplay;
        [SerializeField] public UI_EndScreen ui_endScreen;
        [SerializeField] public EventSystem eventSystem;

        [Header("Core")]
        [SerializeField] public GameObject player;
        [SerializeField] public Player playerScript;
        [SerializeField] public DataAndStates dataAndStates;
        
        [Header("Managers")]
        [SerializeField] public GameManager gameManager;
        [SerializeField] public WaveManager waveManager;
        [SerializeField] public PoolManager poolManager;
        [SerializeField] public SceneHandler sceneHandler;
        [SerializeField] public PlayerPreferences prefs;

        [Header("Others")]
        [SerializeField] public WaveSO[] waveSOs;
        //[SerializeField] public GameObject endScreenPanel;
        //[SerializeField] public HP_Test HP_Test;

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

            dataAndStates.Initialize();
            gameManager.GameStart();
            cameraShakeScript.Initialize();
        }
    }
}
