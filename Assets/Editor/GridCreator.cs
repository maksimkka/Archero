using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridCreator : EditorWindow
{
    private int rowCount = 3;
    private int columnCount = 3;
    private GameObject objectPrefab;
    private GameObject parentObject;

    [MenuItem("Window/Grid/Object Grid Creator")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(GridCreator));
    }

    private void OnGUI()
    {
        GUILayout.Label("Object Grid Creator", EditorStyles.boldLabel);

        objectPrefab = EditorGUILayout.ObjectField("Object Prefab", objectPrefab, typeof(GameObject), false) as GameObject;
        parentObject = EditorGUILayout.ObjectField("Parent Object", parentObject, typeof(GameObject), true) as GameObject;
        rowCount = EditorGUILayout.IntField("Row Count", rowCount);
        columnCount = EditorGUILayout.IntField("Column Count", columnCount);

        if (GUILayout.Button("Create Grid"))
        {
            CreateGrid();
        }
    }

    private void CreateGrid()
    {
        if (objectPrefab == null)
        {
            Debug.LogError("Please assign an object prefab.");
            return;
        }

        for (int row = 0; row < rowCount; row++)
        {
            for (int column = 0; column < columnCount; column++)
            {
                //var newObject = PrefabUtility.InstantiatePrefab(objectPrefab, parentObject.transform) as GameObject;
                var newObject = PrefabUtility.InstantiatePrefab(objectPrefab) as GameObject;
                newObject.transform.position = new Vector3( parentObject.transform.position.x + row,
                    0f,
                    parentObject.transform.position.z + column);
                newObject.transform.parent = parentObject.transform;
                Undo.RegisterCreatedObjectUndo(newObject, "Create Grid");
            }
        }
    }
}
