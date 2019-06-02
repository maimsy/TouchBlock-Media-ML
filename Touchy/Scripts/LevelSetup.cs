using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetup : MonoBehaviour
{

    public int numberOfColumn;
    public int numberOfRow;

    public int currentLevel = 0;
    public GameObject baseParent;
    public GameObject baseObject;


    public List<Tile> ListOfTiles = new List<Tile>();
    private List<Levels> levels = new List<Levels>();

    class coord
    {
        public int x;
        public int y;

        public coord(int v1, int v2)
        {
            x = v1;
            y = v2;
        }
    }

    class Levels
    {
        public int level;
        public int row;
        public coord start;
        public coord end;
        public coord[] touch = new coord[5];
    }

 

    void populateLevels()
    {
        Levels l1 = new Levels();
        l1.level = 1;  l1.row = 6;  l1.start = new coord(0, 0);  l1.end = new coord(3, 3); l1.touch[0] = new coord(2, 3);
        levels.Add(l1);
        Levels l2 = new Levels();
        l2.level = 2; l2.row = 6; l2.start = new coord(0, 0); l2.end = new coord(3, 3); l2.touch[0] = new coord(2, 2);
        levels.Add(l2);
        Levels l3 = new Levels();
        l3.level = 3; l3.row = 6; l3.start = new coord(0, 0); l3.end = new coord(3, 3); l3.touch[0] = new coord(0, 1);
        levels.Add(l3);
        Levels l4 = new Levels();
        l4.level = 4; l4.row = 6; l4.start = new coord(0, 0); l4.end = new coord(4, 4); l4.touch[0] = new coord(1, 2);
        levels.Add(l4);
    }



    private void OnEnable()
    {
        GetComponent<InputManager>().enabled = false;

        levels.Clear();
        populateLevels();

        int countofchiuld = baseParent.transform.childCount;

        if (PlayerPrefs.HasKey("currentLevel"))
        {
            currentLevel = PlayerPrefs.GetInt("currentLevel");
        }
        else {
            PlayerPrefs.SetInt("currentLevel", 0);
        }

        numberOfRow = numberOfColumn = levels[currentLevel].row;

        //if (countofchiuld > (numberOfRow * numberOfColumn))
        //{
        //    for (int i = countofchiuld-1; i > (numberOfRow * numberOfColumn); i--)
        //    {
        //        baseParent.transform.GetChild(i).gameObject.SetActive(false);
        //    }
        //}
        //else {
          
        //    foreach(Transform g in baseParent.GetComponentsInChildren<Transform>())
        //    {
        //        g.gameObject.SetActive(true);
        //    }
        //}

        for (int i = 0; i < numberOfRow; i++)
        {
            for (int j = 0; j < numberOfColumn; j++)
            {
                GameObject t;
                //Base objects
                if (countofchiuld == 0)
                {
                    t = Instantiate(baseObject, new Vector3(i * 1.5f, 0, j * 1.5f), Quaternion.identity, baseParent.transform);
                }
                //else if (countofchiuld < (numberOfRow * numberOfColumn)) {
                //    t = Instantiate(baseObject, new Vector3(i * 1.5f, 0, j * 1.5f), Quaternion.identity, baseParent.transform);
                //} 
                else
                {
                    t = baseParent.transform.GetChild(numberOfRow * i + j).gameObject; 
                }
                t.GetComponent<Renderer>().sharedMaterial = (Material)Resources.Load("Blue", typeof(Material));
                t.GetComponent<Tile>().SetCoords(i, j);

                //TODO: Set it better for each level
                if (i == levels[currentLevel].start.x && j == levels[currentLevel].start.y) { t.GetComponent<Tile>().tileType = TileNameType.Start; }
                else if (i == levels[currentLevel].end.x && j == levels[currentLevel].end.y) { t.GetComponent<Tile>().tileType = TileNameType.End; }
                else if (i == levels[currentLevel].touch[0].x && j == levels[currentLevel].touch[0].y)
                {
                    t.GetComponent<Tile>().tileType = TileNameType.Touch;
                }
                else t.GetComponent<Tile>().tileType = TileNameType.Normal;
                t.GetComponent<Tile>().SetMaterial(t.GetComponent<Tile>().tileType);
                t.GetComponent<Tile>().GeneralTile(t.GetComponent<Tile>().tileType);
            }
        }

       
            ListOfTiles.Clear(); 
        ListOfTiles = new List<Tile>(baseParent.transform.GetComponentsInChildren<Tile>());

        //TODO: Set First Selected in a better way for each level
        Coords c = new Coords { X = 0, Y = 0 };
        ListOfTiles.Find(x => x.coords.X == levels[currentLevel].start.x && x.coords.Y == levels[currentLevel].start.y).generalTile.isSelected = true;

        //TODO: Do this in a better way for each level
        GetComponent<InputManager>().enabled = true;


        this.enabled = false;
    }




}
