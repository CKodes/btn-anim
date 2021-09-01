using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class Backend : MonoBehaviour
{
    public delegate void OnEnterHover();
    public static event OnEnterHover onEntHover;

    public delegate void OnExitHover();
    public static event OnExitHover onExtHover;

    public delegate void OnClickDown();
    public static event OnClickDown onPress;

    public delegate void OnClickRelease();
    public static event OnClickRelease onRelease;

    void OnGUI()
    {
        if (onEntHover != null)
            onEntHover();

        if (onExtHover != null)
            onExtHover();

        if (onPress!= null)
            onPress();

        if (onRelease != null)
            onRelease();
    }
}
