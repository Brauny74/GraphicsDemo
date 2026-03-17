using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Events;

public class UsableActivateEvent : Usable
{
    public UnityEvent OnUse;

    public override void Use()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        OnUse.Invoke();
    }

    public void UseFinished()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        OnUsingFinished.Invoke();
    }
}
