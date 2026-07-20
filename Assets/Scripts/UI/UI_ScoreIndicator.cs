using System.Collections;
using TMPro;
using UnityEngine;


namespace SpaceInvader
{
    public class UI_ScoreIndicator : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private Color[] _colorsUsed;

        [SerializeField] private float _lifespan;
        [SerializeField] private float _increaseYPositionBy;

        private Vector3 _initPos;

        public void OnEnable()
        {
            _initPos = scoreText.transform.position;

            StartCoroutine(ReturnAfter(gameObject));
            StartCoroutine(Animate());
        }

        public void OnDisable()
        {
            ResetPosition();
        }

        private void ResetPosition()
        {
            scoreText.transform.position = _initPos;
        }

        public void SetScore(float score)
        {
            scoreText.text = $"{score}";
        }

        private IEnumerator Animate()
        {
            int i = 0;
            float currentLifespan = 0f;

            while (currentLifespan < _lifespan)
            {
                scoreText.color = _colorsUsed[i];
                i++;
                i %= _colorsUsed.Length;

                scoreText.transform.position = new Vector3(scoreText.transform.position.x,
                                                            scoreText.transform.position.y + _increaseYPositionBy,
                                                            scoreText.transform.position.z);

                currentLifespan += Time.deltaTime;
                yield return new WaitForSeconds(0.1f);
            }
        }
        
        private IEnumerator ReturnAfter(GameObject obj)
        {
            yield return new WaitForSeconds(_lifespan);

            if (obj.activeSelf)
            {
                PoolManager.Instance.ReturnScoreIndicator(obj);
            }
        }
    }
}
