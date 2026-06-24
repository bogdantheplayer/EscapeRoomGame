using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SimpleQuestion
{
    public string question;
    public string[] answers = new string[4];
    public int correctIndex;
}

public class RoomQuizSimple : MonoBehaviour
{
    public SimpleQuestion[] questions = new SimpleQuestion[5];

    [SerializeField] private string nextScene = "Camera2";

    private int currentQuestionIndex = 0;
    private bool solved = false;
    private bool waitingForAnswer = false;

    public bool IsSolved()
    {
        return solved;
    }

    public void Interact()
    {
        if (solved)
        {
            SceneManager.LoadScene(nextScene);
            return;
        }

        if (currentQuestionIndex >= questions.Length)
        {
            solved = true;
            ShowText("Felicitari! Ai completat camera. Interactioneaza din nou pentru a progresa la urmatoarea camera.");
            return;
        }

        waitingForAnswer = true;
        ShowCurrentQuestion();
    }

    private void Update()
    {
        if (!waitingForAnswer || solved) return;

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            CheckAnswer(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CheckAnswer(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CheckAnswer(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CheckAnswer(3);
        }
    }

    private void ShowCurrentQuestion()
    {
        SimpleQuestion q = questions[currentQuestionIndex];

        string questionText =
            "Intrebarea " + (currentQuestionIndex + 1) + " / " + questions.Length + "\n\n" +
            q.question + "\n\n" +
            "0: " + q.answers[0] + "\n" +
            "1: " + q.answers[1] + "\n" +
            "2: " + q.answers[2] + "\n" +
            "3: " + q.answers[3] + "\n\n" +
            "Apasa 0, 1, 2 sau 3.\n" +
            "Daca nu stii raspunsul, apasa orice alta tasta si revino dupa ce mai citesti.";

        ShowText(questionText);
    }

    private void CheckAnswer(int selected)
    {
        SimpleQuestion q = questions[currentQuestionIndex];

        if (selected == q.correctIndex)
        {
            currentQuestionIndex++;
            waitingForAnswer = false;

            if (currentQuestionIndex >= questions.Length)
            {
                solved = true;
                ShowText("Felicitari! Ai completat camera.");
            }
            else
            {
                ShowText("Raspuns corect! Revino pentru intrebarea urmatoare.");
            }
        }
        else
        {
            waitingForAnswer = false;
            ShowText("Raspuns gresit. Mai cauta informatia in camera si incearca din nou.");
        }
    }

    private void ShowText(string text)
    {
        Debug.Log(text);

        if (ScreenMessageUI.Instance != null)
        {
            ScreenMessageUI.Instance.ShowMessage(text);
        }
    }
}