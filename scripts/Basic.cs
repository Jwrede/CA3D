using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Basic : MonoBehaviour
{
    private bool initialized = false;
    [HideInInspector] public int[] currRow;
    public int[] nextRow;
    [HideInInspector] public int[,] currRow2D;
    private int[,] nextRow2D;
    [HideInInspector] public int amount;
    private int[] rule;
    [HideInInspector] public List<List<GameObject>> CubeMeshes = new List<List<GameObject>>();
    [HideInInspector] public Color[] ColorsArr;
    [HideInInspector] public int gen;
    [HideInInspector] public int colors;
    [HideInInspector] public int cubeMeshAmount;
    private bool transparent;
    private int Base;
    private bool time;
    [HideInInspector] public string dimension;
    private bool globalUpdate;
    private string neighbourhood;
    private bool gameOfLife;
    private int MinGOL0;
    private int MaxGOL0;
    private int MinGOL1;
    private int MaxGOL1;
    public Toggle UpdateInPlace;
    public Button Pause;
    public Slider ColorSlider;
    public Toggle Transparent;
    public InputField Rule;
    public Button Dimension;
    public GameObject prefabCube;
    public Slider UpdateTime;
    public Slider InitialState;
    public Toggle GlobalUpdate;
    public Button Neighbourhood;
    public Slider GameOfLife;
    public Camera MainCamera;
    public Button RandomString;
    public GameObject Tooltip;
    public Text GenText;

    void initArrays(int amount)
    {
        if (dimension == "1D")
        {
            this.amount = amount;
            currRow = new int[amount];
            nextRow = new int[amount];

            for (int i = 0; i < amount; i++)
            {
                if (InitialState.value == 0)
                {
                    if (i == amount / 2)
                    {
                        currRow[i] = 1;
                    }
                    else
                    {
                        currRow[i] = 0;
                    }
                }
                else
                {
                    float rand = UnityEngine.Random.Range(0f, 1f);
                    currRow[i] = rand > 1 - InitialState.value ? 1 : 0;
                }
            }
        }
        else if (dimension == "2D")
        {
            this.amount = amount;
            currRow2D = new int[amount, amount];
            nextRow2D = new int[amount, amount];

            for (int i = 0; i < amount; i++)
            {
                for (int j = 0; j < amount; j++)
                {
                    if (InitialState.value == 0)
                    {
                        if (i == amount / 2 && j == amount / 2)
                        {
                            currRow2D[i, j] = 1;
                        }
                        else
                        {
                            currRow2D[i, j] = 0;
                        }
                    }
                    else
                    {
                        float rand = UnityEngine.Random.Range(0f, 1f);
                        currRow2D[i, j] = rand > 1 - InitialState.value ? 1 : 0;
                    }
                }
            }

        }
    }

    public void spawnRow(int colors)
    {
        if (dimension == "1D")
        {
            int cubeMeshesIndex = 0;
            for (int color = 0; color <= colors; color++)
            {
                if((!transparent && color == 0) || color > 0)
                {
                    for (int i = 0; i < amount; i++)
                    {
                        if (currRow[i] == color)
                        {
                            GameObject cube = Instantiate(prefabCube, new Vector3(i, 0, 0), Quaternion.identity, parent: CubeMeshes[cubeMeshesIndex][gen].transform);
                            cube.transform.gameObject.tag = "Cubes";
                        }
                    }
                    combineMeshes(cubeMeshesIndex, color);
                    cubeMeshesIndex++;
                }
            }
            gen++;

        }
        else if (dimension == "2D")
        {
            int cubeMeshesIndex = 0;
            
            for (int color = 0; color <= colors; color++)
            {
                if ((!transparent && color == 0) || color > 0) {
                    for (int i = 0; i < amount; i++)
                    {
                        for (int j = 0; j < amount; j++)
                        {
                            if (currRow2D[i, j] == color)
                            {
                                GameObject cube = Instantiate(prefabCube, new Vector3(i, 0, j), Quaternion.identity, parent: CubeMeshes[cubeMeshesIndex][gen].transform);
                                cube.transform.gameObject.tag = "Cubes";
                            }
                        }
                    }
                    combineMeshes(cubeMeshesIndex, color);
                    cubeMeshesIndex++;
                }
            }
            gen++;
        }
    }

    private void deleteTag(string tag)
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject cube in cubes)
        {
            Destroy(cube.GetComponent<MeshFilter>().mesh);
            Destroy(cube);
        }
    }

    private void combineMeshes(int cubeMeshesIndex, int color)
    {
        Vector3 position = CubeMeshes[cubeMeshesIndex][gen].transform.position;
        CubeMeshes[cubeMeshesIndex][gen].transform.position = Vector3.zero;

        MeshFilter[] meshFilters = CubeMeshes[cubeMeshesIndex][gen].GetComponentsInChildren<MeshFilter>();
        deleteTag("Cubes");

        List<CombineInstance> combine = new List<CombineInstance>();
        for (int i = 0; i < meshFilters.Length; i++)
        {
            if (meshFilters[i] != null && i > 0)
            {
                CombineInstance ci = new CombineInstance();
                ci.mesh = meshFilters[i].sharedMesh;
                ci.transform = meshFilters[i].transform.localToWorldMatrix;
                combine.Add(ci);
            }
        }
        CubeMeshes[cubeMeshesIndex][gen].transform.GetComponent<MeshFilter>().mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        CubeMeshes[cubeMeshesIndex][gen].transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine.ToArray(), true, true);
        CubeMeshes[cubeMeshesIndex][gen].transform.position = position;
        CubeMeshes[cubeMeshesIndex][gen].GetComponent<MeshRenderer>().material.color = ColorsArr[color];
        CubeMeshes[cubeMeshesIndex][gen].transform.gameObject.SetActive(true);
    }

    private int modulo(int dividend, int divisor)
    {
        int rest = dividend % divisor;
        return rest < 0 ? rest + divisor : rest;
    }

    private void updateRow()
    {
        if (dimension == "1D")
        {
            for (int i = 0; i < amount; i++)
            {
                if (neighbourhood == "2 Neighbours")
                {
                    nextRow[i] = apply(currRow[modulo(i - 1, amount)], currRow[i], currRow[modulo(i + 1, amount)]);
                }
                else if (neighbourhood == "4 Neighbours")
                {
                    nextRow[i] = apply(currRow[modulo(i - 2, amount)], currRow[modulo(i - 1, amount)], currRow[i], currRow[modulo(i + 1, amount)], currRow[modulo(i + 2, amount)]);
                }
                else if (neighbourhood == "6 Neighbours")
                {
                    nextRow[i] = apply(currRow[modulo(i - 3, amount)], currRow[modulo(i - 2, amount)], currRow[modulo(i - 1, amount)], currRow[i], currRow[modulo(i + 1, amount)], currRow[modulo(i + 2, amount)], currRow[modulo(i + 3, amount)]);
                }
            }
            if (globalUpdate)
            {
                Array.Copy(nextRow, currRow, amount);
            }
            else
            {
                currRow = nextRow;
            }
        }
        else if (dimension == "2D")
        {
            for (int i = 0; i < amount; i++)
            {
                for (int j = 0; j < amount; j++)
                {
                    if (neighbourhood == "4 Neighbours")
                    {
                        nextRow2D[i, j] = apply(currRow2D[modulo(i - 1, amount), j], currRow2D[i, modulo(j - 1, amount)], currRow2D[i, j], currRow2D[i, modulo(j + 1, amount)], currRow2D[modulo(i + 1, amount), j]);

                    }
                    else if (neighbourhood == "8 Neighbours")
                    {
                        nextRow2D[i, j] = apply(currRow2D[modulo(i - 1, amount), modulo(j - 1, amount)], currRow2D[modulo(i - 1, amount), j], currRow2D[modulo(i - 1, amount), modulo(j + 1, amount)],
                                                    currRow2D[i, modulo(j - 1, amount)], currRow2D[i, j], currRow2D[i, modulo(j + 1, amount)],
                                                    currRow2D[modulo(i + 1, amount), modulo(j - 1, amount)], currRow2D[modulo(i + 1, amount), j], currRow2D[modulo(i + 1, amount), modulo(j + 1, amount)]);
                    }
                }
            }
            if (globalUpdate)
            {
                currRow2D = (int[,])nextRow2D.Clone();
            }
            else
            {
                currRow2D = nextRow2D;
            }
        }
    }

    private int apply(params int[] list)
    {
        if (gameOfLife)
        {
            if (list[list.Length / 2] == 1)
            {
                return list.Sum() >= MinGOL1 && list.Sum() <= MaxGOL1 ? 1 : 0;

            }
            else
            {
                return list.Sum() >= MinGOL0 && list.Sum() <= MaxGOL0 ? 1 : 0;
            }
        }
        string s = "";
        foreach (int i in list)
        {
            s += i;
        }
        int index = convertToInt(s, Base);
        return rule[index];
    }


    public void newCubeMesh(int colors)
    {
        for (int i = 0; i < cubeMeshAmount; i++)
        {
            CubeMeshes.Add(new List<GameObject>());
            CubeMeshes[i].Add(new GameObject("Cube" + gen + "_" + i));
            CubeMeshes[i][gen].transform.gameObject.tag = "CombinedMesh";
            CubeMeshes[i][gen].AddComponent<MeshFilter>();
            CubeMeshes[i][gen].AddComponent<MeshRenderer>();
        }
    }

    private int convertToInt(string str, int Base)
    {
        int len = str.Length;
        int power = 1;
        int num = 0;

        for (int i = len - 1; i >= 0; i--)
        {
            num += Int32.Parse(str[i].ToString()) * power;
            power *= Base;
        }

        return num;
    }

    private void initColorArray()
    {
        ColorsArr = new Color[(int)ColorSlider.value + 1];
        for (int i = 1; i <= ColorSlider.value + 1; i++)
        {
            Dropdown currDropdown = GameObject.Find("Color" + i).GetComponent<Dropdown>();
            ColorsArr[i - 1] = getColor(currDropdown.options[currDropdown.value].text);
        }
    }

    private Color getColor(string color)
    {
        if (color == "blue") return Color.blue;
        else if (color == "green") return Color.green;
        else if (color == "white") return new Color(255f, 255f, 255f);
        else if (color == "black") return Color.black;
        else if (color == "yellow") return new Color(255f, 255f, 0f);
        else if (color == "magenta") return Color.magenta;
        else if (color == "red") return Color.red;
        else return Color.grey;
    }

    IEnumerator waiter()
    {
        while (true)
        {
            if (Pause.GetComponentInChildren<Text>().text == "Resume")
            {
                break;
            }
            yield return new WaitForSeconds(UpdateTime.value);

            newCubeMesh(colors);
            updateRow();
            GenText.text = "Gen: " + gen;

            if (!time)
            {
                for (int color = 0; color < cubeMeshAmount; color++)
                {
                    foreach (GameObject o in CubeMeshes[color])
                    {
                        if (o != null) o.transform.position += new Vector3(0, 1f, 0);
                    }
                    if (gen >= 1000 && dimension == "1D")
                    {
                        Destroy(CubeMeshes[color][gen - 1000].GetComponent<MeshFilter>().mesh);
                        Destroy(CubeMeshes[color][gen - 1000]);
                    }
                    else if (gen >= 100 && dimension == "2D")
                    {
                        Destroy(CubeMeshes[color][gen - 100].GetComponent<MeshFilter>().mesh);
                        Destroy(CubeMeshes[color][gen - 100]);
                    }
                }
            }
            else
            {
                for (int color = 0; color < cubeMeshAmount; color++)
                {
                    Destroy(CubeMeshes[color][gen - 1].GetComponent<MeshFilter>().mesh);
                    Destroy(CubeMeshes[color][gen - 1]);
                }
            }
            spawnRow(colors);
        }
    }


    public void spawnNextRow()
    {
        if (initialized)
        {
            if (Pause.GetComponentInChildren<Text>().text == "Pause") Pause.GetComponentInChildren<Text>().text = "Resume";
            else Pause.GetComponentInChildren<Text>().text = "Pause";
            StartCoroutine(waiter());
        }
    }

    IEnumerator showTooltip()
    {
        Tooltip.transform.Find("Image").gameObject.SetActive(true);
        Tooltip.GetComponentInChildren<Text>().text = "Your Rule should be " + RandomString.GetComponent<RandomString>().size + " numbers long and just contain numbers between 0 and " + (Base - 1);
        yield return new WaitForSeconds(4f);
        Tooltip.transform.Find("Image").gameObject.SetActive(false);
    }

    private bool ruleCheck(string rule)
    {
        if (rule.Length != RandomString.GetComponent<RandomString>().size) return false;
        foreach (char c in rule)
        {
            int n;
            if (!Int32.TryParse(c.ToString(), out n)) return false;
            if (n < 0 || n >= Base) return false;
        }
        return true;
    }

    public void init(float amountf)
    {
        colors = (int)ColorSlider.value;
        Base = colors + 1;
        string ruleString = Rule.GetComponent<InputField>().text;
        if (ruleCheck(ruleString))
        {
            rule = ruleString.ToCharArray().Select(c => Convert.ToInt32(c.ToString())).ToArray();
            Array.Reverse(rule);

            initialized = true;
            transparent = Transparent.isOn;
            CubeMeshes = new List<List<GameObject>>();

            cubeMeshAmount = colors;
            if (!transparent)
            {
                cubeMeshAmount++;
            }

            deleteTag("CombinedMesh");

            gen = 0;
            GenText.text = "Gen: " + gen;

            newCubeMesh(colors);
            initColorArray();

            globalUpdate = GlobalUpdate.isOn;
            neighbourhood = Neighbourhood.GetComponentInChildren<Text>().text;
            gameOfLife = GameOfLife.value == 1;
            if (gameOfLife)
            {
                MinGOL0 = (int)GameObject.Find("LowerBound0").GetComponent<Slider>().value;
                MaxGOL0 = (int)GameObject.Find("UpperBound0").GetComponent<Slider>().value;
                MinGOL1 = (int)GameObject.Find("LowerBound1").GetComponent<Slider>().value + 1;
                MaxGOL1 = (int)GameObject.Find("UpperBound1").GetComponent<Slider>().value + 1;
            }
            dimension = Dimension.GetComponentInChildren<Text>().text;
            int amount = (int)amountf;
            initArrays(amount);

            time = UpdateInPlace.isOn;
            spawnRow(colors);

            MainCamera.GetComponent<Movement>().movingToTarget = true;
            MainCamera.GetComponent<Movement>().moveTowards = new Vector3(amount / 2, amount / 6, -amount / 3);
            MainCamera.GetComponent<Movement>().rotateTowards = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            StartCoroutine(showTooltip());
        }
    }
}