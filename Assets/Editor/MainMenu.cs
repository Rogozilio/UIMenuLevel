using System;
using System.Collections;
using System.Collections.Generic;
using PlasticGui.Help;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class MainMenu : EditorWindow
{
    private List<Box> _listBoxs;

    private string _name = "";
    private string _description = "";
    private string _textButtonStartlevel;

    private Sprite _logo;
    private Sprite _imageAboveName;
    private Sprite _imageUp;
    private Sprite _imageLeft;
    private Sprite _imageRight;
    private Sprite _imageBackground;

    private Color _colorStartButton;
    private Color _colorBackground;

    private Box _boxTexts;
    private Box _boxImages;
    private Box _boxSettings;

    private Font _fontName;
    private Font _fontDes;
    private Font _fontStartBtn;

    private int _sizeFontName = 14;
    private int _sizeFontDes = 14;
    private int _sizeFontStartBtn = 14;
    private float _alphaBackground = 1f;

    [MenuItem("Window/MainMenu")]
    public static MainMenu OpenMainMenuEdit()
    {
        return GetWindow<MainMenu>("Main Menu");
    }

    private void OnEnable()
    {
        _listBoxs = new List<Box>();
        _boxTexts = new Box("Настройка текста");
        _listBoxs.Add(_boxTexts);
        _boxImages = new Box("Настройка изображений");
        _listBoxs.Add(_boxImages);
        _boxSettings = new Box("Общие настройки");
        _listBoxs.Add(_boxSettings);

        _boxTexts.Actions += BoxTexts;
        _boxImages.Actions += BoxImages;
        _boxSettings.Actions += BoxSettings;
    }

    private void OnDisable()
    {
        _boxTexts.Actions -= BoxTexts;
        _boxImages.Actions -= BoxImages;
        _boxSettings.Actions -= BoxSettings;
    }

    private void OnGUI()
    {
        Show();
    }

    private void AutoHeightBoxOnLastRect(Box box)
    {
        Rect lastRect = GUILayoutUtility.GetLastRect();
        if (lastRect.y > 0)
        {
            box.Height = lastRect.y + lastRect.height;
            for (int i = _listBoxs.IndexOf(box); i > -1; i--)
            {
                box.Height -= _listBoxs[i].Rect.y + _listBoxs[i].Rect.height;
            }
        }
    }

    private void BoxTexts()
    {
        GUILayout.Space(10f);
        GUILayout.Label("Название миссии:");
        _name = GUILayout.TextField(_name, GUILayout.MinWidth(120f));

        GUILayout.BeginHorizontal();
        GUILayout.Label("Шрифт:");
        _fontName = (Font) EditorGUILayout
            .ObjectField(_fontName, typeof(Font), false, GUILayout.Width(110f));
        GUILayout.Label("Размер:");
        _sizeFontName = EditorGUILayout.IntField(_sizeFontName);
        GUILayout.EndHorizontal();

        GUILayout.Space(10f);
        GUILayout.Label("Описание:");
        _description = GUILayout.TextArea(_description);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Шрифт:");
        _fontDes = (Font) EditorGUILayout
            .ObjectField(_fontDes, typeof(Font), false, GUILayout.Width(110f));
        GUILayout.Label("Размер:");
        _sizeFontDes = EditorGUILayout.IntField(_sizeFontDes);
        GUILayout.EndHorizontal();

        GUILayout.Space(10f);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Текст кнопки запуска миссии:");
        _colorStartButton = EditorGUILayout.ColorField(_colorStartButton);
        GUILayout.EndHorizontal();
        _textButtonStartlevel =
            GUILayout.TextField(_textButtonStartlevel, GUILayout.MinWidth(120f));

        GUILayout.BeginHorizontal();
        GUILayout.Label("Шрифт:");
        _fontStartBtn = (Font) EditorGUILayout
            .ObjectField(_fontStartBtn, typeof(Font), false, GUILayout.Width(110f));
        GUILayout.Label("Размер:");
        _sizeFontStartBtn = EditorGUILayout.IntField(_sizeFontStartBtn);
        GUILayout.EndHorizontal();
        AutoHeightBoxOnLastRect(_boxTexts);

        GUILayout.Space(10f);
    }

    private void BoxImages()
    {
        GUILayout.Space(10f);
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();

        //GUILayout.Label("Логотип");
        GUILayout.BeginHorizontal();
        GUILayout.Space(10f);
        _logo = (Sprite) EditorGUILayout.ObjectField(_logo, typeof(Sprite), false,
            GUILayout.Height(80f), GUILayout.Width(80f));
        GUILayout.EndHorizontal();
        GUILayout.Space(10f);
        //GUILayout.Label("Изображение над названием");
        GUILayout.BeginHorizontal();
        GUILayout.Space(10f);
        _imageAboveName =
            (Sprite) EditorGUILayout.ObjectField(_imageAboveName, typeof(Sprite), false,
                GUILayout.Height(80f), GUILayout.Width(80f));
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();

        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();
        GUILayout.Space(10f);
        //GUILayout.Label("Изображение сверху");
        _imageUp = (Sprite) EditorGUILayout.ObjectField(_imageUp, typeof(Sprite), false,
            GUILayout.Height(80f), GUILayout.ExpandWidth(true));
        GUILayout.Space(10f);
        GUILayout.EndHorizontal();
        GUILayout.Space(10f);
        GUILayout.BeginHorizontal();
        //GUILayout.Label("Изображение слева");
        GUILayout.Space(10f);
        _imageLeft = (Sprite) EditorGUILayout.ObjectField(_imageLeft, typeof(Sprite), false,
            GUILayout.Height(80f), GUILayout.ExpandWidth(true));
        GUILayout.Space(10f);
        //GUILayout.Label("Изображение справа");
        _imageRight =
            (Sprite) EditorGUILayout.ObjectField(_imageRight, typeof(Sprite), false,
                GUILayout.Height(80f), GUILayout.ExpandWidth(true));
        GUILayout.Space(10f);
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        AutoHeightBoxOnLastRect(_boxImages);

        GUILayout.Space(10f);
    }

    private void BoxSettings()
    {
        GUILayout.Space(10f);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Цвет заднего фона");
        _colorBackground = EditorGUILayout.ColorField(_colorBackground);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Изображение заднего фона");
        _imageBackground =
            (Sprite) EditorGUILayout.ObjectField(_imageBackground, typeof(Sprite), false);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Прозрачность виджета");
        _alphaBackground = EditorGUILayout.Slider(_alphaBackground, 0, 1);
        GUILayout.EndHorizontal();
        AutoHeightBoxOnLastRect(_boxSettings);

        GUILayout.Space(10f);
    }

    private void Show()
    {
        _boxTexts.Show();
        _boxImages.Show();
        _boxSettings.Show();

        if (GUILayout.Button("Создать"))
        {
            CreateMenuLevel();
        }
    }

    private void CreateMenuLevel()
    {
        GameObject menuLevel = null;
        string[] assetNames = AssetDatabase.FindAssets("", new[] {"Assets/Prefabs/Menu"});
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            menuLevel = AssetDatabase.LoadAssetAtPath<GameObject>(SOpath);
            if (menuLevel.name == "MenuLevel")
                break;
        }

        foreach (var component in menuLevel.GetComponentsInChildren<Text>())
        {
            switch (component.name)
            {
                case "Name":
                    component.text = _name;
                    component.font = _fontName;
                    component.fontSize = _sizeFontName;
                    break;
                case "Description":
                    component.text = _description;
                    component.font = _fontDes;
                    component.fontSize = _sizeFontDes;
                    break;
                case "TextStart":
                    component.text = _textButtonStartlevel;
                    component.font = _fontStartBtn;
                    component.fontSize = _sizeFontStartBtn;
                    break;
            }
        }

        foreach (var component in menuLevel.GetComponentsInChildren<Image>())
        {
            switch (component.name)
            {
                case "Logo":
                    component.sprite = _logo;
                    break;
                case "ImageName":
                    component.sprite = _imageAboveName;
                    break;
                case "ImageUp":
                    component.sprite = _imageUp;
                    break;
                case "ImageLeft":
                    component.sprite = _imageLeft;
                    break;
                case "ImageRight":
                    component.sprite = _imageRight;
                    break;
                case "Start":
                    component.color = _colorStartButton;
                    break;
                case "Background":
                    component.sprite = _imageBackground;
                    component.color = _colorBackground;
                    break;
            }
        }

        GameObject menuLevelHow = Instantiate(menuLevel);
        menuLevelHow.name = "MenuLevel";
        
        // 1 - Прозрачность всего виджета?
        // 2 - По поводу следующего окна
    }
}