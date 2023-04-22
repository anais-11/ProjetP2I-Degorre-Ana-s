using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Classe reliée à toutes les pop-up permettant de gérer leur ouverture et fermeture
public class PopUpManager : MonoBehaviour
{
    public Button CloseButton { get; set; }


    public void Awake()
    {        
        CenterPopUp();
    }



    //Ferme la pop-up en l'activant
    public void ClosePopUp()
    {
        Debug.Log("Fermetuuuure");
        
        gameObject.SetActive(false);
    }

    //Ferme la pop-up en la désactivant
    public void OpenPopUp()
    {
        //Il est nécessaire de resélectionner le bouton correct, car le gestionnaire d'événement garde en mémoire le dernier configuré
        CloseButton = gameObject.transform.Find("Button").GetComponent<Button>();
        CloseButton.onClick.RemoveAllListeners(); //On retire la mémoire du gestionnaire d'événenements
        CloseButton.onClick.AddListener(ClosePopUp); //On configure le clic que le bouton de la pop-up
        gameObject.SetActive(true);
    }

    //Centre la pop-up dans le canvas
    public void CenterPopUp()
    {
        // Récupére la référence du GameObject du canvas dans la hiérarchie
        GameObject canvas = GameObject.Find("Canvas");

        // Défini le canvas comme parent de l'objet instancié
        gameObject.transform.SetParent(canvas.transform, false);

        // Centre la Pop-up dans le canvas
        gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}

