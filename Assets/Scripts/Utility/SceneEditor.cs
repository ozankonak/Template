#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Utility
{
    public abstract class SceneEditor
    {
        private const string MenuScenePath = "Assets/Scenes/Menu.unity";
        private const string GameScenePath = "Assets/Scenes/Game.unity";

        private const int GameResolutionIndex = 3;
    
        [MenuItem("Scenes/Play")]
        static void PlayGame()
        {
            EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(0));
            EditorApplication.isPlaying = true;
        }
    
        [MenuItem("Scenes/Menu")]
        static void OpenMenuScene()
        {
            GameViewUtils.SetSize(GameResolutionIndex);
            EditorSceneManager.OpenScene(MenuScenePath);
        }
    
        [MenuItem("Scenes/Game")]
        static void OpenGameScene()
        {
            GameViewUtils.SetSize(GameResolutionIndex);
            EditorSceneManager.OpenScene(GameScenePath);
        }
    }
}
#endif