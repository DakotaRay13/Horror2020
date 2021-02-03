using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text ButtonPromptText;

    // Start is called before the first frame update
    void Start()
    {
        EmptyButtonPrompt();
    }

    public void EmptyButtonPrompt()
    {
        //Empty out the button Prompt
        ButtonPromptText.text = "";
    }

    public void FillButtonPrompt(string Message)
    {
        ButtonPromptText.text = Message;
    }
}
