using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlUsa : MonoBehaviour
{
    private bool trebuieSaSeDeschida = false;
    private Quaternion rotatieTinta;
    [SerializeField] private string nextScene = "EgiptScene";

    void Start()
    {
        // Setam ca usa sa se deschida la 90 de grade pe axa Y
        rotatieTinta = Quaternion.Euler(0, 90, 0);
    }

    void Update()
    {
        // Daca a primit comanda, usa se roteste lin in fiecare cadru
        if (trebuieSaSeDeschida == true)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, rotatieTinta, Time.deltaTime * 2f);
            
            SceneManager.LoadScene(nextScene);
        }
    }

    // Aceasta functie va fi chemata cand raspunzi corect la toate intrebarile
    public void DeschideUsa()
    {
        trebuieSaSeDeschida = true;
    }
}