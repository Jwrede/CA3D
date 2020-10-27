using System;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public GameObject prefab;
    public GameObject CellularAutomata;
    public GameObject EditColorChange;
    private GameObject cube;
    private Basic script;
    private void Update()
    {
        script = CellularAutomata.GetComponent<Basic>();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo;
        if(Physics.Raycast(ray, out hitinfo))
        {
            RaycastHit selection = hitinfo;
            int x = (int)(selection.point.x + 0.5);
            int y = 0;
            int z = script.dimension == "1D" ? 0 : (int)(selection.point.z + 0.5);
            x = Mathf.Min(x, script.amount-1);
            x = Mathf.Max(x, 0);
            z = Mathf.Min(z, script.amount-1);
            z = Mathf.Max(z, 0);
            Vector3 position = new Vector3(x, y, z);
            
            if (cube == null)
            {
                cube = Instantiate(prefab, position, Quaternion.identity);
                cube.AddComponent<MeshRenderer>();
                cube.transform.localScale = new Vector3(1.01f, 1.01f, 1.01f);
            }
            else if (cube.transform.position != position)
            {
                hover(selection, position);
            }
            if (Input.GetMouseButtonDown(0))
            {
                click(selection, x,z);
            }
        }
        else
        {
            if(cube != null)
            {
                destroy(cube);
            }
        }
    }

    private void hover(RaycastHit selection, Vector3 position)
    {
        Destroy(cube.GetComponent<MeshFilter>().mesh);
        Destroy(cube);
        if(selection.transform.name == "Raycastground")
        {

            cube = Instantiate(prefab, position, Quaternion.identity);
            cube.AddComponent<MeshRenderer>();
        }

    }

    private void click(RaycastHit selection, int x, int z)
    {
        Basic script = CellularAutomata.GetComponent<Basic>();
        int currColor = EditColorChange.GetComponent<EditColorChange>().currColor;
        string dimension = script.dimension;
        if(dimension == "1D")
        {
            int[] currRow = script.currRow;
            currRow[x] = currRow[x] == currColor ? 0 : currColor;
        }
        else
        {
            int[,] currRow2D = script.currRow2D;
            currRow2D[x, z] = currRow2D[x,z] == currColor ? 0 : currColor;
        }
        script.gen--;
        for (int color = 0; color < script.cubeMeshAmount; color++)
        {
            destroy(script.CubeMeshes[color][script.gen]);
            script.CubeMeshes[color].RemoveAt(script.gen);
        }
        script.newCubeMesh(script.colors);
        script.spawnRow(script.colors);
    }

    private void destroy(GameObject o)
    {
        Destroy(o.GetComponent<MeshFilter>().mesh);
        Destroy(o.GetComponent<MeshFilter>());
        Destroy(o.GetComponent<MeshRenderer>());
        Destroy(o);
    }
}
