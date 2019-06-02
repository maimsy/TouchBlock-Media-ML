using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject baseParent;
    private List<Tile> ListOfTiles;
    public Tile selectedTile;

    private void Start()
    {
        ListOfTiles = new List<Tile>(baseParent.transform.GetComponentsInChildren<Tile>());
    }

    private void OnEnable()
    {
        PlayerPrefs.SetInt("currentLevel", 0);
    }
}
