using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(DBMenuLevels))]
public class DBMenuLevelsEditor : Editor
{
    private int _numberLevel;
    private List<string> _nameLevels;
    private List<int> _numberLevels;

    private DBMenuLevels _dataMenuLevels;

    private void OnEnable()
    {
        _nameLevels = new List<string>();
        _numberLevels = new List<int>();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        _nameLevels.Clear();
        _numberLevels.Clear();

        _dataMenuLevels = null;
        string[] assetNames = AssetDatabase.FindAssets("", new[] {"Assets/UI/"});
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            _dataMenuLevels =
                (DBMenuLevels) AssetDatabase.LoadAssetAtPath<ScriptableObject>(SOpath);
            ;
        }

        GUILayout.Space(30f);

        if (_dataMenuLevels != null
            && _dataMenuLevels.MenuLevels.Count > 0)
        {
            for (int i = 0; i < _dataMenuLevels.MenuLevels.Count; i++)
            {
                _nameLevels.Add(_dataMenuLevels.MenuLevels[i].name);
                _numberLevels.Add(i);
            }

            _numberLevel = EditorGUILayout.IntPopup("Уровень", _numberLevel
                , _nameLevels.ToArray(), _numberLevels.ToArray());
        }

        GUILayout.Space(10f);
        if (GUILayout.Button("Создать UI"))
        {
            _dataMenuLevels.CreateMenuLevel(_numberLevel);
        }
        GUILayout.Space(5f);
        if (GUILayout.Button("Обновить UI"))
        {
            _dataMenuLevels.RefreshMenuLevel(_numberLevel);
        }
    }
}