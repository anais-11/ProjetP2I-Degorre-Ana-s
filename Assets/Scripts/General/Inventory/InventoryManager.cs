using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public static Inventory inventory;

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

        // Vérifier si une instance de l'inventaire existe déjà
        if (inventory == null)
        {
            // Récupérer l'inventaire depuis l'objet Inventory dans la scène
            inventory = FindObjectOfType<Inventory>();
            
        }

    }
} 