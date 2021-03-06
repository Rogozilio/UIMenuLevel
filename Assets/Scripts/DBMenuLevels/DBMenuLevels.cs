using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[Serializable]
public struct DataMenuLevel
{
    public string name;
    public string description;
    public string textButtonStartLevel;
    public string bigDescription;

    public Sprite logo;
    public Sprite imageAboveName;
    public Sprite imageUp;
    public Sprite imageLeft;
    public Sprite imageRight;
    public Sprite imageBackground;

    public Color colorStartButton;
    public Color colorBackground;

    public Font fontName;
    public Font fontDes;
    public Font fontStartBtn;
    public Font fontBigDes;

    public int sizeFontName;
    public int sizeFontDes;
    public int sizeFontStartBtn;
    public int sizeFontBigDes;
    public float alpha;

    public bool isUseBigDescription;
}

[CreateAssetMenu(menuName = "DBMenuLevels", order = 1), ExecuteInEditMode]
public class DBMenuLevels : ScriptableObject
{
    public List<DataMenuLevel> MenuLevels = new List<DataMenuLevel>();
    #if (UNITY_EDITOR)
    public void CreateMenuLevel(int index)
    {
        var menuLevel = MenuLevels[index];
        GameObject canvasMenuLevel = null;
        string[] assetNames = AssetDatabase.FindAssets("", new[] {"Assets/Prefabs/Menu"});
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            canvasMenuLevel = AssetDatabase.LoadAssetAtPath<GameObject>(SOpath);
            if (canvasMenuLevel.name == "MenuLevel")
                break;
        }
        GameObject menuLevelNow = Instantiate(canvasMenuLevel);
        SetMenuLevel(menuLevelNow, menuLevel);
    }

    public void RefreshMenuLevel(int index)
    {
        GameObject menuLevel = GameObject.Find(MenuLevels[index].name);
        if (menuLevel != null)
        {
            SetMenuLevel(menuLevel, MenuLevels[index]); 
        }
    }

    private void SetMenuLevel(GameObject menuLevelNow, DataMenuLevel menuLevel)
    {
        foreach (var component in menuLevelNow.GetComponentsInChildren<Image>())
        {
            Color color = component.color;
            color.a = menuLevel.alpha;
            switch (component.name)
            {
                case "Logo":
                    component.sprite = menuLevel.logo;
                    component.color = color;
                    break;
                case "ImageName":
                    component.sprite = menuLevel.imageAboveName;
                    component.color = color;
                    break;
                case "ImageUp":
                    component.sprite = menuLevel.imageUp;
                    component.color = color;
                    break;
                case "ImageLeft":
                    component.sprite = menuLevel.imageLeft;
                    component.color = color;
                    break;
                case "ImageRight":
                    component.sprite = menuLevel.imageRight;
                    component.color = color;
                    break;
                case "ButtonStart":
                    color = menuLevel.colorStartButton;
                    color.a = menuLevel.alpha;
                    component.color = color;
                    break;
                case "ButtonBack":
                    color = menuLevel.colorStartButton;
                    color.a = menuLevel.alpha;
                    component.color = color;
                    break;
                case "ButtonNextInfo":
                    color = menuLevel.colorStartButton;
                    color.a = menuLevel.alpha;
                    component.color = color;
                    break;
                case "Background":
                    component.sprite = menuLevel.imageBackground;
                    color = menuLevel.colorBackground;
                    color.a = menuLevel.alpha;
                    component.color = color;
                    break;
            }
        }

        foreach (var component in menuLevelNow.GetComponentsInChildren<Text>())
        {
            Color color = Color.white;
            color.a = menuLevel.alpha;
            switch (component.name)
            {
                case "Name":
                    menuLevelNow.name = menuLevel.name;
                    component.text = menuLevel.name;
                    component.font = menuLevel.fontName;
                    component.fontSize = menuLevel.sizeFontName;
                    component.alignment = TextAnchor.MiddleLeft;
                    component.color = color;
                    break;
                case "Description":
                    component.text = menuLevel.description;
                    component.font = menuLevel.fontDes;
                    component.fontSize = menuLevel.sizeFontDes;
                    component.color = color;
                    break;
                case "TextStart":
                    component.text = menuLevel.textButtonStartLevel;
                    component.font = menuLevel.fontStartBtn;
                    component.fontSize = menuLevel.sizeFontStartBtn;
                    component.color = color;
                    break;
                case "TextBack":
                    component.font = menuLevel.fontStartBtn;
                    component.fontSize = menuLevel.sizeFontStartBtn;
                    component.color = color;
                    component.transform.parent.gameObject.SetActive(false);
                    break;
                case "MoreInfo":
                    component.text = menuLevel.bigDescription;
                    component.font = menuLevel.fontBigDes;
                    component.fontSize = menuLevel.sizeFontBigDes;
                    component.color = color;
                    component.gameObject.SetActive(false);
                    break;
                case "TextMoreInfo":
                    component.font = menuLevel.fontStartBtn;
                    component.color = color;
                    component.gameObject.transform.parent.gameObject.SetActive(false);
                    break;
            }
        }

        menuLevelNow.GetComponentInChildren<EventMenuLevel>()
            .isButtonNextinfoActive = menuLevel.isUseBigDescription;
    }
    #endif
}