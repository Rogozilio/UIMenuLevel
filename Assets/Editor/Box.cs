using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box
{
    private string _name;
    private float _height = 300f;
    private Rect _rect;

    public delegate void Action();

    public event Action Actions;

    public float Height
    {
        get => _height;
        set => _height = value;
    }

    public Rect Rect => _rect;

    public Box(string name)
    {
        _name = name;
    }

    public void Show()
    {
        GUILayout.BeginVertical(_name, "window", GUILayout.Width(300f), GUILayout.Height(_height));
        Actions?.Invoke();
        _rect = GUILayoutUtility.GetLastRect();
        GUILayout.EndVertical();
    }
}