using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class FurnitureInteractionPopUp : MonoBehaviour
{
    public TextMeshProUGUI NameObject { get; set; }
    public TextMeshProUGUI Action1 { get; set; }
    public TextMeshProUGUI Action2 { get; set; }
   
    //Permet de définir quels objects contiennent les textes à modifier 
    public void Awake()
    {
      NameObject= GameObject.Find("Name").GetComponent<TextMeshProUGUI>();
      Action1 = GameObject.Find("action1").GetComponent<TextMeshProUGUI>();
      Action2 = GameObject.Find("action2").GetComponent<TextMeshProUGUI>();
    }

    
}
