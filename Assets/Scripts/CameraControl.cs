using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform camPos;

    private float horizontalMove;
    private float verticalMove;
    private float scale = 15f;
    private float rotation;

    #region XYZ
    private float X;
    private float Y;
    private float Z;
    #endregion

    private void Start()
    {
        // rotation по иксу в радианах (нужно для синусов  в методе ApproximationCalculating)
        rotation = camPos.rotation.eulerAngles.x * Mathf.PI / 180;
    }

    private void Update()
    {
        GetCoordinateAxis();
        CameraMove();
        camPos.position = new Vector3(X, Y, Z);

        ApproximationCalculating();
        SetScale();
        camPos.position = new Vector3(X, Y, Z);        
    }

    private void CameraMove()
    {
        horizontalMove = Input.GetAxis("HorizontalMovingCamera");
        X += horizontalMove * scale * Time.deltaTime;
        verticalMove = Input.GetAxis("VerticalMovingCamera");
        Z += verticalMove * scale * Time.deltaTime;
    }

    /// <summary>
    /// Высчитывает отдаление камеры при скролле колесиком мышки и задает Scale.
    /// </summary>
    private void ApproximationCalculating()
    {
        float approximation = -1 * Input.mouseScrollDelta.y;
        float subY = Y + approximation * Mathf.Sin(rotation) * 5;
        float subZ = Z - approximation * Mathf.Cos(rotation) * 5;
        if (subY > 80 || subY < 25)
            return;
        else
        {
            Y = subY;
            Z = subZ;
        }
    }

    /// <summary>
    /// Функция, которая вычисляет скорость передвижения камеры в зависиомсти от ее отдаления.
    /// При большом отдалении скорость камеры увеличивается.
    /// </summary>
    private void SetScale()
    {
        float n = Y - 25; // то, насколько увеличилось отдаление
        n *= 5f / 11f; // то, насколько надо увеличить scale относительно первоначального значения (15f)
        scale = 15 + n;
    }

    /// <summary>
    /// Задает полям X, Y, Z соовтетствующие координаты.
    /// </summary>
    private void GetCoordinateAxis()
    {
        X = camPos.position.x;
        Y = camPos.position.y;
        Z = camPos.position.z;
    }   
}
