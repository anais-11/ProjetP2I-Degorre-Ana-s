using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManagerLoader : MonoBehaviour
{
    void Start()
    {
        // Trouve l'objet contenant la grille de jeu créée dans la scène 1
        GameObject GameColor = GameObject.Find("GameColor");

        // Active la grille de jeu
        GameColor.SetActive(true);
    }
}