using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Slider ColorSlider;
    public Button RandomString;
    public GameObject CanvasObj;
    public Button StartStop;
    private bool cursorVisible = true;
    void Start()
    {
        ColorSlider.GetComponent<DeactivateColorPickers>().deactivateColorPickers();
        RandomString.GetComponent<RandomString>().randomString();
    }
    private void Update()
    {
        if (Cursor.visible) Cursor.visible = cursorVisible;
        if (Input.GetKeyDown("escape")) Application.Quit();
        if (Input.GetKeyDown("return"))
        {
            cursorVisible = !cursorVisible;
            CanvasObj.SetActive(!CanvasObj.activeSelf);
        }
        if (Input.GetKeyDown("b")) StartStop.onClick.Invoke();
    }
}
