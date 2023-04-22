using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class CarousselClues : MonoBehaviour
{
    public Button prevButton;
    public Button nextButton;
    public Button closeButton;
    public GameObject noneClue;
    public int currentPosition;
    public GameObject positionClues;
    public GameObject orderClue;
    public GameObject inventoryCluesFound;
    public Animator animator;
    public bool ckeckClickGameColor;
    public bool listenersAttached = false;
    public static CarousselClues instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance du caroussel dans la scène ");
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        Debug.Log("AWAKE");
    }
    // Use this for initialization
    void Start()
    {
        Initiate();
        Debug.Log("START");
    }

    //On récupère tous les objets de la scne nécessaire au fonctionnement du caroussel
    public void Initiate()
    {
        ckeckClickGameColor = true;
        //On récupère les objets de l'interface
        GameObject btnPrev = GameObject.Find("goPrevious");
        prevButton = btnPrev.GetComponent<Button>();
        GameObject btnNext = GameObject.Find("goNext");
        nextButton = btnNext.GetComponent<Button>();

        GameObject btnClose = GameObject.Find("close");
        closeButton = btnClose.GetComponent<Button>();


        positionClues = GameObject.Find("positionClues");
        orderClue = GameObject.Find("clueOrder");
        noneClue = GameObject.Find("noneClue");
        noneClue.SetActive(true);

        inventoryCluesFound = GameObject.Find("CluesFound");
        animator = inventoryCluesFound.GetComponent<Animator>();

        prevButton.onClick.RemoveAllListeners(); //On retire la mémoire du gestionnaire d'événenements
        nextButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();

        // ajoute les listeners pour les boutons
        prevButton.onClick.AddListener(DisplayPreviousClue);
        nextButton.onClick.AddListener(DisplayNextClue);
        closeButton.onClick.AddListener(CloseCaroussel);
        listenersAttached = true;
        



    }

    //On ouvre le caroussel
    public void OpenCaroussel()
    {
        currentPosition = 0;
        ckeckClickGameColor = false;
        animator.SetBool("isOpen", true);
        DisplayCluesFind();
    }

    //Affiche les indices trouvés
    public void DisplayCluesFind()
    {
        Debug.Log("Je suis deisplay");
        //On désactive pour l'instant les indices affichés
        orderClue.SetActive(false);
        positionClues.SetActive(false);

        foreach(int id in InventoryManager.inventory.cluesFound)
        {
            Debug.Log(id);
        }
        
        //Si aucun indice n'a été trouvé, on affiche un message par défaut
        if (InventoryManager.inventory.cluesFound.Count != 0)
        {
            noneClue.SetActive(false);


            //Récupération de l'id de l'indice à la position actuelle
            int id = InventoryManager.inventory.cluesFound[currentPosition];
            Debug.Log("curentPosition : " + currentPosition);

            Debug.Log("id : " + id);
            //Si c'est l'indice permettant de savoir l'ordre des clics sur les cubes
            if (id == 4)
            {
                orderClue.SetActive(true);
                InventoryManager.inventory.InstanciateCluesOrder(".2");

            }
            else
            {
                Debug.Log("J'affiche");
                positionClues.SetActive(true);
                InventoryManager.inventory.InstanciateClueCubesPosition(id, ".2");

            }
        }
        

    }

    //Affiche l'indice précédent
    public void DisplayPreviousClue()
    {
        Debug.Log("Je suis previous");
        currentPosition--;
        if (currentPosition <0)
        {
            currentPosition = 0;
        }
        
        DisplayCluesFind();
    }

    //Affiche l'indice suivant
    public void DisplayNextClue()
    {
        Debug.Log("Je suis next");
        currentPosition++;
        if (currentPosition >= InventoryManager.inventory.cluesFound.Count)
        {
            currentPosition = 0;
        }
        DisplayCluesFind();
    }

    //Ferme le caroussel
    public void CloseCaroussel()
    {
        
        animator.SetBool("isOpen", false);
        ckeckClickGameColor = true;
    }


}
