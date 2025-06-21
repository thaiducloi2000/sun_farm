using System;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Score;

[CreateAssetMenu(fileName = "ItemType Config", menuName = "Configs/ItemTypeConfig")]
public class ItemTypeConfig : SerializedScriptableObject
{
    private ItemTypeData[] Data;

    public void Load()
    {
#if UNITY_EDITOR
        List<string> ids = new();
        foreach (var data in Data)
        {
            {
                ids.Add(data.item_type);
            }
        }

        string outputPath = Path.Combine(Application.dataPath, GameDefine.Path.ItemType_Path);
        GameDefine.EnumDefine.ItemType.GenerateEnum(ids, outputPath);
        AssetDatabase.Refresh();
        UnityEditor.Compilation.CompilationPipeline.RequestScriptCompilation();
#endif
    }
}

[Serializable]
public class ItemTypeData
{
    public string item_type;
}