using System;
using System.Collections;
using System.Collections.Generic;
using PlasticPipe.Server;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PreviewBox
{
    private DataMenuLevel _menuLevel;

    private GUIStyle _styleName = new GUIStyle();
    private GUIStyle _styleDescription = new GUIStyle();
    private GUIStyle _styleStartButton = new GUIStyle();
    private GUIStyle _styleBigDescription = new GUIStyle();

    private Material _material;

    private bool _isMainWindow;

    public bool IsMainWindow
    {
        set => _isMainWindow = value;
    }

    public DataMenuLevel MenuLevel
    {
        get => _menuLevel;
        set => _menuLevel = value;
    }

    public PreviewBox()
    {
        _isMainWindow = true;
        Material material;
        string[] assetNames = AssetDatabase.FindAssets("", new[] {"Assets/Materials"});
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            material = AssetDatabase.LoadAssetAtPath<Material>(SOpath);
            if (material != null)
                _material = material;
        }
    }

    public void Show()
    {
        DrawImageBackground();
        if (_isMainWindow)
        {
            

            DrawImageAboveName();
            DrawImageUp();
            DrawImageLeft();
            DrawImageRight();
            DrawColorStartButton();
            

            DrawTextName();
            DrawTextDescription();
            
        }
        else
        {
            DrawTextBigDescription();
        }
        DrawImageLogo();
        DrawTextStartButton();
    }

    private float GetSizeWidth(float percent)
    {
        return (Screen.width - 315f) / (57f / percent);
    }

    private float GetSizeHeight(float percent)
    {
        return (Screen.height) / (34f / percent);
    }

    private void DrawImageLogo()
    {
        Rect rect = new Rect(315 + GetSizeWidth(11f), 10, GetSizeWidth(11f), GetSizeHeight(3f));
        Material material = new Material(_material);
        Color color = _material.color;
        color.a = _menuLevel.alpha;
        material.color = color;

        if (_menuLevel.logo != null)
            EditorGUI.DrawPreviewTexture(rect, _menuLevel.logo.texture, material);
        else
            EditorGUI.DrawPreviewTexture(rect, Texture2D.normalTexture, material);
    }

    private void DrawImageAboveName()
    {
        Rect rect = new Rect(315 + GetSizeWidth(1f), 10 + GetSizeHeight(1f)
            , GetSizeWidth(27f), GetSizeHeight(16f));
        Material material = new Material(_material);
        Color color = _material.color;
        color.a = _menuLevel.alpha;
        material.color = color;

        if (_menuLevel.imageAboveName != null)
            EditorGUI.DrawPreviewTexture(rect, _menuLevel.imageAboveName.texture, material);
        else
            EditorGUI.DrawPreviewTexture(rect, Texture2D.normalTexture, material);
    }

    private void DrawImageUp()
    {
        Rect rect = new Rect(315 + GetSizeWidth(29f), 10 + GetSizeHeight(1f)
            , GetSizeWidth(27f), GetSizeHeight(16f));
        Material material = new Material(_material);
        Color color = _material.color;
        color.a = _menuLevel.alpha;
        material.color = color;

        if (_menuLevel.imageUp != null)
            EditorGUI.DrawPreviewTexture(rect, _menuLevel.imageUp.texture, material);
        else
            EditorGUI.DrawPreviewTexture(rect, Texture2D.normalTexture, material);
    }

    private void DrawImageLeft()
    {
        Rect rect = new Rect(315 + GetSizeWidth(29f), 10 + GetSizeHeight(18f)
            , GetSizeWidth(13f), GetSizeHeight(9f));
        Material material = new Material(_material);
        Color color = _material.color;
        color.a = _menuLevel.alpha;
        material.color = color;

        if (_menuLevel.imageLeft != null)
            EditorGUI.DrawPreviewTexture(rect, _menuLevel.imageLeft.texture, material);
        else
            EditorGUI.DrawPreviewTexture(rect, Texture2D.normalTexture, material);
    }

    private void DrawImageRight()
    {
        Rect rect = new Rect(315 + GetSizeWidth(43f), 10 + GetSizeHeight(18f)
            , GetSizeWidth(13f), GetSizeHeight(9f));
        Material material = new Material(_material);
        Color color = _material.color;
        color.a = _menuLevel.alpha;
        material.color = color;

        if (_menuLevel.imageRight != null)
            EditorGUI.DrawPreviewTexture(rect, _menuLevel.imageRight.texture, material);
        else
            EditorGUI.DrawPreviewTexture(rect, Texture2D.normalTexture, material);
    }

    private void DrawColorStartButton()
    {
        Rect rect = new Rect(315 + GetSizeWidth(29f), 10 + GetSizeHeight(28f)
            , GetSizeWidth(27f), GetSizeHeight(5f));
        Material material = new Material(_material);
        Color color = _menuLevel.colorStartButton;
        color.a = _menuLevel.alpha * _menuLevel.colorStartButton.a;
        material.color = color;

        EditorGUI.DrawPreviewTexture(rect, Texture2D.whiteTexture, material);
    }

    private void DrawImageBackground()
    {
        Rect rect = new Rect(315 + GetSizeWidth(0f), 10 + GetSizeHeight(0f)
            , GetSizeWidth(57f), GetSizeHeight(34f));
        Material material = new Material(_material);
        Color color = _menuLevel.colorBackground;
        color.a = _menuLevel.alpha;
        material.color = color;

        if (_menuLevel.imageBackground != null)
        {
            EditorGUI.DrawPreviewTexture(rect, _menuLevel.imageBackground.texture, material);
        }
        else
            EditorGUI.DrawPreviewTexture(rect, Texture2D.whiteTexture, material);
    }

    private void DrawTextName()
    {
        Rect rect = new Rect(315 + GetSizeWidth(1f), 10 + GetSizeHeight(18f)
            , GetSizeWidth(27f), GetSizeHeight(3f));
        Color color = Color.white;
        color.a = _menuLevel.alpha;

        _styleName.font = _menuLevel.fontName;
        _styleName.fontSize = _menuLevel.sizeFontName;
        _styleName.normal.textColor = color;

        EditorGUI.LabelField(rect, _menuLevel.name, _styleName);
    }

    private void DrawTextDescription()
    {
        Rect rect = new Rect(315 + GetSizeWidth(1f), 10 + GetSizeHeight(22f)
            , GetSizeWidth(27f), GetSizeHeight(11f));
        Color color = Color.white;
        color.a = _menuLevel.alpha;

        _styleDescription.font = _menuLevel.fontDes;
        _styleDescription.fontSize = _menuLevel.sizeFontDes;
        _styleDescription.alignment = TextAnchor.UpperLeft;
        _styleDescription.normal.textColor = color;

        EditorGUI.LabelField(rect, _menuLevel.description, _styleDescription);
    }

    private void DrawTextStartButton()
    {
        Rect rect = new Rect(315 + GetSizeWidth(29f), 10 + GetSizeHeight(28f)
            , GetSizeWidth(27f), GetSizeHeight(5f));
        Color color = Color.white;
        color.a = _menuLevel.alpha;

        _styleStartButton.font = _menuLevel.fontStartBtn;
        _styleStartButton.fontSize = _menuLevel.sizeFontStartBtn;
        _styleStartButton.alignment = TextAnchor.MiddleCenter;
        _styleStartButton.normal.textColor = color;

        EditorGUI.LabelField(rect, _menuLevel.textButtonStartLevel, _styleStartButton);
    }

    private void DrawTextBigDescription()
    {
        Rect rect = new Rect(315 + GetSizeWidth(4f), 10 + GetSizeHeight(4f)
            , GetSizeWidth(50f), GetSizeHeight(23f));
        Color color = Color.white;
        color.a = _menuLevel.alpha;

        _styleBigDescription.font = _menuLevel.fontBigDes;
        _styleBigDescription.fontSize = _menuLevel.sizeFontBigDes;
        _styleBigDescription.alignment = TextAnchor.UpperLeft;
        _styleBigDescription.normal.textColor = color;

        EditorGUI.LabelField(rect, _menuLevel.bigDescription, _styleBigDescription);
    }
}