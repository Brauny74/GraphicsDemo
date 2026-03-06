using UnityEngine;

public class UsableShowDialogue : Usable
{
    public Dialogue Dialogue;

    public override void Use()
    {
        DialogueManager.Instance.StartDialogue(Dialogue);
        DialogueManager.Instance.OnDialogueFinished.AddListener(FinishUsing);
    }

    public void FinishUsing()
    {
        OnUsingFinished.Invoke();
        DialogueManager.Instance.OnDialogueFinished.RemoveListener(FinishUsing);
    }
}
