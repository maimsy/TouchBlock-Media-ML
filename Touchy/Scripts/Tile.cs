using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{ 
    public Coords coords;
    public TileNameType tileType;
    public GeneralTile generalTile;
    

    private Material purple; //start
    private Material pink;   //end
    private Material black; //touch

    void Awake()
    {
        coords = new Coords() { X = 0, Y = 0 };
        purple = (Material)Resources.Load("purple", typeof(Material));
        pink = (Material)Resources.Load("pink", typeof(Material));
        black = (Material)Resources.Load("black", typeof(Material));
        //GeneralTile(tileType); 
    }

    public void GeneralTile(TileNameType t)
    {
        if (t == TileNameType.End) {
            generalTile = new TileEnd(); 
        }
        else if (t == TileNameType.Start) {
            generalTile = new TileStart(); 
        }
        else if (t == TileNameType.Touch) {
            generalTile = new TileTouch(); 
        }
        else { generalTile = new TileNormal(); }
        generalTile.InitTile();
    }

    public void SetMaterial(TileNameType t)
    {
        if (t == TileNameType.End)
        { 
            GetComponent<Renderer>().material = pink;
        }
        else if (t == TileNameType.Start)
        { 
            GetComponent<Renderer>().material = purple;
        }
        else if (t == TileNameType.Touch)
        { 
            GetComponent<Renderer>().material = black;
        }
        else {  } 
    }

    public void SetCoords(int x, int y)
    {
        if (coords != null)
        {
            coords.X = x;
            coords.Y = y;
        }
    }

    public Coords GetCoords()
    {
        return coords;
    }

    public void Selected()
    {
        //Change material;
    }

    public void Deselected()
    {
        //Change material
    }
}