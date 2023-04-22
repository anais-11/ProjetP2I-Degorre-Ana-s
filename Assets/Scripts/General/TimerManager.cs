using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    public float timeRemaining=600; // temps total en secondes
    public TextMeshProUGUI timeText; // texte pour afficher le temps restant
    public static TimerManager instance;
    public GameObject timesUpPopUp;
    public bool timeCanPass = false;
    public GameSceneManager gameSceneManager;
    public Animator timeIsOverAnimator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    public void Start()
    {
        timeIsOverAnimator = GameObject.Find("TimesUp").GetComponent<Animator>();
        timeText = GameObject.Find("TimeLeft").GetComponent<TextMeshProUGUI>();
    }

    public void LaunchTimer()
    {
        Animator WelcomeAnimator = GameObject.Find("WelcomeRules").GetComponent<Animator>();
        WelcomeAnimator.SetBool("isOpen", false);
        timeCanPass = true; //On commence à lancer le timer
    }


    // Update is called once per frame
    void Update()
    {
        if (timeRemaining <= 0 && timeCanPass)
        {
            timeRemaining = 0; //Le temps est écoulé
            timeCanPass = false;
            TimeIsOver();
        }
        else if(timeCanPass)
        {
            timeRemaining -= Time.deltaTime; //on soustraie le temps écoulé depuis la dernière frame            

        }
        UpdateTimer();
    }

    void UpdateTimer()
    {
        //On met à jour le texte qui affiche le temps restant
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void TimeIsOver()
    {
        Debug.Log("affiche la pop up");
        //On récupère et instancie la pop-up qui indique à l'utilisateur qu'il a perdu
        //Puis on définit le clic sur le bouton rejouer pour qu'il appelle la fonction replay
       
        timeIsOverAnimator.SetBool("isOpen", true);

        GameObject ObjectButton = GameObject.Find("TimesUp");
        Button buttonReplay = ObjectButton.transform.Find("Button").GetComponent<Button>();
        buttonReplay.onClick.AddListener(Replay);
        Debug.Log("bouton ok");


        Scene currentScene = SceneManager.GetActiveScene();//On récupère la scène actuelle
        if (currentScene.name == "PrincipalRoom")
        {
            MovePlayer playerRoom = GameObject.Find("Player").GetComponent<MovePlayer>();
            playerRoom.canMove = false;

        }
        else if(currentScene.name == "Stroop3D")
        {
            HealthState playerStroop = GameObject.Find("Player").GetComponent<HealthState>();
            playerStroop.canMove = false;
        }   
    }

    public void Replay()
    {
        timeRemaining = 600;
        timeIsOverAnimator.SetBool("isOpen", false);
        gameSceneManager = GameObject.Find("GameSceneManager").GetComponent<GameSceneManager>();
        gameSceneManager.Replay();
    }


}
