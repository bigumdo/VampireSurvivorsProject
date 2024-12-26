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


        [MenuItem("Custom/EditorWindow")] //�ش� ��ư�� ������ Init �Լ��� ����
        private static void Init()
        {
            //Window ����()�ȿ� string���� ���ڸ� ������ â�� ���ڸ� ���ϴ� ��� �ٲ� �� �ִ�.
            //()�� string�� ���� ������ Ŭ���� �̸��� ���.
            TestEditor editorWindow = GetWindow<TestEditor>("BGDTestEditor");
            editorWindow.minSize = new Vector2(700, 500);
            editorWindow.Show(); //Window ����
        }

        

        private void OnGUI()
        {
            intVar = GUILayout.Toolbar(intVar, System.Enum.GetNames(typeof(Number)));

            switch (intVar)
            {
                case 0:
                    EditorGUILayout.LabelField("Test1");
                    TestToggle = EditorGUILayout.Toggle("ToggleTest", TestToggle);
                    boolVar = EditorGUILayout.Foldout(boolVar, "My FoldOut", true);//���ڸ� Ŭ���ص� ��۵� ���ΰ�
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
