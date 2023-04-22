using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
    public string nameObject; //Récupère le nom de l'objet
    public Animator colorClueAnimator; //Récupère l'animator de l'indice de l'odre des clics sur les cubes colorés
    public Animator uniqueColorClue; //Récupère l'animator qui affiche l'indice de chaque cube coloré
    public Animator alreadyPlayedAnimator;
    public bool isOpen = false; //Indique si la pop-up est ouverte
    public static ActionManager instanceAction; //Permet de récupérer l'instance actuelle de l'interaction qui est en cours

    //Permet d'indiquer si l'indice a déjà été trouvé, pour ne pas le rajouter à l'inventaire
    public bool armoireClueFind = false;
    public bool plantClueFind = false;
    public bool bedClueFind = false;
    public bool lampClueFind = false;
    public bool tableClueFind = false;

    private void Awake()
    {
        if (instanceAction != null)
        {
            Debug.LogWarning("Il y a plus d'une instance d'actionManager dans la scène ");
            return;
        }
        instanceAction = this;
    }


    //Permet de gérer l'objet avec lequel on fait l'action
    public void SetObjectAction(string name)
    {
        nameObject = name.ToLower();
        ObjectAction(instanceAction);

    }

    //Appelle différentes méthodes en fonction de l'objet avec lequel est effectuée l'action 
    public void ObjectAction(ActionManager instance)
    {
        string name = instance.nameObject;
        switch (name)
        {
            case "bibliothèque":
                ArmoireActions();
                break;
            case "canapé":
                SofaRightActions();
                break;
            case "plante" or "lit" or "table" or "lampe":
                ShowClueColor(name);
                break;
            case "arcade":
                Debug.Log("arcade 1");
                ArcadeAction();
                break;


        }

    }

    //Permet de lancer le jeu StroopGame
    public void ArcadeAction()
    {
        //On lance le jeu seulement si la map du jeu n'a pas encore été trouvée
        if (!InventoryManager.inventory.mapStroopGame3D)
        {
            SceneManager.LoadScene("Stroop3D");
        }
        else
        {
            alreadyPlayedAnimator.SetBool("isOpen", true);
        }
    }

    //Permet d'afficher l'indice de l'odre de clics des cubes du jeu ColorGame
    public void ArmoireActions()
    {

       
        
        InventoryManager.inventory.InstanciateCluesOrder( ".1"); //On instancie le visuel de l'indice permettant de savoir l'ordre des clics sur les cubes
        

        if (!isOpen)
        {
            colorClueAnimator.SetBool("clueIsOpen", true);

            if (!armoireClueFind)
            {
                InventoryManager.inventory.AddClue();
                InventoryManager.inventory.cluesFound.Add(4); //On ajoute l'id de l'indice trouvé à l'inventaire
                armoireClueFind = true;
            }

            isOpen = true;
        }
        else
        {
            colorClueAnimator.SetBool("clueIsOpen", false);
            isOpen = false;
        }
    }

    //Permet de lancer le jeu ColorGame
    public void SofaRightActions()
    {
        //On lance le jeu seulement si la map du jeu n'a pas encore été trouvée
        if (!InventoryManager.inventory.mapGameColor)
        {
            SceneManager.LoadScene("ColorGame");
        }
        else
        {
            alreadyPlayedAnimator.SetBool("isOpen", true);
        }
       

    }

    //Permet de définir quel indice de cube il faut afficher en fonction de l'objet
    public void ShowClueColor(string name)
    {
        int index = 0;
        bool clueFind = false;

        switch (name)
        {
            case "plante":

                clueFind = plantClueFind;

                index = 0;
                if (!plantClueFind)
                {
                    plantClueFind = true;
                    InventoryManager.inventory.cluesFound.Add(0); //On ajoute l'id de l'indice trouvé à l'inventaire
                   
                }
               
                break;

            case "lit":

                clueFind = bedClueFind;

                index = 1;
                if (!bedClueFind)
                {
                    bedClueFind = true;
                    InventoryManager.inventory.cluesFound.Add(1);
                    
                }
               
                break;

            case "lampe":

                clueFind = lampClueFind;
                index = 2;
                if (!lampClueFind)
                {
                    
                    lampClueFind = true;
                    InventoryManager.inventory.cluesFound.Add(2);

                }
                break;

            case "table":
                clueFind = tableClueFind;
                index = 3;
                if (!tableClueFind)
                {
                                                       
                    tableClueFind = true;
                    InventoryManager.inventory.cluesFound.Add(3);
                  
                }
               
                break;

               
        }
        InventoryManager.inventory.InstanciateClueCubesPosition(index, ".1");//On instancie l'indice trouvé


        if (!isOpen)
        {
            //On oure l'indice d'un cube coloré
            uniqueColorClue.SetBool("isOpen", true);

            if (!clueFind)
            {
                InventoryManager.inventory.AddClue(); //On ajoute l'indice à l'inventaire
            }

            isOpen = true;
        }
        else
        {
            //On ferme l'indice d'un cube coloré
            uniqueColorClue.SetBool("isOpen", false);
            isOpen = false;
        }
    }

    public void CloseAlreadyPlayed()
    {
        alreadyPlayedAnimator.SetBool("isOpen", false);
    }

}