using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateScript : EditorWindow
{
    // メニューへのパス
    private const string MENU_PATH = "Assets/Create Script/";
    private const string MENU_PATH_TOOLS = "Tools/Create Script/";

    // タイトル
    private const string HLSL_PATH = "HLSL Script";
    private const string TEXT_PATH = "Text File";

    // 拡張子
    private static string extensionName = "";
    // 新しいスクリプトの名前
    private static string newScriptName = "";

    // HLSL
    [MenuItem(MENU_PATH + HLSL_PATH), MenuItem(MENU_PATH_TOOLS + HLSL_PATH)]
    private static void CreateHLSL()
    {
        string hlsl = "hlsl";
        ShowWindow(hlsl);
    }

    // TEXT
    [MenuItem(MENU_PATH + TEXT_PATH), MenuItem(MENU_PATH_TOOLS + TEXT_PATH)]
    private static void CreateText()
    {
        string text = "txt";
        ShowWindow(text);
    }

    // Window
    private static void ShowWindow(string _extensionName)
    {
        extensionName = _extensionName;
        EditorWindow.GetWindow<CreateScript>("Create Script");
    }

    // GUI
    private void OnGUI()
    {
        // 拡張子
        EditorGUILayout.LabelField("Extension : " + extensionName);
        GUILayout.Space(10);
        // 名前
        GUILayout.Label("New Script Name");
        newScriptName = GUILayout.TextField(newScriptName);
        GUILayout.Space(30);

        // 閉じる
        if(GUILayout.Button("Create"))
        {
            if(Create())
            {
                this.Close();
            }
        }
    }

    // スクリプトを作成する
    private static bool Create()
    {
        // Error : 名前がない
        if(string.IsNullOrEmpty(newScriptName))
        {
            Debug.Log("Script Name is Blank");
            return false;
        }

        // Error : 名前かぶり、拡張子不明
        string directoryPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        if(!string.IsNullOrEmpty(directoryPath))
        {
            string exportPath = directoryPath + "/" + newScriptName + "." + extensionName;
            if(System.IO.File.Exists(exportPath))
            {
                Debug.Log(exportPath + " already exists");
                return false;
            }
            File.WriteAllText(exportPath,"");
        }
        else
        {
            
            if(extensionName == "txt")
            {
                directoryPath = EditorUtility.SaveFilePanelInProject("Create Text File", newScriptName, extensionName, "", directoryPath);
            }
            else if(extensionName == "hlsl")
            {
                directoryPath = EditorUtility.SaveFilePanelInProject("Create HLSL Script", newScriptName, extensionName, "", directoryPath);
            }

            if(!string.IsNullOrEmpty(directoryPath))
            {
                directoryPath = AssetDatabase.GenerateUniqueAssetPath(directoryPath);
                File.WriteAllText(directoryPath,"");
            }
        }

        // リリース
        AssetDatabase.Refresh();

        return true;
    }
}