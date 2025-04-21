using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static int LargeLevelSize = 15;
    public static int SmallLevelSize = 7;

    public static void LargeLevelCameraSetup()
    {
        Camera.main.orthographicSize = LargeLevelSize;
    }

    public static void SmallLevelCameraSetup()
    {
        Camera.main.orthographicSize = SmallLevelSize;
    }
}
