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
            DisplayNextLine();
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
            return;
        }
    }
}
