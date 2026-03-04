using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLockPlayer : MonoBehaviour
{
    public void OnPause(InputAction.CallbackContext inputContext)
    {
        if (inputContext.ReadValueAsButton())
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
