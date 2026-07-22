using System.Collections;
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

        public IEnumerator AnimateScoreText(){
            SFXManager.Instance.PlayLoopedSFX(SFX.ScoreIncrements);
            float temp = 0f;

            while (temp < DataAndStates.Instance.score)
            {
                _scoreText.text = $"{temp}";
                temp += Random.Range(10, 20);
                yield return new WaitForSeconds(0.01f);
            }

            _scoreText.text = $"{DataAndStates.Instance.score}";
            SFXManager.Instance.MuteLoopedSFX();
        }

        public IEnumerator AnimateCoinText(){
            float temp = 0f;

            while (temp < DataAndStates.Instance.coin)
            {
                _coinsText.text = $"{temp}";
                temp += Random.Range(10, 20);
                yield return new WaitForSeconds(0.01f);
            }

            _coinsText.text = $"{DataAndStates.Instance.coin}";
        }
    }
}
