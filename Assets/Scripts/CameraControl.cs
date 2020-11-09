using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform camPos;

    private float horizontalMove;
    private float verticalMove;
    private float scale = 15f;
    private float rotation;

    #region Rotation with mouse
    // Код отсюда: https://youtu.be/jNvmp4SZj9c
    
    private float _mouseSensitivity = 0.1f;
    private Vector3 _mousePreveousePos;
    private float _rotationX;
    private float _rotationY;

    /// <summary>
    /// Вращение камеры с помощью ПКМ. Код отсюда: https://youtu.be/jNvmp4SZj9c.
    /// </summary>
    void CustomRotation()
    {
        Vector3 _mouseDelta;

        if (Input.GetMouseButtonDown(1))
        {
            _mousePreveousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            _mouseDelta = Input.mousePosition - _mousePreveousePos;
            _mousePreveousePos = Input.mousePosition;

            _rotationX -= _mouseDelta.y * _mouseSensitivity;
            _rotationY += _mouseDelta.x * _mouseSensitivity;

            transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0f);
        }
    }
    #endregion

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
        if (subY > 50 || subY < 14)
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
