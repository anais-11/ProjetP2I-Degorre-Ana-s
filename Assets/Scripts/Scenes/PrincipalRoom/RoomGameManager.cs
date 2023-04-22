using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoomGameManager : MonoBehaviour
{
    public MovePlayer movePlayer;
    public RoomGameManager instance;
    public GameObject WelcomePopUp;
    public GameObject door;
    public GameObject player;
    public Inventory inventory;
    public TimerManager timer;
    public Animator winAnimator;
    public bool doorOpen;
    public bool endGame;
    private Vector2 doorInitialPosition;
    RectTransform rectTransformDoor;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance d'interactionManager dans la scène ");
            return;
        }
        instance = this;
       

    }

    public void Start()
    {
        door = GameObject.Find("door");

        // Récupère le RectTransform de l'objet
        door = GameObject.Find("door");
        rectTransformDoor = door.GetComponent<RectTransform>();
        doorInitialPosition = rectTransformDoor.anchoredPosition;

        //Permet d'afficher la fenêtre de bienvenue sur le jeu

        inventory = FindObjectOfType<Inventory>();
        movePlayer = FindObjectOfType<MovePlayer>();
        timer = GameObject.Find("Timer").GetComponent<TimerManager>();
        doorOpen = false;
        endGame = false;
        player = GameObject.Find("Player");

    }

    private void Update()
    {       
       
        float playerY = player.transform.position.y;

        //Si le joueur a trouvé toutes les maps et que la porte est encore fermée
        if (inventory.countMaps == 2 && !doorOpen)
        {
            OpenDoor();//On ouvre la porte
        }
        if (doorOpen && playerY<-4.5 && !endGame) //Si le joueur a passé le pas de la porte
        {
            EndGame(); //Il a gagné, on finit le jeu
        }

       
    }

    public void OpenDoor()
    {
        door = GameObject.Find("door");

        // Récupère le RectTransform de l'objet
        rectTransformDoor = door.GetComponent<RectTransform>();


        // Déplace le rectangle vers la droite
        rectTransformDoor.anchoredPosition += new Vector2(2, 0);
        doorOpen = true;
    }

    public void EndGame()
    {
        movePlayer.canMove = false;
        endGame=true;
        winAnimator.SetBool("isOpen", true);

        Button buttonReplay = GameObject.Find("Win").transform.Find("Button").GetComponent<Button>();
        buttonReplay.onClick.AddListener(Replay);

    }

    public void Replay()
    {

        winAnimator.SetBool("isOpen", false);
        doorOpen = false;
        endGame = false;
        rectTransformDoor.anchoredPosition = doorInitialPosition;
        timer = GameObject.Find("Timer").GetComponent<TimerManager>();
        timer.Replay();

    }



}
