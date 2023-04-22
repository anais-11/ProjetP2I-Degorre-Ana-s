using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public int id; //stocke d'id du cube
    private GameColorManager gameColorManager; //stocke la couleur du cube affichée
    public Color originalColor; //stocke la couleur du cube avant transformation


   
    private void Start()
    {
        // Récupère l'instance de GameColorManager dans la scène
        gameColorManager = FindObjectOfType<GameColorManager>();
        originalColor = GetComponent<Renderer>().material.color;


    }

    //Lorsque le joueur clique sur le cube
    void OnMouseDown()
    {
         gameColorManager = FindObjectOfType<GameColorManager>();
        if (gameColorManager.clicsActiveCubes == true)
        {
            // Récupération du Renderer du cube
            Renderer cubeRenderer = this.GetComponent<Renderer>();


            // Récupération de la couleur actuelle du cube
            Color currentColor = originalColor;

            // Réduction de la valeur alpha à 0.5
            currentColor.a = 0.5f;

            // Mise à jour de la couleur du matériau du cube
            cubeRenderer.material.color = currentColor;

            SendIdCombinason();
        }
      
    }

    //Appelle la fonction pour instancier la combinaison du joueur
    public void SendIdCombinason()
    {
        gameColorManager.SetPlayerCombinaison(id);
    }
}
