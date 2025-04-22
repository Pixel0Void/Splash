using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static void SetOrthographicSize(int size)
    {
        Camera.main.orthographicSize = size;
    }
}
