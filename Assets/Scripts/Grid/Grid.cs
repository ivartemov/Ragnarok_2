using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private LineRenderer lineRender;
    public GameObject grid;

    [SerializeField, Range(0.1f, 5f)]
    private float offset;           // уровень, на который поднята сетка над уровнем карты (plane)

    [SerializeField]
    private int mapLen = 150;       // длина карты, позже будет задаваться не вручную


    void Start()
    {
        lineRender = gameObject.GetComponent<LineRenderer>();
        gridLineRenderGenerate();
    }

    public void GridActive()
    {
        if (grid.activeSelf)
            grid.SetActive(false);
        else
            grid.SetActive(true);
    }

    private void gridLineRenderGenerate()
    {

        List<Vector3> lineRenderPositons = new List<Vector3>();

        lineRenderPositons.Add(new Vector3(0, offset, 0));

        for (int x = 0; x < mapLen + 1; x++)
        {
            float zPos = x % 2 == 0 ? mapLen : 0;
            float xPos = x;

            lineRenderPositons.Add(new Vector3(xPos, offset, zPos));

            if (x < 150)
                lineRenderPositons.Add(new Vector3(xPos + 1, offset, zPos));
        }

        lineRenderPositons.Add(new Vector3((mapLen % 2 == 0 ? mapLen : 0), offset, 0));


        for (int z = 0; z < mapLen + 1; z++)
        {
            float xPos = z % 2 != 0 ? mapLen : 0;
            float zPos = z;

            lineRenderPositons.Add(new Vector3(xPos, offset, zPos));

            if (z < 150)
                lineRenderPositons.Add(new Vector3(xPos, offset, zPos + 1));
        }

        lineRender.positionCount = lineRenderPositons.Count;
        lineRender.SetPositions(lineRenderPositons.ToArray());
        lineRender.startWidth = 0.1f;
        lineRender.endWidth = 0.1f;
    }
}
