using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public GameObject LevelSetup;
    public Button nxtbutt;
    public Button prevButt;

    public void Retry()
    {
        SceneManager.LoadScene("TouchyScene");
    }

    public void NextLevel()
    {
        if (PlayerPrefs.GetInt("currentLevel") == 3)
        {
            nxtbutt.interactable = false;
            
        }
        else
        {
            PlayerPrefs.SetInt("currentLevel", ++LevelSetup.GetComponent<LevelSetup>().currentLevel);
            SceneManager.LoadScene("TouchyScene");
        }
    }

    public void PrevLevel()
    {
        if (PlayerPrefs.GetInt("currentLevel") == 0)
        {
            prevButt.interactable = false;

        }
        else
        {
            PlayerPrefs.SetInt("currentLevel", --LevelSetup.GetComponent<LevelSetup>().currentLevel);
            SceneManager.LoadScene("TouchyScene");
        }
    }
}
