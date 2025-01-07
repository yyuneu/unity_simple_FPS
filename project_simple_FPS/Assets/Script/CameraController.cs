using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static CameraController instance;
    public static CameraController Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance)
        {
            Destroy(instance);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        transform.position = PlayerController.Instance.eyes.transform.position;

        Quaternion xQuat, yQuat;
        xQuat = InputManager.Instance.xQuat;
        yQuat = InputManager.Instance.yQuat;

        transform.localRotation = xQuat * yQuat;
    }
}