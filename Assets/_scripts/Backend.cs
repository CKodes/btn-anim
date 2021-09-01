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

    void OnGUI()
    {
        if (onEntHover != null)
            onEntHover();

        if (onExtHover != null)
            onExtHover();
    }
}
