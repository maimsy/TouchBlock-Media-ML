using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using UnityEngine.SceneManagement;
using System.Linq;

public class TouchyAgent : Agent
{
    public GameObject baseofTiles;
    public InputManager inputManager;
    public Tile SelectedTile;

    [SerializeField]
    private int numberOfYellows = 0;


    private const int Up = 1;
    private const int Down = 2;
    private const int Left = 4;
    private const int Right = 3;

    ////movement 1 Up , 2 Down, 3 Left, 4 Right
    public override void CollectObservations()
    {
        SelectedTile = inputManager.selectedTile.GetComponent<Tile>();
        //SetMask(); 
    }

    private void SetMask()
    {
        // Prevents the agent from picking an action that would make it collide with a wall
        var positionX = SelectedTile.coords.X; //(int)transform.position.x;
        var positionY = SelectedTile.coords.Y; //(int)transform.position.z;
        var maxPosition = 3;

        if (positionX == 0)
        {
            SetActionMask(Right);
        }

        if (positionX == maxPosition)
        {
            SetActionMask(Left);
        }

        if (positionY == 0)
        {
            SetActionMask(Up);
        }

        if (positionY == maxPosition)
        {
            SetActionMask(Down);
        }
    }

    //Press leftA4, rightD3, upW1 and downS2
    //Aim is to reach red and cover all blues
    //SetReward when
    //Done when not being able to finish all blues
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        int act = (int)vectorAction[0];

        if (!LevelSetup.isActiveAndEnabled)
        {

            if (brain.brainParameters.vectorActionSpaceType == SpaceType.discrete)
            {
                switch (act)
                {
                    case 0:
                        inputManager.PressedW();
                        break;
                    case 1:
                        inputManager.PressedS();
                        break;
                    case 2:
                        inputManager.PressedD();
                        break;
                    case 3:
                        inputManager.PressedA();
                        break;
                    default:
                        break;
                }
            }

            //If on End and All are Yellows
            if (CheckWinCondition())
            {
                SetReward(1f);
                Debug.Log("CHAMPION");
                Done();
            }
            else
            {
                AddReward(-0.5f);
            }

            //How close to End
            if (Vector3.Distance(inputManager.selectedTile.gameObject.transform.position, LevelSetup.ListOfTiles.First(t => t.tileType == TileNameType.End).gameObject.transform.position) < 1f)
            {
                AddReward(0.02f);
                Debug.Log("Close to End Tile!");
            }


            //Number of Yellows
            int currentYellows = LevelSetup.ListOfTiles.Count(t => t.gameObject.GetComponent<Renderer>().sharedMaterial == (Material)Resources.Load("yellow", typeof(Material)));
            // Debug.Log("yellowws "+currentYellows);
            if (currentYellows > 12)
            {
                AddReward(0.6f);
                Debug.Log("yellowws");
            }
            else
            {
                AddReward(-0.5f);
            }
            numberOfYellows = currentYellows;
        }
    }



    public bool CheckWinCondition()
    {
        if (inputManager.selectedTile.tileType == TileNameType.End)
        {
            bool isAllYellow = !LevelSetup.ListOfTiles.Any(t => t.gameObject.GetComponent<Renderer>().sharedMaterial == (Material)Resources.Load("Blue", typeof(Material)));
            Debug.Log("Success check " + isAllYellow);
            return isAllYellow;
        }
        return false;
    }

    public LevelSetup LevelSetup;


    public override void AgentReset()
    {
        Debug.Log("AgentReset");

        if (PlayerPrefs.GetInt("currentLevel") >= 3)
        {
            PlayerPrefs.SetInt("currentLevel", 0);
        }
        else
        {
            PlayerPrefs.SetInt("currentLevel", PlayerPrefs.GetInt("currentLevel") + 1);
        }


        LevelSetup.enabled = true;
    }

    public void FixedUpdate()
    {
        WaitTimeInference();
    }

    public Camera renderCamera;
    public float timeBetweenDecisionsAtInference;
    private float timeSinceDecision;

    private void WaitTimeInference()
    {
        if (renderCamera != null)
        {
            renderCamera.Render();
        }


        else
        {
            if (timeSinceDecision >= timeBetweenDecisionsAtInference)
            {
                timeSinceDecision = 0f;
                RequestDecision();
            }
            else
            {
                timeSinceDecision += Time.fixedDeltaTime;
            }
        }
    }
}
