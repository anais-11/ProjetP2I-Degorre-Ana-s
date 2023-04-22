using UnityEngine;


public class Interaction
{
    //Nom de l'objet officiel sur l'interface
    public string NameObject { get; set; }

    //Nom de l'objet utilisé
    public string NameUsed { get; set; }

    //Actions possibles que l'on peut faire avec l'objet
    public string[] Actions { get; set; }

    //Numéro de l'action correcte ( 1 = l'action n°1 est la bonne; 2= l'action n°2 est la bonne ; 3 = aucune action n'est bonne)
    public int RightChoice { get; set; }

    //Indique quel est le résultat de l'action lorsqu'elle est correcte ("Clue", "Game", "Nothing")
    public string Result { get; set; }

    public Interaction(string name,string nameUsed, string[] actions, int rightChoice, string result)
    {
        this.NameObject = name;
        this.NameUsed = nameUsed;
        this.Actions = actions;
        this.RightChoice = rightChoice;
        this.Result = result;
    }
}
