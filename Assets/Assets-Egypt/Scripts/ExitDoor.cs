using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    public float interactDistance=3f;
    public TMP_Text messageText;
    public bool gameFinished = false;
    private Transform player;

    void Start()
    {
        player=GameObject.FindGameObjectWithTag("Player").transform;

        if(messageText!=null)
        {
            messageText.text="";
        }
    }

    void Update()
    {
        if (gameFinished) return; 

        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= interactDistance)
        {
            if (QuestionManager.instance.AllQuestionsAnswered())
            {
                messageText.text = "Press E to leave";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    EndGame();
                }
            }
            else
            {
                messageText.text = "You need to answer all 5 hidden questions first";
            }
        }
        else
        {
            messageText.text = "";
        }
    }

    void EndGame()
    {
        Debug.Log("Game completed!");
        messageText.text= "Felicitari! Ai scapat din ultima camera!";
        gameFinished = true;
    }
}