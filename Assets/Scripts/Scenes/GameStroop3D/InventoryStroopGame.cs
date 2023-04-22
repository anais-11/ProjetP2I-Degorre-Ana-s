using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryStroopGame : MonoBehaviour
{
    //nombre de diamants que le joueur doit rammasser
    public int NbRed { get; set; }
    public int NbGreen { get; set; }
    public int NbBlue { get; set; }
    public int NbYellow { get; set; }

    public List<TextColor> listText;
    public TextMeshProUGUI NbDiams { get; set; }
    public int DiamsCount { get; set; }
    public TextMeshProUGUI NbDiamsTotal { get; set; }
    public int DiamsTotal { get; set; }



    private void Start()
    {
        InitiateNbDiams();
    }

    // Update is called once per frame
    void Update()
    {
        NbDiams = GameObject.Find("diamsCounter").GetComponent<TextMeshProUGUI>();
        NbDiams.text = DiamsCount.ToString();

    }

    public void InitiateNbDiams()
    {
        DiamsCount = 0;
        DiamsTotal = NbBlue + NbGreen + NbRed + NbYellow;

        NbDiamsTotal = GameObject.Find("diamsTotal").GetComponent<TextMeshProUGUI>();
        NbDiamsTotal.text = DiamsTotal.ToString();
    }

    //Permet de savoir si le joueur a ramassé tous les diamants
    public bool VerifyDiamsCount()
    {
        bool equal = false;

        if (DiamsCount == DiamsTotal)
        {
            equal = true;
        }
        return equal;
    }

    public void ResetStroopInventory()
    {
        NbBlue = 0;
        NbGreen = 0;
        NbRed = 0;
        NbYellow = 0;
        listText.Clear();
        DiamsCount = 0;
        DiamsTotal = 0;
    }
}
