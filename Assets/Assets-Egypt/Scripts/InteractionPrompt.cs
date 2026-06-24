using UnityEngine;

public class InteractionPrompt : MonoBehaviour
{
    public GameObject promptCanvas;
    public float showDistance=3f;

    private Transform player;
    private Transform cam;

    void Start()
    {
        player=GameObject.FindGameObjectWithTag("Player").transform;
        cam=Camera.main.transform;

        if(promptCanvas!=null)
        {
            promptCanvas.SetActive(false);
        }
    }

    void Update()
    {
        float distance=Vector3.Distance(player.position,transform.position);

        if(distance<=showDistance)
        {
            promptCanvas.SetActive(true);
            FaceCamera();
        }
        else
        {
            promptCanvas.SetActive(false);
        }
    }

    void FaceCamera()
    {
        promptCanvas.transform.LookAt(promptCanvas.transform.position+cam.forward);
    }
}