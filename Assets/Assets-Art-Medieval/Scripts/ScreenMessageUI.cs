using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMessageUI : MonoBehaviour
{
    public static ScreenMessageUI Instance;

    private string message = "";
    private bool isVisible = false;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowMessage(string newMessage)
    {
        message = newMessage;
        isVisible = true;
    }

    public void HideMessage()
    {
        isVisible = false;
        message = "";
    }

    private void Update()
    {
        if (!isVisible) return;

        if (Input.anyKeyDown &&
            !Input.GetKeyDown(KeyCode.E) &&
            !Input.GetKeyDown(KeyCode.Alpha0) &&
            !Input.GetKeyDown(KeyCode.Alpha1) &&
            !Input.GetKeyDown(KeyCode.Alpha2) &&
            !Input.GetKeyDown(KeyCode.Alpha3))
        {
            HideMessage();
        }
    }

    private void OnGUI()
    {
        if (!isVisible) return;

        float boxWidth = 760f;
        float boxHeight = 240f;
        float x = (Screen.width - boxWidth) / 2f;
        float y = (Screen.height - boxHeight) / 2f;

        GUIStyle style = new GUIStyle(GUI.skin.box);
        style.fontSize = 24;
        style.alignment = TextAnchor.MiddleCenter;
        style.wordWrap = true;

        GUI.Box(new Rect(x, y, boxWidth, boxHeight), message, style);
    }
}
