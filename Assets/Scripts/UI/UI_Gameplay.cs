using UnityEngine;

namespace SpaceInvader
{
    public class UI_Gameplay : MonoBehaviour
    {
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
            GameplayInitializer.Instance.ui_endScreen.AnimateScoreText();
            GameplayInitializer.Instance.ui_endScreen.AnimateCoinText();
        }
    }
}
