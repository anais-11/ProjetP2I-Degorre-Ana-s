using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class InitiateStroopGame : MonoBehaviour
{
    public InventoryStroopGame inventory;  
    public GameObject RedDiamPrefab;
    public GameObject BlueDiamPrefab;
    public GameObject GreenDiamPrefab;
    public GameObject YellowDiamPrefab;


    public List<Color> colorList;
    public List<TextColor> ListTexts = new List<TextColor>();
    public List<GameObject> ListDiams = new List<GameObject>();

    public Terrain terrain;




    // Start is called before the first frame update
    void Start()
    {
        InitiateColor();

        inventory = FindObjectOfType<InventoryStroopGame>();
        terrain = FindObjectOfType<Terrain>();

        

        //On crée les diamants sur la map en définissant le nombre de diamants de chaque couleur que le joueur doit trouver
        InitiateColorToFind();


        //On créer les différents objet text qui apparaîtront à l'écran
        InitiateTextColor();

       
    }

    //Permet de recommencer le jeu
    public void Replay()
    {
        ListTexts.Clear();
        inventory.ResetStroopInventory();
        inventory.InitiateNbDiams();

        //Permet de supprimer tous les diamants de la map
        foreach (GameObject diam in ListDiams)
        {
            Destroy(diam);
        }
        ListDiams.Clear();

        //Nouvelle création de diamants et de textes colorés
        InitiateColorToFind();
        InitiateTextColor();
    }

    public void InitiateColor()
    {
        Color colorRed = new Color32(232, 11, 11, 255);
        Color colorBlue = new Color32(11, 111, 232, 255);
        Color colorYellow = new Color32(241, 211, 25, 255);
        Color colorGreen = new Color32(46, 196, 16, 255);
        colorList = new List<Color>() { colorRed, colorBlue, colorYellow, colorGreen };

    }

    

    //Permet de retourner vers la salle principale
    public void ReturnToRoom()
    {
        SceneManager.LoadScene("PrincipalRoom");
    }

    public void InitiateColorToFind()
    {
        //On instancie combien de texte d'une couleur spécifique le joueur devra trouver
        //Ex : le joueur devra trouver entre 1 et 3 diamants de couleur bleu
        inventory.NbBlue = Random.Range(2, 4);
        //On crée le nombre de diamants nécessaires
        CreateDiams(inventory.NbBlue, BlueDiamPrefab,"bleu");

        inventory.NbYellow = Random.Range(2, 4);
        CreateDiams(inventory.NbYellow, YellowDiamPrefab, "jaune");

        inventory.NbRed = Random.Range(2, 4);
        CreateDiams(inventory.NbRed, RedDiamPrefab, "rouge");

        inventory.NbGreen = Random.Range(2, 4);
        CreateDiams(inventory.NbGreen, GreenDiamPrefab,"vert");



    }

    //Premet de créer un nombre de diamant en fonction d'un prefab
    public void CreateDiams(int nbDiams, GameObject prefabDiam, string color)
    {
        // Plage de coordonnées du terrain
        float xMin = 10;
        float xMax = 90;
        float zMin = 10;
        float zMax = 90;
        float yAboveTerrain = 3 / 2;

        //On ajoute une rotation car le prefab de base en possède une pour que le diamant soit vertical
        Quaternion rotation = Quaternion.Euler(-89.98f, 0f, 0f);

        //On crée le nombre de diamans bleus nécessaires + 3 en plus 
        for (int i = 0; i < nbDiams + 7; i++)
        {
            float x = Random.Range(xMin, xMax); // Coordonnée x aléatoire
            float z = Random.Range(zMin, zMax); // Coordonnée y aléatoire
            // Permet de récupérer la hauteur actuelle du terrain pour que le diamant ne soit pas enterré dans une coline
            float y = terrain.SampleHeight(new Vector3(x, 0, z)) + yAboveTerrain; 

            Vector3 position = new Vector3(x, y, z); // Création de la position aléatoire du diamant
            GameObject diams = Instantiate(prefabDiam, position, rotation); // Instantiation du préfab avec la position aléatoire

            //On ajoute le script diams au composant créé en définissant sa couleur
            diams.AddComponent<Diams>();
            diams.GetComponent<Diams>().color = color;

            ListDiams.Add(diams);

        }
    }

    //Initialise les textes affichés 
    public void InitiateTextColor()
    {
        int  id= 0;
        Debug.Log("Bleu : " + inventory.NbBlue);
        CreateText(inventory.NbBlue, "bleu",ref id) ;
        Debug.Log("Vert : " + inventory.NbGreen);
        CreateText(inventory.NbGreen, "vert", ref id);
        Debug.Log("Rouge : " + inventory.NbRed);
        CreateText(inventory.NbRed, "rouge", ref id);
        Debug.Log("Jaune : " + inventory.NbYellow);
        CreateText(inventory.NbYellow, "jaune", ref id);
        MixTextColor();

        //On instancie la liste dans l'inventaire
        inventory.listText = ListTexts;

        
    }

    //Crée les objets text
    public void CreateText(int nbTextColor, string text,  ref int id)
    {

        //On créé le nombre de textes nécessaires correspondant à une couleur 
        for (int i =0; i<nbTextColor; i++)
        {
            //On définit une couleur aléatoire pour le texte


            Color colorText = SelectColor(text);
            //On instancie un objet text
            TextColor textColored = new TextColor(id, colorText, text);
            ListTexts.Add(textColored);
            id++;

            Debug.Log("text :" +text + " Couleur : " + colorText.ToString());
        }
    }

    //Permet de choisir une couleur pour le mot, différente de sa signification, (ex : ne pas choisir du jaune pour le mot "jaune")
    public Color SelectColor(string color)
    {
        int except=0;

        switch (color)
        {
            case "bleu":
                except = 1;
                break;
            case "rouge":
                except = 0;
                break;
            case "jaune":
                except = 2;
                break;
            case "vert":
                except = 3;
                break;
        }

        int randomColor = Random.Range(0, 3);
        while (randomColor == except)
        {
            randomColor = Random.Range(0, 3);
        }

        Color chosenColor = colorList[randomColor];
        return chosenColor;
    }


    //Permet de mélanger aléatoirement l'odre des texts
    public void MixTextColor()
    {
        int n = ListTexts.Count;

        while (n > 1)
        {
            n--;
            int random = Random.Range(0, n + 1);
            TextColor value = ListTexts[random];
            
            ListTexts[random] = ListTexts[n];
            ListTexts[n] = value;
        }
    }
}

      


    

   
       
    

