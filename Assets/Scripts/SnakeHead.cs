using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeHead : MonoBehaviour {

    private static SnakeHead _instance;
    public static SnakeHead Instance
    {
        get
        {
            return _instance;
        }
    }

    public List<Transform> bodyList = new List<Transform>();
    public float speed = 0.35f;
    private float speedOld;
    public static int step = 35;
    private int x;
    private int y;
    private Vector3 headPos;
    public GameObject bodyPrefab;
    public Sprite[] bodySprites = new Sprite[2];
    public GameObject dieEffect;
    public AudioClip eatClip;
    public AudioClip dieClip;
    private Transform snakeBodyList;
    private bool isUpgrade = false;
    private bool isDie = false;

    void Awake()
    {
        snakeBodyList = GameObject.Find("SnakeBodyList").transform;
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("sh", "sh01"));
        bodySprites[0] = Resources.Load<Sprite>(PlayerPrefs.GetString("sb01", "sb0101"));
        bodySprites[1] = Resources.Load<Sprite>(PlayerPrefs.GetString("sb02", "sb0102"));
    }

    void Start()
    {
        speedOld = speed;
        InvokeRepeating("Move", 0, speed);
        x = 0;
        y = step;
    }

    void Update()
    {
        if (MainUI.Instance.isPause || isDie)
            return;
        if (speed != speedOld)
        {
            SpeedUp();
            speedOld = speed;
            if (speed == 0.05f)
                SnakeUpgrade();
        }
        if (Input.GetKey(KeyCode.W) && y != -step)
        {
            x = 0;
            y = step;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.S) && y != step)
        {
            x = 0;
            y = -step;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        else if (Input.GetKey(KeyCode.A) && x != step)
        {
            x = -step;
            y = 0;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        else if (Input.GetKey(KeyCode.D) && x != -step)
        {
            x = step;
            y = 0;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
        }
    }

    void Move()
    {
        headPos = gameObject.transform.localPosition;
        gameObject.transform.localPosition = new Vector3(headPos.x + x, headPos.y + y, headPos.z);
        if (bodyList.Count > 0)
        {
            for(int i = bodyList.Count - 2; i >= 0; i--)
            {
                bodyList[i + 1].localPosition = bodyList[i].localPosition;
            }
            bodyList[0].localPosition = headPos;
        }
    }

    void Grow()
    {
        int index = (bodyList.Count % 2 == 0 && !isUpgrade) ? 0 : 1;
        GameObject body = Instantiate(bodyPrefab, new Vector3(2000, 2000, 0), Quaternion.identity);
        body.GetComponent<Image>().sprite = bodySprites[index];
        body.transform.SetParent(snakeBodyList, false);
        bodyList.Add(body.transform);
        AudioSource.PlayClipAtPoint(eatClip, Vector3.zero);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Destroy(collision.gameObject);
            int maxx = FoodMaker.Instance.maxx;
            int maxy = FoodMaker.Instance.maxy;
            int xoffset = FoodMaker.Instance.xoffset;
            int x = Random.Range(-maxx + xoffset, maxx);
            int y = Random.Range(-maxy, maxy);
            int i = 0;
            while (i < snakeBodyList.childCount)
            {
                Vector3 t = snakeBodyList.GetChild(i).transform.localPosition;
                if (!(t.x <= x * step + 25 && t.x >= x * step - 25 && t.y <= y * step + 25 && t.y >= y * step - 25))
                    i++;
                else
                {
                    x = Random.Range(-maxx + xoffset, maxx);
                    y = Random.Range(-maxy, maxy);
                    i = 0;
                }
            }
            FoodMaker.Instance.MakeFood(x, y);
            Grow();
            MainUI.Instance.UpdateUI();
        }
        else if(collision.tag == "Body")
        {
            Die();
        }
        else
        {
            if (MainUI.Instance.isBorder)
            {
                Die();
                return;
            }
            switch (collision.gameObject.name)
            {
                case "Up":
                    transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y, transform.localPosition.z);
                    break;
                case "Down":
                    transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y, transform.localPosition.z);
                    break;
                case "Left":
                    transform.localPosition = new Vector3(-transform.localPosition.x + 7 * step, transform.localPosition.y, transform.localPosition.z);
                    break;
                case "Right":
                    transform.localPosition = new Vector3(-10 * step, transform.localPosition.y, transform.localPosition.z);
                    break;
            }
        }
    }

    public void SpeedUp()
    {
        CancelInvoke();
        InvokeRepeating("Move", 0, speed);
    }

    private void SnakeUpgrade()
    {
        isUpgrade = true;
        for(int i = 0; i < snakeBodyList.childCount; i++)
        {
            snakeBodyList.GetChild(i).GetComponent<Image>().sprite = bodySprites[1];
        }
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(dieClip, Vector3.zero);
        CancelInvoke();
        isDie = true;
        GameObject dieEff = Instantiate(dieEffect) as GameObject;
        dieEff.transform.SetParent(snakeBodyList, false);
        dieEff.transform.localPosition = headPos + new Vector3(0, 0, -1);
        PlayerPrefs.SetInt("lastScore", MainUI.Instance.score);
        if(PlayerPrefs.GetInt("bestScore",0) < MainUI.Instance.score)
        {
            PlayerPrefs.SetInt("bestScore", MainUI.Instance.score);
        }
        StartCoroutine(GameOver(2));
    }

    IEnumerator GameOver(float t)
    {
        yield return new WaitForSeconds(t);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void OnW()
    {
        if(y != -step)
        {
            x = 0;
            y = step;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void OnS()
    {
        if(y != step)
        {
            x = 0;
            y = -step;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
    }

    public void OnA()
    {
        if(x != step)
        {
            x = -step;
            y = 0;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
    }

    public void OnD()
    {
        if(x != -step)
        {
            x = step;
            y = 0;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
        }
    }
}
