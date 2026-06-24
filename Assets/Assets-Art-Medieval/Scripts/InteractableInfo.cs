using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableInfo : MonoBehaviour
{
    [TextArea(2, 5)]
    public string infoTitle;

    [TextArea(5, 10)]
    public string infoText;

    public void Interact()
    {
        string fullMessage = infoTitle + "\n\n" + infoText;

        Debug.Log(fullMessage);

        if (ScreenMessageUI.Instance != null)
        {
            ScreenMessageUI.Instance.ShowMessage(fullMessage);
        }
    }
}