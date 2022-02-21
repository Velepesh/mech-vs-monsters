using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public static class CameraSwitcher
{
    static List<CinemachineVirtualCamera> _cameras = new List<CinemachineVirtualCamera>();

    public static CinemachineVirtualCamera ActiveCamera = null;

    public static bool IsActiveCamera(CinemachineVirtualCamera camera)
    {
        return camera == ActiveCamera;
    }

    public static void SwitchCamera(CinemachineVirtualCamera camera)
    {
        camera.Priority = 10;
        ActiveCamera = camera;

        foreach (CinemachineVirtualCamera item in _cameras)
        {
            if (item != camera && item.Priority != 0)
            {
                item.Priority = 0;
            }
        }
    }

    public static void Register(CinemachineVirtualCamera camera)
    {
        _cameras.Add(camera);
    }

    public static void Unregister(CinemachineVirtualCamera camera)
    {
        _cameras.Remove(camera);
    }
}