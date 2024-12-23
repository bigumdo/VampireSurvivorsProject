#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace BGD.Editor
{
    public class TestEditor : EditorWindow
    {
        private GameObject gameObjectVar;

        private int intVar;
        private float floatVar;
        private Vector2 vector2Var;
        private Vector3 vector3Var;
        private Color colorVar;

        [MenuItem("Custom/EditorWindow")] //�ش� ��ư�� ������ Init �Լ��� ����
        private static void Init()
        {
            TestEditor editorWindow = (TestEditor)GetWindow(typeof(TestEditor)); //Window ����
            editorWindow.Show(); //Window ����
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("My Label");
            EditorGUILayout.LabelField("My BoldLabel", EditorStyles.boldLabel);

            gameObjectVar = (GameObject)EditorGUILayout.ObjectField("My GameObject"
                , gameObjectVar, typeof(GameObject), true);

            intVar = EditorGUILayout.IntField("My Int", intVar);
            floatVar = EditorGUILayout.FloatField("My Float", floatVar);
            vector2Var = EditorGUILayout.Vector2Field("My Vector2", vector2Var);
            vector3Var = EditorGUILayout.Vector3Field("My Vector3", vector3Var);
            colorVar = EditorGUILayout.ColorField("My Color", colorVar);
        }
    }
}
#endif
