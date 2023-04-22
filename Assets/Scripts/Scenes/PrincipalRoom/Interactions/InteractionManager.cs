using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    public string reference = "Object"; //Permet d'initialiser le nom de l'objet actuel
    public int rightChoice; //Stocke l'action qui est correcte
   
    public GameObject PopUpInteraction; //Contient le prefab de la pop-up de l'interaction avec un objet
    public GameObject PopUpWrongAction; //Contient le prefab de la pop-up lorsque le joueur fait une mauvaise action



    private void Start()
    {
        //On crée la pop-up correpondant avec l'interaction entre le joueur et un objet et celle de lorsqu'il fait une mauvaise action
        PopUpInteraction = Instantiate(PopUpInteraction);
        PopUpWrongAction = Instantiate(PopUpWrongAction);

        //On  assigne le script commun à toutes les pop-up permettant de les ouvrir, fermer et centrer sur le canvas
        PopUpInteraction.AddComponent<PopUpManager>();
        PopUpWrongAction.AddComponent<PopUpManager>();


        //On lui assigne le script correspondant à la pop-up d'une interaction avec un objet
        PopUpInteraction.AddComponent< FurnitureInteractionPopUp>();
        
       

        //On ferme les pop up -->il n'y a pas encore eu d'interactions
        PopUpInteraction.GetComponent<PopUpManager>().ClosePopUp();
        PopUpWrongAction.GetComponent<PopUpManager>().ClosePopUp();

      
    }


    //Méthode qui récupère les informations de l'interaction avec un objet
    public void StartInteraction(Interaction interaction)
    {
        //On ouvre la popup d'interaction
        PopUpInteraction.GetComponent<PopUpManager>().OpenPopUp();

        //On instancie les valeurs de FurnitureInteraction
        PopUpInteraction.GetComponent< FurnitureInteractionPopUp> ().NameObject.text= interaction.NameUsed;
        reference= interaction.NameUsed;//Permet de stocker pour accéder facilement à l'objet avec lequel le joueur interagit
        PopUpInteraction.GetComponent<FurnitureInteractionPopUp>().Action1.text = interaction.Actions[0];
        PopUpInteraction.GetComponent<FurnitureInteractionPopUp>().Action2.text = interaction.Actions[1];

        //On récupère les valeurs correctes
        rightChoice = interaction.RightChoice;
        
        
    }


    

    //On vérifie si le joueur sélectionne une touche du clavier
    void Update()
    {
        //Condition pour effectuer les actions seulement lorsque le joueur est en train de faire une interaction avec un objet
        if (reference!= "Object")
        {

            //Si le joueur choisi une action (1 ou 2)
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.B))
            {
                //Fermeture de la fenêtre d'interaction
                PopUpInteraction.GetComponent<PopUpManager>().ClosePopUp();
                //Conversion de la lettre choisie en chiffre pour faciliter la suite
                int numberChoice;

                //On associe la touche tapée à une valeur
                if (Input.GetKeyDown(KeyCode.A))
                {
                    numberChoice = 1;
                }
                else
                {
                    numberChoice = 2;
                }

                //Si l'action choisie par le joueur n'amène à rien 
                if (numberChoice != rightChoice)
                {
                    //Affiche un panel indiquant que le choix effectué n'ammène à rien
                    PopUpWrongAction.GetComponent<PopUpManager>().OpenPopUp();
                }
                //Si l'action choisie par le joueur est une bonne action, 
                else
                {
                    ActionManager.instanceAction.SetObjectAction(reference);

                }
                reference= "Object"; //On rétabli une valeur de référence indiquant qu'il n'y a plus d'interaction
            }
        }
    } 


}