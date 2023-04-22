using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameColorManager : MonoBehaviour
{
    private List<int> playerCombination = new List<int>(); //Récupère les entiers du cubes cliqués par le joueur
    public Animator welcomeAnimator; //récupère l'animator de la fenêtre de bienvenue
    public Animator failAnimator; //récupère l'animator de la pop-up d'échec
    public Animator successAnimator; //récupère l'animator de la pop-up de succes
    public bool clicsActiveCubes = false; //Indique si le joueur peut cliquer sur les cubes
    public bool clicsAllowed=true; //Indique si le joueur peut cliquer de façon générale
    public InstanciateColorGame instanciateColorGame; //Permet de récupérer la grille de jeu
    public CarousselClues carousselClues; //récupère le carroussel des indices trouvés


    //Appelé lors de la première frame pour présenter le jeu
    public void Start()
    {
        //On récupère l'objet qui contient la grille
        GameObject grille = GameObject.Find("GameColor");
        instanciateColorGame = grille.GetComponent<InstanciateColorGame>();
        instanciateColorGame.ShowGrille();

        //On récupère le composant caroussel de l'objet Inventory
        GameObject inventory = GameObject.Find("Inventory");
        carousselClues = inventory.GetComponent<CarousselClues>();
        

        //On ouvre la fenêtre de présentation du jeu
        welcomeAnimator.SetBool("WelcomeIsOpen", true);
        clicsAllowed = false;
    }

    private void Update()
    {
        //On vérifie si le joueur peut cliquer sur les cubes de la grille
        if (carousselClues.ckeckClickGameColor&&clicsAllowed)
        {
            clicsActiveCubes = true;
        }
        else
        {
            clicsActiveCubes = false;
        }
    }

   

    //Permet de réinitialiser le jeu du joueur pour recommencer
    public void Replay()
    {
        //On ferme la fenêtre d'échec
        failAnimator.SetBool("FailIsOpen", false);

        //Permet de rénitialiser les cubes sélectionnés par le joueur
        playerCombination = new List<int>();

        ReloadColors();

        //On rend les clics sur les cubes possible
        clicsAllowed = true;

    }




    //Permet de fermer la fenêtre présentation du jeu
    public void CloseWelcome()
    {
        welcomeAnimator.SetBool("WelcomeIsOpen", false);
        clicsAllowed = true;
    }




    //Récupère les clics sur les cubes du joueur
    public void SetPlayerCombinaison(int id)
    {
        Debug.Log(id);
        playerCombination.Add(id);

        //Une fois sa combinaison complétée
        if (playerCombination.Count == 4)
        {
            clicsAllowed = false;
            VerifyCombinaison();
        }
    }


    //Vérifie si la combinaison du joueur est la combinaison correcte
    public void VerifyCombinaison()
    {
        int mistakes = 0;
        for (int i = 0; i < instanciateColorGame.combination.Count; i++)
        {
            if (instanciateColorGame.combination[i] != playerCombination[i])
            {
                mistakes++;
            }
        }

        //Ouvre une fenêtre d'échec
        if (mistakes != 0)
        {
            failAnimator.SetBool("FailIsOpen", true);

        }
        //Ouvre une fenêtre de succès
        else
        {
            InventoryManager.inventory.AddMap(); //On ajoute une map à l'inventaire
            InventoryManager.inventory.mapGameColor = true; //On indique que la map pour se jeu a été trouvée
            successAnimator.SetBool("SuccessIsOpen", true);
        }
    }

    public void ReloadColors()
    {
        //Permet de rénitialiser les couleur d'origine des cubes (avant qu'ils soient cliqués)
        for (int i = 0; i < instanciateColorGame.cubes.GetLength(0); i++)
        {
            for (int j = 0; j < instanciateColorGame.cubes.GetLength(1); j++)
            {
                instanciateColorGame.cubes[i, j].GetComponent<Renderer>().material.color = instanciateColorGame.cubes[i, j].GetComponent<CubeScript>().originalColor;

            }
        }
    }

    //Permet de revenir vers la pièce principale
    public void ReturnToRoom()
    {
        ReloadColors();
        instanciateColorGame.HideGrille();
        SceneManager.LoadScene("PrincipalRoom");

    }





}