using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodMaker : MonoBehaviour {

    private static FoodMaker _instance;
    public static FoodMaker Instance
    {
        get
        {
            return _instance;
        }
    }

    public int maxx = 10;
    public int maxy = 6;
    public int xoffset = 4;
    public GameObject foodPrefab;
    public Sprite[] foodSprites;
    private Transform foodHolder;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        foodHolder = GameObject.Find("FoodHolder").transform;
        MakeFood(Random.Range(-3, 3), Random.Range(-3, 3));
    }

    public void MakeFood(int x,int y)
    {
        int index = Random.Range(0, foodSprites.Length);
        GameObject food = Instantiate(foodPrefab);
        food.GetComponent<Image>().sprite = foodSprites[index];

        food.transform.SetParent(foodHolder,false);

        food.transform.localPosition = new Vector3(x * SnakeHead.step, y * SnakeHead.step, 0); ;
    }
}
