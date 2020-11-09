using UnityEngine;
using UnityEngine.EventSystems;

public class Build : MonoBehaviour
{
    public bool[,] occupiedCells = new bool[150, 150];

    private GameObject cube;

    Ray ray;
    RaycastHit hit;

    private bool buildMode = false;
    private bool placement = true;

    private void Start()
    {
        cube = CreateCube();
    }

    private void Update()
    {
        if (buildMode && !EventSystem.current.IsPointerOverGameObject())
        {
            if (placement)
            {
                Placement();
                if (Input.GetMouseButton(0))
                {
                    placement = false;
                    buildMode = false;
                }
            }
            else if (Input.GetMouseButton(1))
            {
                placement = true;
                buildMode = false;
            }
        }
    }

    public void ButtonBuilding()
    {
        buildMode = true;
    }

    /// <summary>
    /// Ставит куб туда, где находится мышка (при этом на определенню клетку)
    /// </summary>
    private void Placement()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            cube.transform.position = new Vector3(
                (int)hit.point.x,
                hit.point.y + 1.6f,
                (int)hit.point.z
                );
        }
    }

    private GameObject CreateCube()
    {
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(4, 3, 4);
        cube.GetComponent<Renderer>().material.color = Color.blue;
        Destroy(cube.GetComponent<BoxCollider>());
        return cube;
    }



    #region Код plcement'a из инета
    //public string[] tags; // массив тегов, объекты которых можно двигать
    //public Camera _camera; // основная камера сцены
    //public enum ProjectMode { Project3D = 0, Project2D = 1 };
    //public ProjectMode mode = ProjectMode.Project3D;
    //public float step = 5; // шаг для изменения высоты в 3D
    //private Transform curObj;
    //private float mass;

    //void Start()
    //{
    //    if (mode == ProjectMode.Project2D) _camera.orthographic = true;
    //}

    //bool GetTag(string curTag)
    //{
    //    bool result = false;
    //    foreach (string t in tags)
    //    {
    //        if (t == curTag) result = true;
    //    }
    //    return result;
    //}

    //void LateUpdate()
    //{
    //    if (Input.GetMouseButton(1)) // Удерживать правую кнопку мыши
    //    {
    //        if (mode == ProjectMode.Project3D)
    //        {
    //            RaycastHit hit;
    //            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
    //            if (Physics.Raycast(ray, out hit))
    //            {
    //                if (GetTag(hit.transform.tag) && hit.rigidbody && !curObj)
    //                {
    //                    curObj = hit.transform;
    //                    mass = curObj.GetComponent<Rigidbody>().mass; // запоминаем массу объекта
    //                    curObj.GetComponent<Rigidbody>().mass = 0.0001f; // убираем массу, чтобы не сбивать другие объекты
    //                    curObj.GetComponent<Rigidbody>().useGravity = false; // убираем гравитацию
    //                    curObj.GetComponent<Rigidbody>().freezeRotation = true; // заморозка вращения
    //                    curObj.position += new Vector3(0, 0.5f, 0); // немного приподымаем выбранный объект
    //                }
    //            }

    //            if (curObj)
    //            {
    //                Vector3 mousePosition = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camera.transform.position.y));
    //                curObj.GetComponent<Rigidbody>().MovePosition(new Vector3(mousePosition.x, curObj.position.y + Input.GetAxis("Mouse ScrollWheel") * step, mousePosition.z));

    //            }
    //        }
    //        else
    //        {
    //            RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

    //        }
    //    }
    //}
    #endregion
}