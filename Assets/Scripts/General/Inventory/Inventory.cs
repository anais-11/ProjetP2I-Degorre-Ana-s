using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public TextMeshProUGUI nbClues; //Correspond au texte du nombre d'indices trouvés à afficher
    public int countClues=0; //Nombre d'indices trouvés
    public TextMeshProUGUI nbMaps;//Correspond au texte du nombre de map trouvées à afficher
    public int countMaps=0; //Nombre de maps trouvés
    public Inventory instance;
    public List<ColorClue> clueColorList = new List<ColorClue>(); //La liste des indices à trouver pour le jeu ColorGame
    public bool mapGameColor = false;
    public bool mapStroopGame3D = false;

    public List<int> cluesFound = new List<int>(); //Stocke la liste des indices que le joueur a trouvé


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("Il y a plus d'une instance de l'inventaire dans la scène");
            Destroy(gameObject);

            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject); //Permet de garder l'inventaire lorsqu'on change de scène

    }

    //Met à jour le nombre de carte
    public void UpdateNbMap()
    {
        nbMaps = GameObject.Find("nbMaps").GetComponent<TextMeshProUGUI>();
        nbMaps.text = countMaps.ToString();
    }

    //Met à jour le nombre d'indice
    public void UpdateNbClue()
    {
        nbClues = GameObject.Find("nbClues").GetComponent<TextMeshProUGUI>();
        nbClues.text = countClues.ToString();
    }

    //Met tout à jour
    public void UpdateAll()
    {
        UpdateNbMap();
        UpdateNbClue();
    }

    //Ajoute un indice
    public void AddClue()
    {
        countClues += 1;
        UpdateNbClue();      
               
    }

    //Ajoute une carte
    public void AddMap()
    {
        countMaps += 1;
        UpdateNbMap();        
    }   


    //Permet d'instancier les indices des cubes colorés (ceux trouvés par l'utilisateur) et ceux qu'il peut retrouver dans son inventaire
    public void InstanciateCluesOrder(string version)
    {
        for (int id = 1; id <= 4; id++)
        {
            Image Cube = GameObject.Find("Cube" + id.ToString() + version).GetComponent<Image>();
            TextMeshProUGUI TextCube = GameObject.Find("TextCube" + id.ToString() + version).GetComponent<TextMeshProUGUI>();
            Cube.color = InventoryManager.inventory.clueColorList[id - 1].Color;
            TextCube.text = id.ToString();
        }
    }

    //Permet d'instancier la position d'un cube indice
    public void InstanciateClueCubesPosition(int index, string version)
    {
        //On récupère le cube indice à modifier
        Image cube = GameObject.Find("cubeClue"+version).GetComponent<Image>();

        //On récupère le texte du cube indice à modifier
        TextMeshProUGUI clueText = GameObject.Find("textClue"+version).GetComponent<TextMeshProUGUI>();

        //On accède à la couleur du cube et on modifie sa couleur avec celle du cube référent
        cube.color = InventoryManager.inventory.clueColorList[index].Color;


        //On modifie le texte du cube indice avec la position du cube référent
        //On fait appel à InventoryManager qui permet de d'accéder à l'instance unique de l'inventaire 
        clueText.text = InventoryManager.inventory.clueColorList[index].RowAndCol;
    } 

    //Est appelé pour rénitialiser le jeu
    public void Replay()
    {
        countClues = 0;
        countMaps = 0;
        UpdateNbClue();
        UpdateNbMap();
        clueColorList.Clear();
        cluesFound.Clear();
        mapGameColor = false;
        mapStroopGame3D = false;
    }

}
