#if UNITY_EDITOR
using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace BGD.Editor
{
    public class TestEditor : EditorWindow
    {
        private bool TestToggle;
        private int intVar;
        private Number TestEnum;
        private bool boolVar;
        public enum Number
        {
            One,Two, Three
        }


        [MenuItem("Custom/EditorWindow")] //해당 버튼을 누르면 Init 함수가 실행
        private static void Init()
        {
            //Window 생성()안에 string으로 글자를 넣으면 창에 글자를 원하는 대로 바꿀 수 있다.
            //()에 string을 넣지 않으면 클래스 이름이 뜬다.
            TestEditor editorWindow = GetWindow<TestEditor>("BGDTestEditor");
            editorWindow.minSize = new Vector2(700, 500);
            editorWindow.Show(); //Window 열기
        }

        

        private void OnGUI()
        {
            intVar = GUILayout.Toolbar(intVar, System.Enum.GetNames(typeof(Number)));

            switch (intVar)
            {
                case 0:
                    EditorGUILayout.LabelField("Test1");
                    TestToggle = EditorGUILayout.Toggle("ToggleTest", TestToggle);
                    boolVar = EditorGUILayout.Foldout(boolVar, "My FoldOut", true);//글자를 클릭해도 토글될 것인가
                    if(boolVar)
                    {
                        ++EditorGUI.indentLevel;
                        GUILayout.BeginVertical();
                        GUILayout.Label("Test1");
                        GUILayout.Label("Test2");
                        GUILayout.Label("Test3");
                        GUILayout.EndVertical();
                        --EditorGUI.indentLevel;
                    }
                    EditorGUILayout.LabelField("Toggle");
                        
                    break;
                case 1:
                    EditorGUILayout.LabelField("Test2");
                    GUILayout.Button("ButtonTest");
                    break;
                case 2:
                    EditorGUILayout.LabelField("Test3");
                    TestEnum = (Number)EditorGUILayout.EnumPopup("My Enum", TestEnum);
                    break;
            }

        }
    }
}
#endif
