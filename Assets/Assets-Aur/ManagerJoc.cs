using UnityEngine;
using TMPro;

[System.Serializable]
public class Intrebare
{
    [TextArea]
    public string textulIntrebarii;
    public string raspunsA;
    public string raspunsB;
    public string raspunsC;
    public string raspunsD;
    public int indexRaspunsCorect;
}

public class ManagerJoc : MonoBehaviour
{
    [Header("Lista ta de intrebari")]
    public Intrebare[] listaIntrebari;
    private int intrebareaCurenta = 0;

    [Header("Legaturi cu Interfata (UI)")]
    public TextMeshProUGUI textIntrebareUI;
    public TextMeshProUGUI textButonA;
    public TextMeshProUGUI textButonB;
    public TextMeshProUGUI textButonC;
    public TextMeshProUGUI textButonD;
    public GameObject panouIntrebari;

    [Header("Legatura cu Usa")]
    public ControlUsa scriptulUsii;

    void Start()
    {
        if (listaIntrebari.Length > 0)
        {
            AfiseazaIntrebarea();
        }
    }

    public void AfiseazaIntrebarea()
    {
        Intrebare intrebareAcum = listaIntrebari[intrebareaCurenta];
        textIntrebareUI.text = intrebareAcum.textulIntrebarii;
        textButonA.text = intrebareAcum.raspunsA;
        textButonB.text = intrebareAcum.raspunsB;
        textButonC.text = intrebareAcum.raspunsC;
        textButonD.text = intrebareAcum.raspunsD;
    }

    public void VerificaRaspunsul(int indexAles)
    {
        if (indexAles == listaIntrebari[intrebareaCurenta].indexRaspunsCorect)
        {
            Debug.Log("Raspuns Corect!");
            intrebareaCurenta++;

            if (intrebareaCurenta >= listaIntrebari.Length)
            {
                scriptulUsii.DeschideUsa();
                InchidePanoul(); // Folosim noua functie si cand castigam!
            }
            else
            {
                AfiseazaIntrebarea();
            }
        }
        else
        {
            Debug.Log("Raspuns Gresit! Incearca din nou.");
        }
    }

    // NOUA FUNCTIE PENTRU BUTONUL X
    public void InchidePanoul()
    {
        panouIntrebari.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}