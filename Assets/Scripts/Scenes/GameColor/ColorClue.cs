using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorClue 
{
    public int Order { get; set; }
    public Color Color { get; set; }
    public string RowAndCol { get; set; }

    public  ColorClue(int order, Color color, string rowAndCol)
    {
        this.Order = order;
        this.Color = color;
        this.RowAndCol = rowAndCol;

    }

}
