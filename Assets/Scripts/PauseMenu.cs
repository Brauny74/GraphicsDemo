using UnityEngine;

public class PauseMenu : PersistentSingleton<PauseMenu>
{
    public bool IsOpened { get; private set; }
    [SerializeField]
    private RectTransform MenuObject;

    public void Open()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        MenuObject.gameObject.SetActive(true);
        IsOpened = true;
    }

    public void Close()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        MenuObject.gameObject.SetActive(false);
        IsOpened = false;
    }
}
