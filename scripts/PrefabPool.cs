using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabPool : MonoBehaviour
{
    public GameObject prefab;
    private int size;
    public Button Dimension;
    private static List<GameObject> pool;
    private int index;
    public void init(int amount)
    {
        pool = new List<GameObject>();
        if (Dimension.GetComponentInChildren<Text>().text == "2D") amount *= amount;
        size = amount*100;
        index = 0;
        for (int i = 0; i < size; i++)
        {
            GameObject cube = Instantiate(prefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            pool.Add(cube);
            cube.SetActive(false);
        }
    }

    public GameObject getPooledObj()
    {
        GameObject cube;
        if (index < size)
        {
            cube = pool[index];
            cube.SetActive(true);
        }
        else
        {
            index = 0;
            cube = pool[index];
        }
        cube.transform.gameObject.tag = "Cubes";
        cube = pool[index];
        index++;
        return cube;
    }
}
