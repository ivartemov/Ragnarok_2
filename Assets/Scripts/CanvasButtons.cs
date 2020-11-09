using UnityEngine;

public class CanvasButtons : MonoBehaviour
{
    public GameObject panel;

    public void ToBuild()
    {
        if (panel.activeSelf)
            panel.SetActive(false);
        else
            panel.SetActive(true);
    }

    private void Update()
    {
        // если нажать на E, то откроется меню выбора построек
        if (Input.GetButtonDown("ToBuild"))
            ToBuild();
    }

    public void Building_1()
    {
        Building.Method();
    }
}
