using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class VirtualPivot : EditorWindow
{
    private GameObject before_gameObject;
    private GameObject after_gameObject;
    private Vector3 vec3;

    private string text = null;

    [MenuItem("Minho/VirtualPivot")]
    static void Init()
    {
        var window = GetWindow(typeof(VirtualPivot));
        window.Show();
    }
    private void OnGUI()
    {
        text = EditorGUILayout.TextField("Name", text);

        GUILayout.Label("before_gameObject : 이동 위치");
        before_gameObject = (GameObject)EditorGUILayout.ObjectField(before_gameObject, typeof(GameObject), true);
        GUILayout.Label("after_gameObject  : 원하는 위치");
        after_gameObject = (GameObject)EditorGUILayout.ObjectField(after_gameObject, typeof(GameObject), true);


        if (GUILayout.Button("RUN"))
        {
            if (text == null || text == "")
                Debug.LogError("VirtualPivot : name is NULL");
            if(before_gameObject == null)
                Debug.LogError("VirtualPivot : before_gameObject is NULL");
            if(after_gameObject == null)
                Debug.LogError("VirtualPivot : after_gameObject is NULL");
            if(before_gameObject != null && after_gameObject != null && text != null && text != "")
                GoVirtualPivot();
        }
    }

    public void GoVirtualPivot()
    {
        VirtualPivotData _virtualPivotData = ScriptableObject.CreateInstance<VirtualPivotData>();
        vec3 = before_gameObject.transform.position - after_gameObject.transform.position;
        _virtualPivotData.name = text;
        _virtualPivotData.position = vec3 * -1;
        Debug.Log(text);
        AssetDatabase.CreateAsset(_virtualPivotData, "Assets/Resources/" + text + ".asset");
    }
}


public class VirtualPivotData : ScriptableObject
{
    public string name;
    public Vector3 position;
}
