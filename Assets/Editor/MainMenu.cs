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
    private List<DataMenuLevel> _dbMenuLevels;
    private DataMenuLevel _valueMenuLevel;
    private PreviewBox _previewBox;

    private Vector2 _scrollPos;

    private string _nameUI = "";

    private Box _boxPreviewMenu;
    private Box _boxMenuLevel;
    private Box _boxTexts;
    private Box _boxImages;
    private Box _boxSettings;

    private int _numberActionMenuLevel = 0;
    private int _widthWindow;
    private int _heightWindow = 800;

    [MenuItem("Window/MainMenu")]
    public static MainMenu OpenMainMenuEdit()
    {
        return GetWindow<MainMenu>("Main Menu");
    }

    private void OnEnable()
    {
        _dbMenuLevels = new List<DataMenuLevel>();

        _listBoxs = new List<Box>();
        _boxPreviewMenu = new Box("Предварительный просмотр меню");
        _listBoxs.Add(_boxPreviewMenu);
        _boxMenuLevel = new Box("Выбор меню уровня");
        _listBoxs.Add(_boxMenuLevel);
        _boxTexts = new Box("Настройка текста");
        _listBoxs.Add(_boxTexts);
        _boxImages = new Box("Настройка изображений");
        _listBoxs.Add(_boxImages);
        _boxSettings = new Box("Общие настройки");
        _listBoxs.Add(_boxSettings);

        _boxPreviewMenu.Actions += BoxPreviewMenu;
        _boxMenuLevel.Actions += BoxMenuLevel;
        _boxTexts.Actions += BoxTexts;
        _boxImages.Actions += BoxImages;
        _boxSettings.Actions += BoxSettings;

        LoadDBMenuLevels();
        if (_dbMenuLevels.Count > 0)
        {
            _numberActionMenuLevel = 0;
        }

        _previewBox = new PreviewBox();
    }

    private void OnDisable()
    {
        _boxPreviewMenu.Actions -= BoxPreviewMenu;
        _boxMenuLevel.Actions -= BoxMenuLevel;
        _boxTexts.Actions -= BoxTexts;
        _boxImages.Actions -= BoxImages;
        _boxSettings.Actions -= BoxSettings;
    }

    private void OnGUI()
    {
        AutoAspectWindow();
        if (_dbMenuLevels.Count > 0)
        {
            _valueMenuLevel = _dbMenuLevels[_numberActionMenuLevel];
        }

        Show();
        if (_dbMenuLevels.Count > 0)
        {
            _dbMenuLevels[_numberActionMenuLevel] = _valueMenuLevel;
            _previewBox.MenuLevel = _valueMenuLevel;
        }
    }

    private void AutoAspectWindow()
    {
        _heightWindow = Screen.height;
        _widthWindow = (int) (_heightWindow * 1.7777778f) + 315;
        minSize = new Vector2(_widthWindow, 500f);
        maxSize = new Vector2(_widthWindow, 700f);
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

    public void BoxPreviewMenu()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(10f);
        if (GUILayout.Button("Главное окно"))
        {
            _previewBox.IsMainWindow = true;
        }

        if (GUILayout.Button("Подробное описание"))
        {
            _previewBox.IsMainWindow = false;
        }

        GUILayout.Space(10f);
        GUILayout.EndHorizontal();

        AutoHeightBoxOnLastRect(_boxPreviewMenu);

        GUILayout.Space(10f);
    }

    private void BoxMenuLevel()
    {
        GUILayout.Space(10f);

        GUILayout.BeginHorizontal();
        GUILayout.Space(10f);
        _nameUI = GUILayout.TextField(_nameUI, GUILayout.Width(120f));
        GUILayout.Space(10f);
        if (GUILayout.Button("Добавить UI"))
        {
            if (_nameUI != "")
            {
                DataMenuLevel newDataMenuLevel = new DataMenuLevel();
                newDataMenuLevel.name = _nameUI;
                newDataMenuLevel.alpha = 1f;
                _dbMenuLevels.Add(newDataMenuLevel);
                _valueMenuLevel = _dbMenuLevels[_numberActionMenuLevel];
            }
        }

        GUILayout.Space(10f);
        GUILayout.EndHorizontal();
        GUILayout.Space(10f);

        for (int i = 0; i < _dbMenuLevels.Count; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10f);
            GUILayout.Label(_dbMenuLevels[i].name, GUILayout.Width(120f));
            GUILayout.Space(10f);
            if (GUILayout.Button("x", GUILayout.Width(30f)))
            {
                _dbMenuLevels.RemoveAt(i);
            }

            GUILayout.Space(10f);
            if (_numberActionMenuLevel != i)
            {
                if (GUILayout.Button("Выбрать"))
                {
                    _numberActionMenuLevel = i;
                    _valueMenuLevel = _dbMenuLevels[_numberActionMenuLevel];
                }
            }

            GUILayout.Space(10f);
            GUILayout.EndHorizontal();
        }

        AutoHeightBoxOnLastRect(_boxMenuLevel);

        GUILayout.Space(10f);
    }

    private void BoxTexts()
    {
        GUILayout.Space(10f);
        GUILayout.BeginHorizontal();
        GUILayout.Space(10f);
        GUILayout.Label("Название миссии:");
        GUILayout.Space(10f);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Space(10f);
        _valueMenuLevel.name = GUILayout.TextField(_valueMenuLevel.name, GUILayout.MinWidth(120f));
        GUILayout.Space(10f);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Space(10f);
        GUILayout.Label("Шрифт:");
        _valueMenuLevel.fontName = (Font) EditorGUILayout
            .ObjectField(_valueMenuLevel.fontName, typeof(Font), false, GUILayout.Width(110f));
        GUILayout.Label("Размер:");
        _valueMenuLevel.sizeFontName = EditorGUILayout.IntField(_valueMenuLevel.sizeFontName);
        GUILayout.Space(10f);
        GUILayout.EndHorizontal();

        GUILayout.Space(10f);

        GUILayout.BeginHorizontal();
        GUILayout.Space(10f);
        GUILayout.Label("Описание:");
        GUILayout.Space(10f);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Space(10f);
        _valueMenuLevel.description = GUILayout.TextArea(_valueMenuLevel.description);
        GUILayout.Space(10f);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Space(10f);
        GUILayout.Label("Шрифт:");
        _valueMenuLevel.fontDes = (Font) EditorGUILayout
            .ObjectField(_valueMenuLevel.fontDes, typeof(Font), false, GUILayout.Width(110f));
        GUILayout.Label("Размер:");
        _valueMenuLevel.sizeFontDes = EditorGUILayout.IntField(_valueMenuLevel.sizeFontDes);
        GUILayout.Space(10f);
        GUILayout.EndHorizontal();

        GUILayout.Space(10f);
        GUILayout.BeginHorizontal();
        GUILayout.Space(10f);
        GUILayout.Label("Текст кнопки запуска миссии:");
        _valueMenuLevel.colorStartButton =
            EditorGUILayout.ColorField(_valueMenuLevel.colorStartButton);
        GUILayout.Space(10f);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Space(10f);
        _valueMenuLevel.textButtonStartLevel =
            GUILayout.TextField(_valueMenuLevel.textButtonStartLevel, GUILayout.MinWidth(120f));
        GUILayout.Space(10f);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Space(10f);
        GUILayout.Label("Шрифт:");
        _valueMenuLevel.fontStartBtn = (Font) EditorGUILayout
            .ObjectField(_valueMenuLevel.fontStartBtn, typeof(Font), false, GUILayout.Width(110f));
        GUILayout.Label("Размер:");
        _valueMenuLevel.sizeFontStartBtn =
            EditorGUILayout.IntField(_valueMenuLevel.sizeFontStartBtn);
        GUILayout.Space(10f);
        GUILayout.EndHorizontal();
        GUILayout.Space(10f);
        GUILayout.BeginHorizontal();
        GUILayout.Space(10f);
        _valueMenuLevel.isUseBigDescription
            = GUILayout.Toggle(_valueMenuLevel.isUseBigDescription,
                "Использовать подробное описание");
        GUILayout.Space(10f);
        GUILayout.EndHorizontal();
        GUILayout.Space(5f);

        if (_valueMenuLevel.isUseBigDescription)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10f);
            _valueMenuLevel.bigDescription
                = GUILayout.TextArea(_valueMenuLevel.bigDescription);
            GUILayout.Space(10f);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Space(10f);
            GUILayout.Label("Шрифт:");
            _valueMenuLevel.fontBigDes = (Font) EditorGUILayout
                .ObjectField(_valueMenuLevel.fontBigDes, typeof(Font), false,
                    GUILayout.Width(110f));
            GUILayout.Label("Размер:");
            _valueMenuLevel.sizeFontBigDes =
                EditorGUILayout.IntField(_valueMenuLevel.sizeFontBigDes);
            GUILayout.Space(10f);
            GUILayout.EndHorizontal();
        }

        AutoHeightBoxOnLastRect(_boxTexts);

        GUILayout.Space(10f);
    }

    private void BoxImages()
    {
        GUILayout.Space(10f);
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();
        GUILayout.Space(10f);
        _valueMenuLevel.logo = (Sprite) EditorGUILayout.ObjectField(_valueMenuLevel.logo,
            typeof(Sprite), false,
            GUILayout.Height(80f), GUILayout.Width(80f));
        GUILayout.EndHorizontal();
        GUILayout.Space(10f);
        GUILayout.BeginHorizontal();
        GUILayout.Space(10f);
        _valueMenuLevel.imageAboveName =
            (Sprite) EditorGUILayout.ObjectField(_valueMenuLevel.imageAboveName, typeof(Sprite),
                false,
                GUILayout.Height(80f), GUILayout.Width(80f));
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();

        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();
        GUILayout.Space(10f);
        _valueMenuLevel.imageUp = (Sprite) EditorGUILayout.ObjectField(_valueMenuLevel.imageUp,
            typeof(Sprite), false,
            GUILayout.Height(80f), GUILayout.ExpandWidth(true));
        GUILayout.Space(10f);
        GUILayout.EndHorizontal();
        GUILayout.Space(10f);
        GUILayout.BeginHorizontal();
        GUILayout.Space(10f);
        _valueMenuLevel.imageLeft = (Sprite) EditorGUILayout.ObjectField(_valueMenuLevel.imageLeft,
            typeof(Sprite), false,
            GUILayout.Height(80f), GUILayout.ExpandWidth(true));
        GUILayout.Space(10f);
        _valueMenuLevel.imageRight =
            (Sprite) EditorGUILayout.ObjectField(_valueMenuLevel.imageRight, typeof(Sprite), false,
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
        GUILayout.Space(10f);
        GUILayout.Label("Цвет заднего фона");
        _valueMenuLevel.colorBackground =
            EditorGUILayout.ColorField(_valueMenuLevel.colorBackground);
        GUILayout.Space(10f);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Space(10f);
        GUILayout.Label("Изображение заднего фона");
        _valueMenuLevel.imageBackground =
            (Sprite) EditorGUILayout.ObjectField(_valueMenuLevel.imageBackground, typeof(Sprite),
                false);
        GUILayout.Space(10f);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Space(10f);
        GUILayout.Label("Прозрачность виджета");
        _valueMenuLevel.alpha =
            EditorGUILayout.Slider(_valueMenuLevel.alpha, 0, 1);
        GUILayout.Space(10f);
        GUILayout.EndHorizontal();

        AutoHeightBoxOnLastRect(_boxSettings);

        GUILayout.Space(10f);
    }

    private void Show()
    {
        _scrollPos = GUILayout.BeginScrollView(_scrollPos
            , GUILayout.Width(315f), GUILayout.Height(Screen.height - 20f));
        _boxPreviewMenu.Show();
        _boxMenuLevel.Show();
        _boxTexts.Show();
        _boxImages.Show();
        _boxSettings.Show();
        GUILayout.EndScrollView();
        _previewBox.Show();
    }

    private void LoadDBMenuLevels()
    {
        DBMenuLevels DBMenuLevels;
        string[] assetNames = AssetDatabase.FindAssets("", new[] {"Assets/Scripts/DBMenuLevels"});
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            DBMenuLevels = (DBMenuLevels) AssetDatabase.LoadAssetAtPath<ScriptableObject>(SOpath);
            if (DBMenuLevels != null && DBMenuLevels.MenuLevels != null)
                _dbMenuLevels = DBMenuLevels.MenuLevels;
        }
    }
}