using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InfiniteRunner {
    public class SceneHandler : MonoBehaviour {
        public static SceneHandler Instance { get; private set; }
        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);
        }


        public void LoadSceneByString(string sceneName) {
            SceneManager.LoadScene(sceneName);
        }

        public void LoadSceneByIndex(int indexScene) {
            SceneManager.LoadScene(indexScene);
        }

        public void ReloadCurrentScene() {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
            Debug.Log("Scene reloaded!");
        }

        public void LoadNextScene() {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            // Check if there is a next scene
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) {
                SceneManager.LoadScene(nextSceneIndex);
            } else {
                Debug.Log("This is the last scene!");
            }
        }

        public void LoadSceneAsynchronously(string sceneName) {
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        IEnumerator LoadSceneAsync(string sceneName) {
            Debug.Log("Starting to load scene: " + sceneName);

            // Start loading the scene in the background
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            // Don't activate the scene immediately (wait until ready)
            asyncLoad.allowSceneActivation = false;

            // Show loading progress
            while (!asyncLoad.isDone) {
                // Progress goes from 0 to 0.9 (90%), then waits for activation
                float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
                Debug.Log($"Loading progress: {progress * 100}%");

                // When loading is almost complete, activate the scene
                if (asyncLoad.progress >= 0.9f) {
                    Debug.Log("Load complete! Activating scene...");
                    asyncLoad.allowSceneActivation = true;
                }

                yield return null; // Wait one frame before checking again
            }
        }

        public void LoadAdditiveScene(string sceneName) {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            Debug.Log($"Loaded {sceneName} additively");
        }

        public void UnloadAdditiveScene(string sceneName) {
            Scene scene = SceneManager.GetSceneByName(sceneName);
            if (scene.isLoaded) {
                SceneManager.UnloadSceneAsync(scene);
                Debug.Log($"Unloaded {sceneName}");
            }
        }

        public int getSceneCount() {
            return SceneManager.sceneCountInBuildSettings;
        }
    }
}
