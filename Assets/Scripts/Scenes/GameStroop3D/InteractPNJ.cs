using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class InteractPNJ : MonoBehaviour
{
    public string Presentation { get; private set; }
    public Queue<string> rules= new Queue<string>();  //Permet de gérer la file d'attente pour afficher les règles du jeu
    public string Success { get; private set; }
    public string Fail { get; private set; }
    public bool isInCollision;

    public StroopGameManager GameManager { get; private set; }

    public InventoryStroopGame Inventory { get; private set; }
    public TextMeshProUGUI TextDisplayed { get; private set; } //Correspond au texte que l'on souhaite afficher
    public Animator InteractionPnjAnimator { get; private set; } //Permet d'afficher la fenêtre d'interaction
    public GameObject InteractionPnj { get; private set; }

    public Animator InteractionAnimator { get; private set; } //Permet d'afficher un encadré lorsque le joueur peut intéragir avec le PNJ
    public GameObject Interaction { get; private set; }

    public Animator GameRulesAnimator { get; private set; }
    public GameObject GameRules { get; private set; }

    public Animator DiamsGivenAnimator { get; private set; }
    public GameObject DiamsGiven { get; private set; }



    void Start()
    {        
        SetProp();
    }

    //Est appelé à chaque changement de fame
    private void Update()
    {
        //On appelle la méthode d'interaction lorsque le joueur est en contact avec le PNJ et qu'il appuie sur la touche E
        if (isInCollision && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    //Lorsque le joueur est en collision avec le PNJ
    private void OnCollisionEnter(Collision infoCollision)
    {
        isInCollision = true;
        InteractionAnimator.SetBool("isOpen", true);
    }

    //Lorsque le joueur n'est plus en collision avec le PNJ
    private void OnCollisionExit(Collision infoCollision)
    {
        isInCollision = false;
        InteractionAnimator.SetBool("isOpen", false);
    }

    //Récupère tous les objets de la scènes pour instancier les propriétés
    public void SetProp()
    {
        Presentation = "Bienvenue sur le jeu STROOPGAME, votre ami avait beaucoup pris l'habitude de venir jouer à ce jeu. Il m'a même confié un secret, que je ne pourrai vous réléler seulement si vous réussissez ce jeu.";
       
        Success = "Félicitation, vous avez réussi à ramasser tous les diamants ! Comme promis, je vous donne un indice vous permettant de retrouver votre ami";
        Fail = "Vous n'avez pas assez de diamants pour que je vous donne un indice, revenez vers moi lorsque vous les diamants nécessaires";

        //Permet de récupérer l'objet qui contiendra le texte que l'on souhaite écrire
        TextDisplayed = GameObject.Find("presentation").GetComponent<TextMeshProUGUI>();

        //Permet de récupérer le fenêtre d'interaction avec le PNJ et son animator
        InteractionPnj= GameObject.Find("InteractPnj");
        InteractionPnjAnimator = InteractionPnj.GetComponent<Animator>();

        //Permet de récupérer la fenêtre d'interaction avec le PNJ et son animator
        Interaction = GameObject.Find("Interact");
        InteractionAnimator = Interaction.GetComponent<Animator>();

        //Permet de récupérer la fenêtre des règles du jeu et son animator
        GameRules = GameObject.Find("GameRules");
        GameRulesAnimator = GameRules.GetComponent<Animator>();

        //Permet de récupérer la fenêtre de lorsque l'utilisateur veut donner des diamants au PNJ et son animator
        DiamsGiven = GameObject.Find("GiveDiams");
        DiamsGivenAnimator = DiamsGiven.GetComponent<Animator>();

        //On récupère l'instance de l'inventaire
        Inventory = FindObjectOfType<InventoryStroopGame>();

        //On récupère le manager du jeu
        GameManager = FindObjectOfType<StroopGameManager>();
    }

    //Affiche la fenêtre d'interaction avec le PNJ
    public void Interact()
    {
        InteractionAnimator.SetBool("isOpen", false); //Ferme la fenêtre d'information d'interaction
        InteractionPnjAnimator.SetBool("isOpen", true); //Ouvre la fenêtre de discussion avec le PNJ
        TextDisplayed.text = Presentation;

    }

    //Affiche la fenêtre des règles du jeu, et les instancier
    public void GiveGamesRules()
    {
        InstanciateGameRules();
        InteractionPnjAnimator.SetBool("isOpen", false);
        GameRulesAnimator.SetBool("isOpen", true);
        TextDisplayed = GameObject.Find("rulesText").GetComponent<TextMeshProUGUI>();
        TextDisplayed.text = rules.Dequeue(); //Permet d'afficher la prochaine phrase du dialogue

    }

    //Instancie les règles du jeu dans une file d'attente
    public void InstanciateGameRules()
    {
        rules.Clear(); //Permet d'être sûr qu'il ne reste plus rien dans la file d'attente

        //Permet d'ajouter les règles du jeu
        rules.Enqueue("Vous devez ramasser des diamants en fonction du texte affiché au dessus de vous");
        rules.Enqueue("Si le texte est : 'vert' et écrit de couleur bleu, vous devez alors ramasser un diamant de la couleur du texte écrit, ici un diamant vert");
        rules.Enqueue("Le but de ce jeu est de vous confronter à l'effet Stroop, à chaque erreur vous perdrez un point de vie");
        rules.Enqueue("Une fois tous les diamants ramassés revenez me voir pour que je vous donne un indice sur la localisation de votre ami");
    }

    //Gère l'affichage des règles du jeu de la file d'attente
    public void DisplayGameRules()
    {
        //Si le joueur a lu toutes les règles du jeu, la fenêtre se femre lorsqu'il appuie sur "J'ai compris !"
        if (rules.Count == 0)
        {
            GameRulesAnimator.SetBool("isOpen", false);
            
            return; //On n'exécute pas la suite du code
        }

        TextDisplayed.text = rules.Dequeue(); //Permet d'afficher la prochaine phrase du dialogue

        //Permet de changer le texte du bouton lorsque la dernière phrase est affichée
        if (rules.Count == 0)
        {
            TextMeshProUGUI button = GameObject.Find("next").GetComponent<TextMeshProUGUI>();
            button.text = "J'ai compris !";
        }
    }

    public void GiveDiams()
    {

        InteractionPnjAnimator.SetBool("isOpen", false); //On ferme la fenêtre d'interaction principale

        DiamsGivenAnimator.SetBool("isOpen", true); //On ouvre la fenêtre d'interaction avec les diamants
        TextMeshProUGUI title = GameObject.Find("titleDiamsGiven").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI result = GameObject.Find("result").GetComponent<TextMeshProUGUI>();

        //Renvoie "true" si le joueur a rammasé tous les diamants nécessaires
        if (Inventory.VerifyDiamsCount())
        {
            title.text = "Merci !";
            result.text = "Bravo ! Tu as rammassé les " + Inventory.DiamsTotal.ToString() + " diamants que je voulais. Je te donne alors un indice sur ton ami pour te récompenser !";
            GameManager.gameFinished = true;
            InventoryManager.inventory.AddMap(); //On ajoute une map à l'inventaire du jeu général
            InventoryManager.inventory.mapStroopGame3D = true; //On indique que la map pour se jeu a été trouvée
        }
        else
        {
            title.text = "Désolé !";
            result.text = "Je ne peux pas te donner mon indice, tu as " + Inventory.DiamsCount.ToString() + " diamants alors qu'il t'en faut " + Inventory.DiamsTotal.ToString() + ". Continue à chercher et reviens me voir !";
            
        }
    }

    //Appelé lorsque le joueur clique sur "OK" sur la fenêtre d'interaction
    public void CloseGiveDiams()
    {
        DiamsGivenAnimator.SetBool("isOpen", false);

        //Si le joueur a fini le jeu Stroop
        if (GameManager.gameFinished)
        {
            GameManager.FinishGame();
        }
    }

    

   
    
   




}
