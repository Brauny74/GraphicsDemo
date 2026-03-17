using UnityEngine;
using PixelCrushers.DialogueSystem;

public class UsableDialogueSystemActivation : Usable
{
    DialogueSystemTrigger trigger;

    private void Start()
    {
        if (OutlineObjectRenderer == null) OutlineObjectRenderer = GetComponent<SkinnedMeshRenderer>();
        _outlineMaterial = OutlineObjectRenderer.materials[OutlineMaterialIndex];
        trigger = GetComponent<DialogueSystemTrigger>();        
    }

    public override void Use()
    {
        PixelCrushers.DialogueSystem.DialogueManager.Instance.conversationEnded += FinishUse;
        trigger.OnUse();       
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;        
    }

    public void FinishUse(Transform actor)
    {
        OnUsingFinished.Invoke();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PixelCrushers.DialogueSystem.DialogueManager.Instance.conversationEnded -= FinishUse;
    }
}
