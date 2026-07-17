
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvader
{
    public class HP_Test : MonoBehaviour
    {
        [Header("Health Bar Related")]
        [SerializeField] private Image image;

        [ContextMenu("simulate kena hit")]
        public void UpdateHPBar()
        {
            DataAndStates.Instance.hp -= 1;
            float fillAmount = DataAndStates.Instance.hp / DataAndStates.Instance.maxHP;
            image.fillAmount = fillAmount;
        }
    }
}
