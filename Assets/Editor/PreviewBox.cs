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
    private GUIStyle _style;
    private Material _material;

    private bool _isUseBigDescription;
    private bool _isMainWindow;

    private float _ratioFontAndScreen;

    public bool IsMainWindow
    {
        set => _isMainWindow = value;
    }
    public bool IsUseBigDescription
    {
        set => _isUseBigDescription = value;
    }

    public DataMenuLevel MenuLevel
    {
        set => _menuLevel = value;
    }

    public PreviewBox()
    {
        _isMainWindow = true;
        _style = new GUIStyle();
        
        _material = new Material(Shader.Find("Sprites/Default"));
        _material.color = Color.white;
    }

    public void Show()
    {
        _ratioFontAndScreen = Screen.height / 634f;
        DrawImageBackground();
        DrawColorStartButton();
        if (_isMainWindow)
        {
            DrawImageAboveName();
            DrawImageUp();
            DrawImageLeft();
            DrawImageRight();

            DrawTextName();
            DrawTextDescription();
            if (_isUseBigDescription)
            {
                DrawButtonMoreInfo();
            }
        }
        else
        {
            DrawTextBigDescription();
            DrawColorBackButton();
            DrawTextBackButton();
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
    private void DrawColorBackButton()
    {
        Rect rect = new Rect(315 + GetSizeWidth(1f), 10 + GetSizeHeight(28f)
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

        _style.font = _menuLevel.fontName;
        _style.fontSize = (int)(_menuLevel.sizeFontName * _ratioFontAndScreen);
        _style.alignment = TextAnchor.MiddleLeft;
        _style.normal.textColor = color;

        EditorGUI.LabelField(rect, _menuLevel.name, _style);
    }

    private void DrawTextDescription()
    {
        Rect rect = new Rect(315 + GetSizeWidth(1f), 10 + GetSizeHeight(22f)
            , GetSizeWidth(27f), GetSizeHeight(11f));
        Color color = Color.white;
        color.a = _menuLevel.alpha;

        _style.font = _menuLevel.fontDes;
        _style.fontSize = (int)(_menuLevel.sizeFontDes * _ratioFontAndScreen);
        _style.alignment = TextAnchor.UpperLeft;
        _style.normal.textColor = color;

        EditorGUI.LabelField(rect, _menuLevel.description, _style);
    }

    private void DrawTextStartButton()
    {
        Rect rect = new Rect(315 + GetSizeWidth(29f), 10 + GetSizeHeight(28f)
            , GetSizeWidth(27f), GetSizeHeight(5f));
        Color color = Color.white;
        color.a = _menuLevel.alpha;

        _style.font = _menuLevel.fontStartBtn;
        _style.fontSize = (int)(_menuLevel.sizeFontStartBtn* _ratioFontAndScreen);
        _style.alignment = TextAnchor.MiddleCenter;
        _style.normal.textColor = color;

        EditorGUI.LabelField(rect, _menuLevel.textButtonStartLevel, _style);
    }
    private void DrawTextBackButton()
    {
        Rect rect = new Rect(315 + GetSizeWidth(1f), 10 + GetSizeHeight(28f)
            , GetSizeWidth(27f), GetSizeHeight(5f));
        Color color = Color.white;
        color.a = _menuLevel.alpha;

        _style.font = _menuLevel.fontStartBtn;
        _style.fontSize = (int)(_menuLevel.sizeFontStartBtn* _ratioFontAndScreen);
        _style.alignment = TextAnchor.MiddleCenter;
        _style.normal.textColor = color;

        EditorGUI.LabelField(rect, "Назад", _style);
    }

    private void DrawTextBigDescription()
    {
        Rect rect = new Rect(315 + GetSizeWidth(4f), 10 + GetSizeHeight(4f)
            , GetSizeWidth(50f), GetSizeHeight(23f));
        Color color = Color.white;
        color.a = _menuLevel.alpha;

        _style.font = _menuLevel.fontBigDes;
        _style.fontSize = (int)(_menuLevel.sizeFontBigDes * _ratioFontAndScreen);
        _style.alignment = TextAnchor.UpperLeft;
        _style.normal.textColor = color;

        EditorGUI.LabelField(rect, _menuLevel.bigDescription, _style);
    }

    private void DrawButtonMoreInfo()
    {
        Rect rect = new Rect(315 + GetSizeWidth(19f), 10 + GetSizeHeight(18f)
            , GetSizeWidth(9f), GetSizeHeight(3f));
        Material material = new Material(_material);
        Color color = _menuLevel.colorStartButton;
        color.a = _menuLevel.alpha * _menuLevel.colorStartButton.a;
        material.color = color;

        EditorGUI.DrawPreviewTexture(rect, Texture2D.whiteTexture, material);
        
        Color colorText = Color.white;
        colorText.a = _menuLevel.alpha;

        _style.font = _menuLevel.fontStartBtn;
        _style.fontSize = 20;
        _style.alignment = TextAnchor.MiddleCenter;
        _style.normal.textColor = colorText;

        EditorGUI.LabelField(rect, "Подробнее", _style);
    }
}