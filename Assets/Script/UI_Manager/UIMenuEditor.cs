using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIMenu))]
public class UIMenuEditor : Editor
{
    private UIMenu uiMenu = null;

    private void OnEnable()
    {
        uiMenu = (UIMenu)target;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Show"))
        {
            uiMenu.Show();
        }

        if (GUILayout.Button("Hide"))
        {
            uiMenu.Hide();
        }

        base.OnInspectorGUI();

    }
}
