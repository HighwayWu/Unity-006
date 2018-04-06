using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour {

    private static MainUI _instance;

    public static MainUI Instance
    {
        get
        {
            return _instance;
        }
    }

    public int score = 0;
    public bool isBorder = true;
    public bool isPause = false;
    public SnakeHead snakeHead;
    public Text levelText;
    public Text scoreText;
    public Image bgImage;
    public Button pauseBtn;
    public Sprite[] pauseSprite;
    private Color tempColor;
    int width;
    int height;

    void Awake()
    {
        width = Screen.currentResolution.width;
        height = Screen.currentResolution.height;
        _instance = this;
        snakeHead = GameObject.Find("SnakeHead").GetComponent<SnakeHead>();
    }

    void Start()
    {
        Screen.SetResolution(1280, 720, true);
        if (PlayerPrefs.GetInt("border", 1) == 0)
        {
            isBorder = false;
        }
        else
        {
            isBorder = true;
        }
    }

    void Update()
    {
        if(score > 200 && score <= 500)
        {
            ColorUtility.TryParseHtmlString("#CCEEFFFF", out tempColor);
            bgImage.color = tempColor;
            levelText.text = "2";
            snakeHead.speed = 0.3f;
        }
        else if(score > 500 && score <= 1000)
        {
            ColorUtility.TryParseHtmlString("#CCFFDBFF", out tempColor);
            bgImage.color = tempColor;
            levelText.text = "3";
            snakeHead.speed = 0.25f;
        }
        else if (score > 1000 && score <= 1500)
        {
            ColorUtility.TryParseHtmlString("#EBFFCCFF", out tempColor);
            bgImage.color = tempColor;
            levelText.text = "4";
            snakeHead.speed  = 0.2f;
        }
        else if (score > 1500 && score <= 2100)
        {
            ColorUtility.TryParseHtmlString("#FFF3CCFF", out tempColor);
            bgImage.color = tempColor;
            levelText.text = "5";
            snakeHead.speed  = 0.15f;
        }
        else if(score > 2100)
        {
            ColorUtility.TryParseHtmlString("#FFDACCFF", out tempColor);
            bgImage.color = tempColor;
            levelText.text = "∞";
            snakeHead.speed  = 0.05f;
        }
    }

    public void UpdateUI(int s = 100)
    {
        score += s;
        scoreText.text = score.ToString();
    }

    public void Pause()
    {
        isPause = !isPause;
        if (isPause)
        {
            Time.timeScale = 0;
            pauseBtn.GetComponent<Image>().sprite = pauseSprite[1];
        }
        else
        {
            Time.timeScale = 1;
            pauseBtn.GetComponent<Image>().sprite = pauseSprite[0];

        }
    }

    public void Home()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    void OnApplicationPause(bool paused)
    {
        if (!paused)
        {
            Screen.SetResolution(1280, 720, true);
        }
    }
}
