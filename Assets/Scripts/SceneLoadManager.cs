using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : PersistentSingleton<SceneLoadManager>
{
    public string LoadSceneName;
    public string GameSceneName;

    public void LoadScene()
    {
        var oper = SceneManager.LoadSceneAsync(GameSceneName, LoadSceneMode.Single);
    }
}
