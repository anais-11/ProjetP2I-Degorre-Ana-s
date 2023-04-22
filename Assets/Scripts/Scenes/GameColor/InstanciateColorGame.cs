using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciateColorGame : MonoBehaviour
{
    public GameObject[,] cubes; //Correspond à la grille de cubes colorés
    public int rows = 4; //Nombre de lignes de la grille
    public int cols = 4; //Nombre de colonne de la grille
    public GameObject cubePrefab; //Stocke le préfab d'un cube
    private int cubeId = 1; //Index pour la création des cubes

    public int combinationLength = 4; //Correspond à la longueur de la combinaison gagnante
    public List<int> combination = new List<int>(); //Stocke les id des cubes de la combinaison gagante

    public List<ColorClue> cluesCombination = new List<ColorClue>(); //Stocke les couleurs des cubes de la combinaison
    public List<Color> colors; //Stocke les couleurs utilisées pour instancier les cubes
    public static InstanciateColorGame instance; //Récupère l'instance de cette grille de jeu   

  

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("Il y a plus d'une instance de l'inventaire dans la scène");
            Destroy(gameObject);

            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {       
            //Création de la grille de cubes
            CreateCubesGrid();
            HideGrille();
            //Initialisation de la combinaison gagnante
            SetCombinaison();       
    }


    //Permet de créer la grille composée du cbes colorés
    public void CreateCubesGrid()
    {
        cubes = new GameObject[rows, cols];

        //Permet de calculer les positions du premier cube de la grille pour qu'il soit centré sur la fenêtre de la caméra
        float startX = -(cols / 2f) * 2f + 1f;
        float startY = (rows / 2f) * 2f - 1f;

        //Crée les cubes dans la grille
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Vector2 position = new Vector2(startX + j * 2f, startY - i * 2f);
                GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
                cubes[i, j] = cube;

                //Associe un id au cube créé
                cube.AddComponent<CubeScript>();
                cube.GetComponent<CubeScript>().id = cubeId;
                cubeId++;
                DontDestroyOnLoad(cubes[i, j]);
            }
        }
        CreateColors();
        //Il est nécessaire d'instancier les couleurs après que tous les cubes aient été créés pour ne pas avoir d'erreur de compilation avec
        //ConfigCubesColor qui parcours la liste de tous les cubes
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {

                ConfigCubesColors(ref colors, cubes[i, j]);
            }
        }
    }

    public void SetCombinaison()
    {

        List<Color> selectedColors = new List<Color>();

        for (int i = 0; i < 4; i++)
        {
            int randomId;
            Color randomColor;

            // Choix aléatoire d'un cube non déjà sélectionné
            do
            {
                randomId = Random.Range(1, rows * cols);
            } while (combination.Contains(randomId));


            //Permet de récupérer la ligne en fonction de l'id
            int row = (randomId - 1) / cols;
            //Permet de récupérer la colonne en fonction de l'id en utilisant le modulo
            int col = (randomId - 1) % cols;

            // Récupération de la couleur du cube sélectionné
            CubeScript cubeScript = cubes[row, col].GetComponent<CubeScript>();
            randomColor = cubeScript.originalColor;

            // Vérification que la couleur n'a pas déjà été sélectionnée dans la combinaison
            if (selectedColors.Contains(randomColor))
            {
                //Permet de réitérer la boucle le i actuel de la boucle for
                i--;
                //Continue permet de sauter les lignes de codes suivantes et passe à la prochaine boucle (retourne à la boucle for) 
                continue;
            }

            // Ajout de l'id du cube sélectionné à la liste des cubes de la combinaison
            combination.Add(randomId);
            //Ajout de la couleur du cube à la liste des couleurs présent dans la combinaison
            selectedColors.Add(randomColor);

            // Création de l'indice en fonction de son numéro de ligne et de colone dans la grille (ex : L1, C2 -> Première ligne et deuxième colonne)
            string rowNumber = "L" + (row + 1).ToString();
            string colNumber = "C" + (col + 1).ToString();
            string rowAndCol = rowNumber + " , " + colNumber;
            ColorClue clue = new ColorClue(randomId, randomColor, rowAndCol);
            cluesCombination.Add(clue);
        }

        //On ajoute la lite des indices créés à l'inventaire du jeu 
        InventoryManager.inventory.clueColorList = cluesCombination;
    }

    //Permet de définir les couleurs utilisées pour les carrés colorés
    public void CreateColors()
    {
        //Orange
        Color c1 = new Color32(251, 128, 101, 255);
        //Bleu
        Color c2 = new Color32(101, 158, 251, 255);
        //Rose
        Color c3 = new Color32(224, 159, 237, 255);
        //Bleu foncé
        Color c4 = new Color32(29, 38, 175, 255);

        colors = new List<Color> { c1, c2, c3, c4 };

    }
    //Permet d'ajouter une couleur à chaque cube. A savoir : 4cubes max ayant la même couleur.
    public void ConfigCubesColors(ref List<Color> colorToUse, GameObject cube)
    {
        //Choisi une couleur aléatoire parmis les couleurs disponibles
        int randomColor = Random.Range(0, colorToUse.Count);
        Color chosenColor = colorToUse[randomColor];
        int nbChosenColor = 0;

        //Vérifie si le nombre de fois que la couleur aléatoire choisie est utilisée dans la grille
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (cubes[i, j].GetComponent<Renderer>().material.color == chosenColor)
                {
                    nbChosenColor++;
                }
            }
        }

        //Vérifie la couleur est déjà utilisée au maximum (4 fois)
        if (nbChosenColor == 4)
        {
            //Enlève la couleur déjà utilisée de la liste des couleurs
            colorToUse.Remove(chosenColor);
            ConfigCubesColors(ref colorToUse, cube);
        }

        //Réitère la fonction pour changer de couleur
        else
        {
            cube.GetComponent<Renderer>().material.color = chosenColor;
            cube.GetComponent<CubeScript>().originalColor = chosenColor;
        }

    }

    //Permet de cacher la grille lorsqu'on n'est pas dans la bonne scène
    public void HideGrille()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                cubes[i, j].SetActive(false);
            }
        }
    }

    //Permet d'afficher la grille lorsqu'on est dans la scène du mini-jeu GameColor
    public void ShowGrille()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                cubes[i, j].SetActive(true);
            }
        }

    }

    //Permet de détruire tous les cubes de la scène
    public void DestroyAllCubes()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Destroy(cubes[i, j]);
            }
        }
    }

    //Permet de rénitialiser le jeu
    public void ResetGame()
    {
        combination.Clear();
        cluesCombination.Clear();
        cubeId = 1;
        DestroyAllCubes();
        Start();
    }

}