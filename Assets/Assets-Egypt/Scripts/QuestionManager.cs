using UnityEngine;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager instance;

    public int answeredQuestions=0;
    public int totalQuestions=5;

    public TMP_Text doorCounterText;

    void Awake()
    {
        instance=this;
    }

    void Start()
    {
        UpdateDoorCounter();
    }

    public void AddCorrectAnswer()
    {
        answeredQuestions++;
        UpdateDoorCounter();
    }

    public bool AllQuestionsAnswered()
    {
        return answeredQuestions>=totalQuestions;
    }

    void UpdateDoorCounter()
    {
        if(doorCounterText!=null)
        {
            doorCounterText.text=answeredQuestions+" / "+totalQuestions;
        }
    }
}