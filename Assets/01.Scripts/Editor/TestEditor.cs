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
        private Vector2 test;

        private readonly string _poolDirectory = "Assets/08.SO/ObjectPool";
        private PoolingTableSO _poolTable;

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

            GUILayout.BeginVertical(EditorStyles.helpBox,GUILayout.Width(300f));
            EditorGUILayout.LabelField("Effect list");
            EditorGUILayout.Space(3f);

            //BeginScrollView(ScrollView������, ���θ� �׻� Ȱ��ȭ �� ������, ���θ� �׻� Ȱ��ȭ �� ������
            //, Horizontal����, Vertical����,���̾ƿ�����)
            EditorGUILayout.BeginScrollView(test,false, true
                ,GUIStyle.none, GUI.skin.verticalScrollbar,GUIStyle.none);

            EditorGUILayout.EndScrollView();

            //scrollPositions[UtilType.Effect] = EditorGUILayout.BeginScrollView(
            //    scrollPositions[UtilType.Effect],
            //    false, true, GUIStyle.none, GUI.skin.verticalScrollbar, GUIStyle.none);
            GUILayout.EndVertical();
        }
    }
}
#endif
