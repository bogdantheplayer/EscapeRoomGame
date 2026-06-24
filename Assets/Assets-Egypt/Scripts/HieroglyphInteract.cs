using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HieroglyphInteract : MonoBehaviour
{
    public GameObject puzzlePanel;

    public TMP_Text questionText;
    public TMP_InputField answerInput;
    public TMP_Text feedbackText;

    public Button exitButton;

    public string question="Decode the hieroglyphs and write the answer:";
    public string correctAnswer="anubis";

    public float interactDistance=3f;

    public MonoBehaviour playerController;

    private Transform player;
    private bool puzzleOpen=false;
    private bool alreadySolved=false;

    void Start()
    {
        player=GameObject.FindGameObjectWithTag("Player").transform;

        if(puzzlePanel!=null)
        {
            puzzlePanel.SetActive(false);
        }

        if(exitButton!=null)
        {
            exitButton.onClick.RemoveAllListeners();
            exitButton.onClick.AddListener(ClosePuzzle);
        }
    }

    void Update()
    {
        if(puzzleOpen)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                ClosePuzzle();
                return;
            }

            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                CheckAnswer();
                return;
            }
        }

        if(alreadySolved || puzzleOpen)
        {
            return;
        }

        float distance=Vector3.Distance(player.position,transform.position);

        if(distance<=interactDistance && Input.GetKeyDown(KeyCode.E))
        {
            OpenPuzzle();
        }
    }

    void OpenPuzzle()
    {
        puzzleOpen=true;

        puzzlePanel.SetActive(true);

        questionText.text=question;
        answerInput.text="";

        if(feedbackText!=null)
        {
            feedbackText.text="";
        }

        LockPlayer(true);

        answerInput.ActivateInputField();
        answerInput.Select();
    }

    void CheckAnswer()
    {
        string playerAnswer=answerInput.text.Trim().ToLower();
        string realAnswer=correctAnswer.Trim().ToLower();

        if(playerAnswer==realAnswer)
        {
            alreadySolved=true;

            if(feedbackText!=null)
            {
                feedbackText.text="Correct!";
            }

            QuestionManager.instance.AddCorrectAnswer();

            ClosePuzzle();
        }
        else
        {
            if(feedbackText!=null)
            {
                feedbackText.text="Wrong answer!";
            }

            answerInput.text="";
            answerInput.ActivateInputField();
            answerInput.Select();
        }
    }

    void ClosePuzzle()
    {
        puzzleOpen=false;

        if(puzzlePanel!=null)
        {
            puzzlePanel.SetActive(false);
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