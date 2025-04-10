using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public string[] dialogueLines;
    private int currentLineIndex = 0;
    public float typeSpeed = 0.05f;
    public GameObject textBubble;

    private Coroutine typeCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        if (dialogueText == null)
        {
            Debug.LogError("Not assigned");
            enabled = false;
            return;
        }

        if (dialogueLines.Length == 0)
        {
            Debug.LogWarning("No lines set");
            enabled = false;
            return;
        }
        DisplayNextLine();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (typeCoroutine != null)
            {
                StopCoroutine(typeCoroutine);
                dialogueText.text = dialogueLines[currentLineIndex - 1];
                typeCoroutine = null;
            }
            else
            {
                DisplayNextLine();
            }
            
        }
    }
    void DisplayNextLine()
    {
        if (currentLineIndex >= dialogueLines.Length)
        {
            Debug.Log("Dialogue Finished!");
            dialogueText.text = "";
            enabled = false;
            SceneManager.LoadScene("Main");
            textBubble.SetActive(false);
            return;
        }

        if (typeCoroutine != null)
        {
            StopCoroutine(typeCoroutine);
        }
        typeCoroutine = StartCoroutine(TypeSentence(dialogueLines[currentLineIndex]));
        currentLineIndex++;
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
        typeCoroutine = null;
    }

    public void StartDialogue(string[] newDialogueLines)
    {
        dialogueLines = newDialogueLines;
        currentLineIndex = 0;
        enabled = true;
        DisplayNextLine();
    }
}
