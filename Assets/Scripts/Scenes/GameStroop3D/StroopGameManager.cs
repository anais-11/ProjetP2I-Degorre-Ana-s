using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class StroopGameManager : MonoBehaviour
{
    public InitiateStroopGame initiateStroopGame;
    public InventoryStroopGame inventory;
    public int indexPlayer;
    public TextMeshProUGUI TextDisplayed;
    public HealthState healthState;
    public Animator GameOverAnimator;
    public Animator animatorWelcome;
    public Animator animatorTextColor;
    public Animator FinishGameAnimator;
    public Animator QuitGame;
    public bool gameFinished;
    public bool quitIsClosed;





    // Use this for initialization
    void Start()
    {       
        InitiateManager();
    }

    public void DisplayWelcome()
    {
        //Affiche la fenêtre de bienvenue du mini-jeu
        animatorWelcome.SetBool("isOpen", true);
    }

    //Permet de fermer la fenêtre de bienvenue
    public void CloseWelcome()
    {
        animatorWelcome.SetBool("isOpen", false);
        animatorTextColor.SetBool("isOpen", true);
    }

    public void InitiateManager()
    {
        gameFinished = false;
        quitIsClosed = true;
        animatorWelcome.SetBool("isOpen", true);
        inventory = FindObjectOfType<InventoryStroopGame>();
        indexPlayer = 0;
        initiateStroopGame = FindObjectOfType<InitiateStroopGame>();
        healthState = FindObjectOfType<HealthState>();
        TextDisplayed = GameObject.Find("ColorName").GetComponent<TextMeshProUGUI>();
        DisplayColorText();
    }

    public void DisplayColorText()
    {
        Debug.Log("ici");
        //On affiche le texte correspondant au nom de la couleur à trouver
        
        TextDisplayed.text = inventory.listText[indexPlayer].text;
        //On modifie la couleur du texte
        TextDisplayed.color = inventory.listText[indexPlayer].color;
    }

    //Vérifie si l'utilisateur a ramassé le bon diamant
    public void VerifyOrderDiams(string colorDiams)
    {
        //On récupère le diamant qu'il doit ramasser
        TextColor textReference = inventory.listText[indexPlayer];

        //Si le diamant choisi est celui qu'il faut ramasser,on passe au texte suivant
        if (colorDiams == textReference.text)
        {
            Debug.Log("OK");
            indexPlayer++;
            inventory.DiamsCount++;

            //Si l'utilisateur a trouvé tous les diamants, il a fini
            if (inventory.VerifyDiamsCount())
            {                
                animatorTextColor.SetBool("isOpen", false);
            }
            else
            {
                DisplayColorText();
            }
            
        }
        //Sinon on enlève un point de vie au joueur
        else
        {
            Debug.Log("Wrong !");
            healthState.WrongDiamants();

            //On vérifie si GameOver 
            if (healthState.canMove == false)
            {
                GameOverAnimator.SetBool("isOpen", true);
                
            }

        }

    }

    //Permet de recommencer une partie
    public void Replay()
    {
        GameOverAnimator.SetBool("isOpen", false);
        healthState.RelivePlayer();
        initiateStroopGame.Replay();
        InitiateManager();
    }

    public void FinishGame()
    {
        healthState.canMove = false;
        FinishGameAnimator.SetBool("isOpen", true);
    }

    //Permet de retourner vers la salle principale
    public void BackToRoom()
    {
        SceneManager.LoadScene("PrincipalRoom");
    }

    public void WantToQuit()
    {       

        if (quitIsClosed)
        {
            QuitGame.SetBool("isOpen", true);
            Debug.Log("Ouvert");
            quitIsClosed = false;
        }

        else
        {
            QuitGame.SetBool("isOpen", false);
            quitIsClosed = true;
        }
        
    }

    
   





}
