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

            GUILayout.BeginVertical(EditorStyles.helpBox,GUILayout.Width(300f));
            EditorGUILayout.LabelField("Effect list");
            EditorGUILayout.Space(3f);

            //BeginScrollView(ScrollView사이즈, 가로를 항상 활성화 할 것인지, 세로를 항상 활성화 할 것인지
            //, Horizontal설정, Vertical설정,레이아웃설정)
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
