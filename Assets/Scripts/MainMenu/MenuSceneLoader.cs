using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class MenuSceneLoader : MonoBehaviour
    {
        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private Slider progressBar;

        private const string GameSceneName = "Game";
        private void Awake()
        {
            loadingScreen.gameObject.SetActive(false);
            progressBar.value = 0;
        }

        public void LoadGameSceneAsync()
        {
            StartCoroutine(LoadAsynchronously());
        }

        IEnumerator LoadAsynchronously()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(GameSceneName);

            loadingScreen.SetActive(true);

            while (!operation.isDone)
            {
                var progress = Mathf.Clamp01(operation.progress / 0.9f);
                progressBar.value = progress;

                yield return null;
            }

            loadingScreen.SetActive(false);
        }
        
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; 
            SceneManager.sceneUnloaded -= OnSceneUnloaded; 
        }
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("Scene Loaded: " + scene.name);
        }

        private void OnSceneUnloaded(Scene scene)
        {
            Debug.Log("Scene Unloaded: " + scene.name); 
        }
    }
}
