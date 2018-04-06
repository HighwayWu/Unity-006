using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour {

    public Text lastText;
    public Text bestText;
    public Toggle blue;
    public Toggle yellow;
    public Toggle border;
    public Toggle free;
    int width;
    int height;

    void Awake()
    {
        Time.timeScale = 1;
        bestText.text = "Best Score: " + PlayerPrefs.GetInt("bestScore", 0);
        lastText.text = "Last Score: " + PlayerPrefs.GetInt("lastScore", 0);
        width = Screen.currentResolution.width;
        height = Screen.currentResolution.height;
    }

    void Start()
    {
        Screen.SetResolution(1280, 720, true);
        if(PlayerPrefs.GetString("sh", "sh01") == "sh01")
        {
            blue.isOn = true;
            PlayerPrefs.SetString("sh", "sh01");
            PlayerPrefs.SetString("sb01", "sb0101");
            PlayerPrefs.SetString("sb02", "sb0102");
        }
        else
        {
            yellow.isOn = true;
            PlayerPrefs.SetString("sh", "sh02");
            PlayerPrefs.SetString("sb01", "sb0201");
            PlayerPrefs.SetString("sb02", "sb0202");
        }
        if(PlayerPrefs.GetInt("border", 1) == 1)
        {
            border.isOn = true;
            PlayerPrefs.SetInt("border", 1);
        }
        else
        {
            free.isOn = true;
            PlayerPrefs.SetInt("border", 0);
        }
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void BlueSelected(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetString("sh", "sh01");
            PlayerPrefs.SetString("sb01", "sb0101");
            PlayerPrefs.SetString("sb02", "sb0102");
        }
    }

    public void YellowSelected(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetString("sh", "sh02");
            PlayerPrefs.SetString("sb01", "sb0201");
            PlayerPrefs.SetString("sb02", "sb0202");
        }
    }

    public void BorderSelected(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("border", 1);
        }
    }

    public void FreeSelected(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("border", 0);
        }
    }

    void OnApplicationPause(bool paused)
    {
        if (!paused)
        {
            Screen.SetResolution(1280, 720, true);
        }
    }
}
