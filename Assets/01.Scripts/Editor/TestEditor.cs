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

        [MenuItem("Custom/EditorWindow")] //해당 버튼을 누르면 Init 함수가 실행
        private static void Init()
        {
            TestEditor editorWindow = (TestEditor)GetWindow(typeof(TestEditor)); //Window 생성
            editorWindow.Show(); //Window 열기
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
