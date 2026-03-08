using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : PersistentSingleton<SceneLoadManager>
{
    public string LoadSceneName;
    public string GameSceneName;

    public void LoadScene()
    {
        SceneManager.LoadScene(LoadSceneName, LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == LoadSceneName)
        {
            var op = SceneManager.LoadSceneAsync(GameSceneName, LoadSceneMode.Single);
            SceneLoader.Instance.Initialize(op);
        }
    }
}
