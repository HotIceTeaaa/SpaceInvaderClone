using TMPro;
using UnityEngine;

namespace SpaceInvader
{
    public class UI_EndScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _endScreenPanel;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _coinsText;

        public void ShowEndScreen()
        {
            _endScreenPanel.SetActive(true);
        }

        public void AnimateScoreText(){
            float temp = 0f;

            while (temp < DataAndStates.Instance.score)
            {
                _scoreText.text = $"{temp}";
                temp += Random.Range(10, 20);
            }

            _scoreText.text = $"{DataAndStates.Instance.score}";
        }

        public void AnimateCoinText(){
            float temp = 0f;

            while (temp < DataAndStates.Instance.coin)
            {
                _coinsText.text = $"{temp}";
                temp += Random.Range(10, 20);
            }

            _coinsText.text = $"{DataAndStates.Instance.coin}";
        }
    }
}
