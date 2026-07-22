using System.Collections;
using UnityEngine;

namespace SpaceInvader
{
    public class UI_Button : MonoBehaviour
    {
        [SerializeField] private RectTransform _buttonRectTransform;

        [Header("Button Animation Related")]
        [SerializeField] private float _waitInterval;
        [SerializeField] private float _targetSize;

        public void PointerEnter()
        {
            SFXManager.Instance.PlaySFX(SFX.ButtonHover);
            StartCoroutine(ButtonIncreaseSize());
        }
        public void PointerExit()
        {
            StartCoroutine(ButtonDecreaseSize());
        }

        public void PointerDown()
        {
            SFXManager.Instance.PlaySFX(SFX.ButtonClick);
        }

        private IEnumerator ButtonIncreaseSize()
        {
            while (_buttonRectTransform.localScale.x < _targetSize) //assuming size di lock
            {
                _buttonRectTransform.localScale = new Vector3(_buttonRectTransform.localScale.x + 0.01f,
                                                              _buttonRectTransform.localScale.y + 0.01f,
                                                              _buttonRectTransform.localScale.z + 0.01f);

                yield return new WaitForSeconds(_waitInterval);
            }
        }

        private IEnumerator ButtonDecreaseSize()
        {
            while (_buttonRectTransform.localScale.x > 1f) //assuming size di lock
            {
                _buttonRectTransform.localScale = new Vector3(_buttonRectTransform.localScale.x - 0.01f,
                                                              _buttonRectTransform.localScale.y - 0.01f,
                                                              _buttonRectTransform.localScale.z - 0.01f);

                yield return new WaitForSeconds(_waitInterval);
            }
        }
    }
}
