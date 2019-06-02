using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject baseParent;

    public Material green;
    public Material blue;
    public Material yellow;

    public Tile selectedTile;
    private Tile newSelectedTile;
    private List<Tile> ListOfTiles = new List<Tile>();

    private int numberOfRow;
    private int numberOfColumn;

 
    private void OnEnable()
    {
        ListOfTiles.Clear();
        ListOfTiles.AddRange(baseParent.transform.GetComponentsInChildren<Tile>());
        numberOfRow = GetComponent<LevelSetup>().numberOfRow;
        numberOfColumn = GetComponent<LevelSetup>().numberOfColumn;
        selectedTile = ListOfTiles.Find(x => x.generalTile.isSelected == true);

        //selectedTile = ListOfTiles.Find(x => x.coords.X == 0 && x.coords.Y == 0);
        newSelectedTile = selectedTile;
        selectedTile.gameObject.GetComponent<Renderer>().material = green;
    }


    // Update is called once per frame
    void Update()
    {

        selectedTile.gameObject.GetComponent<Renderer>().material = green;

        if (newSelectedTile == null) {
            Debug.Log("no new");
        }

        if (CheckifOnEnd())
        {
            //if all yellow change all level
            if (AllYellow())
            {
                //Next level
                Debug.Log("WIN");
            }
        }

         
        if (ListOfTiles.Count > 0 && selectedTile != null)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                PressedW();

            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                PressedA();
            }



            if (Input.GetKeyDown(KeyCode.S))
            {
                PressedS();
            }



            if (Input.GetKeyDown(KeyCode.D))
            {
                PressedD();
            } 
        }

    }

    public void PressedS()
    {

        int newY = (selectedTile.coords.Y == 0) ? 0 : selectedTile.coords.Y - 1;

        newSelectedTile = ListOfTiles.Find(x => x.coords.X == selectedTile.coords.X && x.coords.Y == newY);
        if (newSelectedTile.GetComponent<Renderer>().sharedMaterial != yellow &&
            newSelectedTile.generalTile.isPassable)
        {
            selectedTile.generalTile.isSelected = false;
            selectedTile.gameObject.GetComponent<Renderer>().material = yellow;
            selectedTile = newSelectedTile;
            if (selectedTile == null)
            {
                selectedTile.generalTile.isSelected = true;
            }
            else
            {
                selectedTile.generalTile.isSelected = true;
            }
        }

    }

    public void PressedD()
    {
        int newX = (selectedTile.coords.X == (numberOfColumn - 1)) ? numberOfColumn - 1 : selectedTile.coords.X + 1;
        newSelectedTile = ListOfTiles.Find(x => x.coords.X == newX && x.coords.Y == selectedTile.coords.Y);

        if (newSelectedTile.GetComponent<Renderer>().sharedMaterial != yellow &&
            newSelectedTile.generalTile.isPassable)
        {
            selectedTile.generalTile.isSelected = false;
            selectedTile.gameObject.GetComponent<Renderer>().material = yellow;

            selectedTile = newSelectedTile;
            if (selectedTile == null)
            {
                selectedTile.generalTile.isSelected = true;
            }
            else
            {
                selectedTile.generalTile.isSelected = true;
            }
        }
    }

    public void PressedA()
    {
        int newX = (selectedTile.coords.X == 0) ? 0 : selectedTile.coords.X - 1;
        newSelectedTile = ListOfTiles.Find(x => x.coords.X == newX && x.coords.Y == selectedTile.coords.Y);

        if (newSelectedTile.GetComponent<Renderer>().sharedMaterial != yellow &&
            newSelectedTile.generalTile.isPassable)
        {
            selectedTile.generalTile.isSelected = false;
            selectedTile.gameObject.GetComponent<Renderer>().material = yellow;
            selectedTile = newSelectedTile;
            if (selectedTile == null)
            {
                selectedTile.generalTile.isSelected = true;
            }
            else
            {
                selectedTile.generalTile.isSelected = true;
            }
        }
    }

    public void PressedW()
    {
        int newY = (selectedTile.coords.Y == (numberOfRow - 1)) ? numberOfRow - 1 : selectedTile.coords.Y + 1;

        newSelectedTile = ListOfTiles.Find(x => x.coords.X == selectedTile.coords.X && x.coords.Y == newY);
        if (newSelectedTile.GetComponent<Renderer>().sharedMaterial != yellow &&
            newSelectedTile.generalTile.isPassable
            )
        {
            selectedTile.generalTile.isSelected = false;
            selectedTile.gameObject.GetComponent<Renderer>().material = yellow;

            selectedTile = newSelectedTile;

            if (selectedTile == null)
            {
                selectedTile.generalTile.isSelected = true;
            }
            else
            {
                selectedTile.generalTile.isSelected = true;
            }
        }
    }

    private bool AllYellow()
    {
        foreach (Tile t in ListOfTiles)
        {
            if (t.tileType == TileNameType.Normal && t.GetComponent<Renderer>().sharedMaterial != yellow)
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckifOnEnd()
    {
        if (selectedTile.tileType == TileNameType.End)
        {

            return true;
        }
        return false;
    }
}
