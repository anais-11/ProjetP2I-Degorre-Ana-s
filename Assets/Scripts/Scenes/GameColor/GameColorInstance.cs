using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameColorInstance : MonoBehaviour
{
    public static GameColorInstance instance;
    public static InstanciateColorGame gameColor;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            // Si une autre instance de cet objet existe déjà, détruire l'objet actuel
            Destroy(gameObject);
            return;
        }

        // Sinon, définir cette instance comme l'instance unique
        instance = this;
        DontDestroyOnLoad(gameObject);

        // Vérifier si une instance  existe déjà
        if (gameColor == null)
        {             
            gameColor = FindObjectOfType<InstanciateColorGame>();

        }

    }
}