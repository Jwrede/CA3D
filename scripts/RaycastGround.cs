using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastGround : MonoBehaviour
{
    public GameObject CellularAutomata;
    public GameObject canvas;
    public Material material;
    public GameObject thisButton;
    public GameObject EditColorChange;
    private float offset = 0.01f;
    private GameObject ground;
    private List<GameObject> activeBefore = new List<GameObject>();

    public void spawnRaycastGround()
    {
        if (thisButton.GetComponentInChildren<Text>().text == "Edit")
        {
            foreach(Transform t in canvas.transform)
            {
                if (t.gameObject.activeSelf)
                {
                    t.gameObject.SetActive(t.name == "Edit");
                    activeBefore.Add(t.gameObject);
                }
            }
            edit();
            thisButton.GetComponentInChildren<Text>().text = "Apply";

        }
        else
        {
            apply();
            thisButton.GetComponentInChildren<Text>().text = "Edit";
        }
    }

    private void edit()
    {
        EditColorChange.SetActive(true);
        int amount = CellularAutomata.GetComponent<Basic>().amount;
        string dimension = CellularAutomata.GetComponent<Basic>().dimension;
        ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ground.GetComponent<MeshRenderer>().material = material;
        ground.transform.localScale = dimension == "1D" ? new Vector3(amount + offset*2, 1 + offset*2, 1 + offset*2) : new Vector3(amount + offset*2, 1 + offset*2, amount + offset*2);
        ground.transform.position = dimension == "1D" ? new Vector3(((float)amount / 2) - 0.5f, 0, 0) : new Vector3(((float)amount / 2) - 0.5f, 0, ((float)amount / 2) - 0.5f);
    }

    private void apply() 
    {
        foreach (GameObject o in activeBefore)
        {
            o.SetActive(true);
        }
        activeBefore = new List<GameObject>();
        EditColorChange.SetActive(false);
        Destroy(ground.GetComponent<MeshFilter>().mesh);
        Destroy(ground);
    }
}
