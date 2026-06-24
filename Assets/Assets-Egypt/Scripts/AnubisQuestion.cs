using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnubisQuestion : MonoBehaviour
{
    public GameObject questionPanel;
    public GameObject darkBackground;

    public TMP_Text questionText;

    public Button answerButton1;
    public Button answerButton2;
    public Button answerButton3;
    public Button answerButton4;
    public Button closeButton;

    public string question="Who is Anubis in Egyptian mythology?";

    public string answer1="God of the dead";
    public string answer2="God of the sea";
    public string answer3="God of thunder";
    public string answer4="God of the sun";

    public int correctAnswer=1;

    public float interactDistance=3f;

    public MonoBehaviour playerController;

    private Transform player;
    private bool alreadyAnswered=false;
    private bool questionOpen=false;

    void Start()
    {
        player=GameObject.FindGameObjectWithTag("Player").transform;

        questionPanel.SetActive(false);

        if(darkBackground!=null)
        {
            darkBackground.SetActive(false);
        }
    }

    void Update()
    {
        if(questionOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseQuestion();
            return;
        }

        if(alreadyAnswered || questionOpen)
        {
            return;
        }

        float distance=Vector3.Distance(player.position,transform.position);

        if(distance<=interactDistance && Input.GetKeyDown(KeyCode.E))
        {
            OpenQuestion();
        }
    }

    void OpenQuestion()
    {
        questionOpen=true;

        questionPanel.SetActive(true);

        if(darkBackground!=null)
        {
            darkBackground.SetActive(true);
        }

        questionText.text=question;

        answerButton1.GetComponentInChildren<TMP_Text>().text=answer1;
        answerButton2.GetComponentInChildren<TMP_Text>().text=answer2;
        answerButton3.GetComponentInChildren<TMP_Text>().text=answer3;
        answerButton4.GetComponentInChildren<TMP_Text>().text=answer4;

        answerButton1.onClick.RemoveAllListeners();
        answerButton2.onClick.RemoveAllListeners();
        answerButton3.onClick.RemoveAllListeners();
        answerButton4.onClick.RemoveAllListeners();

        answerButton1.onClick.AddListener(()=>CheckAnswer(1));
        answerButton2.onClick.AddListener(()=>CheckAnswer(2));
        answerButton3.onClick.AddListener(()=>CheckAnswer(3));
        answerButton4.onClick.AddListener(()=>CheckAnswer(4));

        if(closeButton!=null)
        {
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(CloseQuestion);
        }

        LockPlayer(true);
    }

    void CheckAnswer(int selectedAnswer)
    {
        if(selectedAnswer==correctAnswer)
        {
            alreadyAnswered=true;
            QuestionManager.instance.AddCorrectAnswer();

            Debug.Log("Correct answer!");
        }
        else
        {
            Debug.Log("Wrong answer!");
        }

        CloseQuestion();
    }

    void CloseQuestion()
    {
        questionOpen=false;

        questionPanel.SetActive(false);

        if(darkBackground!=null)
        {
            darkBackground.SetActive(false);
        }

        LockPlayer(false);
    }

    void LockPlayer(bool locked)
    {
        if(playerController!=null)
        {
            playerController.enabled=!locked;
        }

        if(locked)
        {
            Cursor.lockState=CursorLockMode.None;
            Cursor.visible=true;
        }
        else
        {
            Cursor.lockState=CursorLockMode.Locked;
            Cursor.visible=false;
        }
    }
}