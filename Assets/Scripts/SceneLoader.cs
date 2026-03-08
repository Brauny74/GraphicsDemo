using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : PersistentSingleton<SceneLoader>
{
    public ProgressBarBehavior progressBar;
    AsyncOperation operation;

    public void Initialize(AsyncOperation op)
    {
        operation = op;
    }

    public void Update()
    {
        if (operation != null)
        {
            progressBar.UpdateBar(operation.progress);
        }
    }
}
