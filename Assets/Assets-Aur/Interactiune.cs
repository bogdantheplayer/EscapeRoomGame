using UnityEngine;

public class Interactiune : MonoBehaviour
{
    public float distantaActiune = 3f; // Cat de aproape de usa trebuie sa fii
    public GameObject canvasIntrebari; // Aici vom atasa Canvas-ul

    [Header("Sistem Bilet")]
    public GameObject panouBiletUI; // Tragem panoul cu X aici in Inspector
    private GameObject biletulDePeJos; // Aici va tine minte ce bilet a ridicat

    [Header("Puzzle Cesti")]
    public GameObject textSecretCesti; // Tragem aici textul "Inima reginei"
    private int cestiSparte = 0;       // Memoreaza cate am spart
    public int totalCestiDeSpart = 3;  // De cate e nevoie pentru a castiga

    private Camera cameraPrincipala; // NOU: Variabila pentru a gasi camera

    void Start()
    {
        // NOU: Initializam camera la pornirea jocului
        cameraPrincipala = GetComponent<Camera>();
        if (cameraPrincipala == null)
        {
            cameraPrincipala = Camera.main;
        }
    }

    void Update()
    {
        // Cand apasam E pe tastatura
        if (Input.GetKeyDown(KeyCode.E))
        {
            // MODIFICAREA PRINCIPALA: 
            // Tragem o raza din camera, trecand exact prin coordonatele cursorului de la mouse de pe ecran
            Ray raza = cameraPrincipala.ScreenPointToRay(Input.mousePosition);
            RaycastHit ceAmLovit;

            // Daca raza loveste ceva in limita a 3 metri...
            if (Physics.Raycast(raza, out ceAmLovit, distantaActiune))
            {
                Debug.Log("Raza a lovit: " + ceAmLovit.collider.name + " care are tag-ul: " + ceAmLovit.collider.tag);

                // ...si acel ceva are eticheta "Usa"
                if (ceAmLovit.collider.CompareTag("Usa"))
                {
                    DeschideUI();
                }
                else if (ceAmLovit.collider.CompareTag("Bilet"))
                {
                    // Salvam obiectul 3D ca sa il putem pune la loc mai tarziu
                    biletulDePeJos = ceAmLovit.collider.gameObject;
                    CitesteBilet();
                }
                // DACA AM LOVIT O CEASCA (CEVA CASABIL)
                else if (ceAmLovit.collider.CompareTag("Casabil"))
                {
                    // 1. Distrugem ceasca
                    Destroy(ceAmLovit.collider.gameObject);

                    // 2. Adaugam 1 la numaratoarea de cesti sparte
                    cestiSparte++;

                    // 3. Verificam daca am spart destule
                    if (cestiSparte >= totalCestiDeSpart)
                    {
                        RezolvaPuzzleCesti();
                    }
                }
            }
        }
    }

    void DeschideUI()
    {
        // 1. Activam Canvas-ul sa apara pe ecran
        canvasIntrebari.SetActive(true);

        // 2. Deblocam cursorul de la mouse ca sa poti da click pe butoane
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void CitesteBilet()
    {
        // 1. Ascundem biletul 3D de pe jos
        biletulDePeJos.SetActive(false);

        // 2. Aratam biletul UI pe ecran
        panouBiletUI.SetActive(true);

        // 3. Deblocam cursorul de la mouse ca sa poti apasa pe X
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void InchideBilet()
    {
        // 1. Ascundem UI-ul de pe ecran
        panouBiletUI.SetActive(false);

        // 2. Punem biletul 3D inapoi pe jos (il facem vizibil)
        if (biletulDePeJos != null)
        {
            biletulDePeJos.SetActive(true);
        }

        // 3. Ascundem si blocam mouse-ul la loc pentru a continua jocul
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void RezolvaPuzzleCesti()
    {
        // Aprindem textul secret ("Inima reginei")
        if (textSecretCesti != null)
        {
            textSecretCesti.SetActive(true);
        }
    }
}