using UnityEngine;

public class TorchInteract : MonoBehaviour
{
    public GameObject fireObject;
    public float interactDistance=3f;

    private Transform player;
    private bool isLit=false;

    void Start()
    {
        player=GameObject.FindGameObjectWithTag("Player").transform;

        if(fireObject!=null)
        {
            fireObject.SetActive(false);
        }
    }

    void Update()
    {
        float distance=Vector3.Distance(player.position,transform.position);

        if(distance<=interactDistance && Input.GetKeyDown(KeyCode.E))
        {
            ToggleFire();
        }
    }

    void ToggleFire()
    {
        isLit=!isLit;

        if(fireObject!=null)
        {
            fireObject.SetActive(isLit);
        }
    }
}