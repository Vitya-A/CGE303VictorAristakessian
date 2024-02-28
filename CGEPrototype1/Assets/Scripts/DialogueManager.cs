using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Must add to use TMP Text
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text textbox;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    // Set these references in the inspector
    public GameObject continueButton;
    public GameObject dialoguePanel;

    void OnEnable()
    {
        // Disables the continue button until the text is done
        continueButton.SetActive(false);
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        textbox.text = "";
        foreach(char letter in sentences[index])
        {
            textbox.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        
        // Re-enables the button now that the sentence is done
        continueButton.SetActive(true);
    }

    public void NextSentence()
    {
        // Disables the continue button until next sentnence is done
        continueButton.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            textbox.text = "";
            StartCoroutine(Type());
        } else
        {
            textbox.text = "";
            // Disables text box now that all text is done
            dialoguePanel.SetActive(false);
        }
    }
}
