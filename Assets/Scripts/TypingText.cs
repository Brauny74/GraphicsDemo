using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Collections;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TypingText : MonoBehaviour
{
    public float TimeBetweenSymbols = 0.1f;
    
    TextMeshProUGUI text;
    string actualText;
    int currentChar = -1;

    bool isStopped = false;

    private bool isTyping;
    public bool IsTyping
    {
        get { return isTyping; }
    }

    public string Text
    {
        get { return text.text; }
        set { 
            SetTypingText(value);
        }
    }

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void SetTypingText(string t) 
    {
        actualText = t;
        currentChar = -1;
        text.text = "<color=#FFFFFF00>" + t + "</color>";
        isTyping = true;
        isStopped = false;
        StartCoroutine("TypingCoroutine");
    }

    public void StopText()
    {
        text.text = actualText;
        isTyping = false;
        isStopped = true;
    }

    private IEnumerator TypingCoroutine()
    {
       yield return new WaitForSeconds(TimeBetweenSymbols);
        if (isStopped)
            yield break;
        currentChar++;
        if (currentChar == actualText.Length)
        {
            StopText();
        }
        else
        {
            text.text = actualText.Substring(0, currentChar) + "<color=#FFFFFF00>" + actualText.Substring(currentChar + 1) + "</color>";
            StartCoroutine("TypingCoroutine");
        }
    }
}
