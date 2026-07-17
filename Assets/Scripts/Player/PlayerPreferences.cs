using UnityEngine;

namespace SpaceInvader
{
    public class PlayerPreferences : MonoBehaviour {
        public static PlayerPreferences Instance { get; private set; }

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        public void SaveFloat(string key, float value) {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }

        public float GetFloat(string key, float defaultValue) {
            return PlayerPrefs.GetFloat(key, defaultValue);
        }

        public void SaveInt(string key, int value) {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }

        public int GetInt(string key, int defaultValue) {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        [ContextMenu("Clear All Keys")]
        public void ClearAllKeys() {
            PlayerPrefs.DeleteAll();
        }

        [ContextMenu("Print Coins")]
        public void PrintCoins() {
            int coins = PlayerPrefs.GetInt("coins", -999);
            Debug.Log(coins);
        }
    }
}