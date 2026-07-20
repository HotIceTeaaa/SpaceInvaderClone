using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvader
{
    public class UI_Gameplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _multiplierText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private Image _gaugeImage;
        public static UI_Gameplay Instance { get; private set; }

        void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        public void UpdateHPBar() {
            GameplayInitializer.Instance.playerScript.UpdateHPBar();
        }

        public void PlayEndScreen()
        {
            GameplayInitializer.Instance.ui_endScreen.ShowEndScreen();
            StartCoroutine(GameplayInitializer.Instance.ui_endScreen.AnimateScoreText());
            StartCoroutine(GameplayInitializer.Instance.ui_endScreen.AnimateCoinText());
        }

        public void PlayAgain()
        {
            SceneHandler.Instance.ReloadCurrentScene();
        }

        public void ToMainMenu()
        {
            SceneHandler.Instance.LoadSceneByIndex(0);
        }

        public void UpdateScoreValue()
        {
            _scoreText.text = $"{DataAndStates.Instance.score}";
        }

        public void UpdateMultiplierValue()
        {
            _multiplierText.text = $"{DataAndStates.Instance.level}X";
        }

        public void UpdateGaugeValue()
        {
            float fillAmount = DataAndStates.Instance.point / GameManager.Instance._increaseLevelThreshold;
            _gaugeImage.fillAmount = fillAmount;
        }

        public void ShowScoreIndicator(Vector3 position, float scoreWorth)
        {
            GameObject gameObject = PoolManager.Instance.GetAndSetPositionRotationScoreIndicator(position, Quaternion.identity);
            UI_ScoreIndicator scoreIndicatorScript = gameObject.GetComponentInChildren<UI_ScoreIndicator>();

            scoreIndicatorScript.SetScore(scoreWorth);
        }
    }
}
