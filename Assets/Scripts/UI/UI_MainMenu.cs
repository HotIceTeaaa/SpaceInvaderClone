using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvader
{
    public class UI_Manager : MonoBehaviour
    {
        [SerializeField] private GameObject[] buyButtonGameObjects;
        [SerializeField] private Button[] selectButtons;
        private Button selectedButton;
        public static UI_Manager Instance { get; private set; }

        void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        public void Initialize()
        {
            //init ship mana yg udh dibeli, kalo udh dibeli button buy g perlu ditunjukin
            for(int i = 0; i < buyButtonGameObjects.Length; i++)
            {
                if(PlayerPreferences.Instance.GetInt($"ship{i}bought", 0) == 1)
                {
                    buyButtonGameObjects[i].SetActive(false);
                }
            }

            //init ship mana yg terakhir diselect
            int j = PlayerPreferences.Instance.GetInt("shipType", 0);
            selectedButton = selectButtons[j];
            ButtonUIToSelected(selectedButton);
        }

        public void BuyShip(int i)
        {
            if(PlayerPreferences.Instance.GetInt("coin", 0) >= 1000)
            {
                int coin = PlayerPreferences.Instance.GetInt("coin", 0);
                coin -= 1000;
                PlayerPreferences.Instance.SaveInt("coin", coin);

                PlayerPreferences.Instance.SaveInt($"ship{i}bought", 1);
                buyButtonGameObjects[i].SetActive(false);
            }
        }

        public void SelectShip(int i)
        {
            PlayerPreferences.Instance.SaveInt("shipType", i);

            ButtonUIToSelect(selectedButton);   //selectedButton adalah button sebelomnya yg dipilih 
            Button button = selectButtons[i];
            selectedButton = button;    //selectedButton diganti jadi button yg dipilih sekarang
            ButtonUIToSelected(button);
        }

        private void ButtonUIToSelected(Button button)
        {
            button.interactable = false;
            TMP_Text text = button.GetComponentInChildren<TMP_Text>();
            text.text = "Selected";
        }

        private void ButtonUIToSelect(Button button)
        {
            button.interactable = true;
            TMP_Text text = button.GetComponentInChildren<TMP_Text>();
            text.text = "Select";
        }

        public void PlayButton()
        {
            SceneHandler.Instance.LoadNextScene();
        }
    }
}
