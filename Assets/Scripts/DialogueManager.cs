using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[Serializable]
public class Dialogue
{
    [SerializeField]
    private List<string> Lines;

    public string this[int i]
    {
        get { return Lines[i]; }
    }

    public int Length
    { 
        get { return Lines.Count; }
    }
}

public class DialogueManager : PersistentSingleton<DialogueManager>
{
    public Canvas DialogueCanvas;

    public TypingText DialogueText;
    private Dialogue dialogue;
    private int currentLine;

    private bool isShowingDialogue;
    private bool isClickOnCooldown;

    public UnityEvent OnDialogueFinished;

    private void Start()
    {
        isShowingDialogue = false;
        DialogueCanvas.gameObject.SetActive(false);
    }

    public void StartDialogue(Dialogue newDialogue)
    {
        if (isShowingDialogue)
            return;
        isShowingDialogue = true;
        isClickOnCooldown = false;
        DialogueCanvas.gameObject.SetActive(true);
        dialogue = newDialogue;
        currentLine = 0;
        DialogueText.Text = dialogue[currentLine];
    }

    public void NextLine()
    {
        currentLine++;
        if (currentLine < dialogue.Length)
            DialogueText.Text = dialogue[currentLine];
        else
            EndDialogue();
    }

    public void InterruptDialogue()
    {
        DialogueText.StopText();
    }

    public void EndDialogue()
    {
        DialogueCanvas.gameObject.SetActive(false);
        currentLine = 0;
        isShowingDialogue = false;
        isClickOnCooldown = false;
        OnDialogueFinished.Invoke();        
    }

    public void OnClick(InputAction.CallbackContext inputContext)
    {
        if (!isShowingDialogue || isClickOnCooldown)
            return;
        isClickOnCooldown = true;
        if (DialogueText.IsTyping)
            InterruptDialogue();
        else
            NextLine();
        StartCoroutine("ClickCooldown");
    }

    IEnumerator ClickCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        isClickOnCooldown = false;
    }
}
