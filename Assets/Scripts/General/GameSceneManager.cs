
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    public GameObject WelcomePopUp;
    public bool sceneFirstTime=true;
    public Button btnCluesInventory;
    public CarousselClues carousselClues;
    public static GameSceneManager instance;
    public TimerManager timer;
    public InstanciateColorGame colorGame;
    public StroopGameManager stroopGame;
    private Scene currentScene;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();//On récupère la scène actuelle
        if (currentScene.name == "PrincipalRoom")
        {
            timer = GameObject.Find("Timer").GetComponent<TimerManager>();
            Animator WelcomeAnimator = GameObject.Find("WelcomeRules").GetComponent<Animator>();
            WelcomeAnimator.SetBool("isOpen", true);
            Debug.Log("animator ouvert");

            GameObject ObjectButton = GameObject.Find("WelcomeRules");
            Button buttonStart = ObjectButton.transform.Find("Button").GetComponent<Button>();
            buttonStart.onClick.AddListener(Launch);
        }
        //Lorsqu'une scène est chargée on appelle la méthode OnScenLoaded, la syntaxe permet d'ajouter la méthode à la liste de celle a effectuée lorsqu'une scène est chargée
        SceneManager.sceneLoaded += OnSceneLoaded;

        FindObjects();
    }



    public void Launch()
    {
        timer.LaunchTimer();
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = SceneManager.GetActiveScene();//On récupère la scène actuelle

        if (scene.name == "PrincipalRoom" && timer.timeRemaining == 600)
        {
            Start(); //Permet de rénitialiser le jeu
        }


        //On actualise le nombre de maps et de clues ramassés à chaque chargement d'une scène
        InventoryManager.inventory.UpdateAll();      

        

        FindObjects();//Récupère les objets nécessaires au fonctionnement des scènes

    }

    
    public void FindObjects()
    {
        //On récupère le caroussel d'indice        
        carousselClues = GameObject.Find("Inventory").GetComponent<CarousselClues>();
        carousselClues.Initiate();

        //Permet d'ouvrir le caroussel des indices trouvés
        btnCluesInventory = GameObject.Find("ButtonClue").GetComponent<Button>();
        btnCluesInventory.onClick.AddListener(OpenCarousselClues);
        Debug.Log("Object found");

        
        timer = GameObject.Find("Timer").GetComponent<TimerManager>();
        timer.Start();

    }

    //Permet d'ouvrir  
    public void OpenCarousselClues()
    {
        Debug.Log("Je veux ouvrir le caroussel");
        carousselClues.OpenCaroussel();
    }

    public void Replay()
    {
        currentScene = SceneManager.GetActiveScene();//On récupère la scène actuelle
        //On rénitialise l'inventaire
        InventoryManager.inventory.Replay();
       
        if (currentScene.name == "Stroop3D")
        {
            ResetStroopGame();
            SceneManager.LoadScene("PrincipalRoom");
            
        }
        else if(currentScene.name == "ColorGame")
        {
            SceneManager.LoadScene("PrincipalRoom");
            
        }
        
            ResetColorGameAndRoom();
           
        
        MovePlayer player = GameObject.Find("Player").GetComponent<MovePlayer>();
        player.Replay();
        Start();
  
    }

    public void ResetStroopGame()
    {
        stroopGame= GameObject.Find("StroopGameManager").GetComponent<StroopGameManager>(); //On récupère le script qui instancie le jeu Stroop3D
        stroopGame.Replay();
    }

    public void ResetColorGameAndRoom()
    {
        colorGame = GameObject.Find("GameColor").GetComponent<InstanciateColorGame>(); //On récupère le script qui instancie le jeu des couleurs
        colorGame.ResetGame();
    }

}
