#if UNITY_EDITOR
using BGD.ObjectPooling;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BGD.Editors
{
    public class TestEditor : EditorWindow
    {
        public enum UtilType
        {
            Pool,
            PowerUp, //뱀서처럼 아이템 업그레이드
            Effect, //해당 아이템 이펙트
        }

        private static Dictionary<UtilType, Vector2> scrollPositions
        = new Dictionary<UtilType, Vector2>();
        private static Dictionary<UtilType, Object> selectedItem
            = new Dictionary<UtilType, Object>();

        private Texture2D _selectTexture;
        private string[] _toolbarItemNames;

        private static Vector2 inspectorScroll = Vector2.zero;
        private Editor _cachedEditor;

        private GUIStyle _selectStyle;

        private readonly string _poolDirectory = "Assets/08.SO/ObjectPool";
        private PoolingTableSO _poolTable;

        [MenuItem("Custom/EditorWindow")] //해당 버튼을 누르면 Init 함수가 실행
        private static void Init()
        {
            //Window 생성()안에 string으로 글자를 넣으면 창에 글자를 원하는 대로 바꿀 수 있다.
            //()에 string을 넣지 않으면 클래스 이름이 뜬다.
            TestEditor editorWindow = GetWindow<TestEditor>("BGDTestEditor");
            editorWindow.minSize = new Vector2(700, 500);//최소 사이즈를 지정한다.
            editorWindow.Show(); //Window 열기
        }

        private T CreateAssetTable<T>(string path) where T : ScriptableObject
        {
            //AssetDatabase.LoadAssetAtPath 원하는 타입과 경로를 넣어서 원하는 에셋을 로드한다.
            T table = AssetDatabase.LoadAssetAtPath<T>($"{path}/table.asset");

            //만약 로드 되지 않았다면 
            if (table == null)
            {
                // T 타입에 ScriptableObject객체를 만듭니다.
                table = ScriptableObject.CreateInstance<T>();

                //경로를 주면 경로에 에셋이 있는지 확인하고 있다면 숫1에서부터 더해가면서 파일이 없을때까지 1씩증가한 경로 반환
                string fileName = AssetDatabase.GenerateUniqueAssetPath($"{path}/table.asset");
                //table에셋을 fileName이라는 경로에 만든다.
                AssetDatabase.CreateAsset(table, fileName);
                Debug.Log($"{typeof(T).Name} Table Created At : {fileName}");
            }
            return table;
        }

        private void OnGUI()
        {
            DrawPoolItems();
        }

        private void OnEnable()
        {
            SetUpUtility();
        }
        private void SetUpUtility()
        {
            _selectTexture = new Texture2D(1, 1); //1픽셀짜리 텍스쳐 그림
            _selectTexture.SetPixel(0, 0, new Color(0.31f, 0.40f, 0.50f));
            _selectTexture.Apply();

            _selectStyle = new GUIStyle();
            _selectStyle.normal.background = _selectTexture;

            //빌드에서 제외하기 위해 사용
            _selectTexture.hideFlags = HideFlags.DontSave;

            _toolbarItemNames = Enum.GetNames(typeof(UtilType));

            //Dictionary기본 설정
            foreach (UtilType type in Enum.GetValues(typeof(UtilType)))
            {
                if (scrollPositions.ContainsKey(type) == false)
                    scrollPositions[type] = Vector2.zero;

                if (selectedItem.ContainsKey(type) == false)
                    selectedItem[type] = null;
            }

            if (_poolTable == null)
            {
                _poolTable = CreateAssetTable<PoolingTableSO>(_poolDirectory);
            }
            //if (_powerUpTable == null)
            //{
            //    _powerUpTable = CreateAssetTable<PowerUpListSO>(_powerUpDirectory);
            //}
            //if (_effectTable == null)
            //{
            //    _effectTable = CreateAssetTable<PowerUpEffectListSO>(_effectDirectory);
            //}
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        private void OnDisable()
        {
            DestroyImmediate(_cachedEditor);
            //DestroyImmediate(_selectTexture);
        }

        private void DrawPoolItems()
        {
            //상단에 메뉴 2개를 만들자.
            EditorGUILayout.BeginHorizontal();
            {
                GUI.color = new Color(0.19f, 0.76f, 0.08f);
                if (GUILayout.Button("Generate Item"))
                {
                    GeneratePoolItem();
                }

                GUI.color = new Color(0.81f, 0.13f, 0.18f);
                if (GUILayout.Button("Generate enum file"))
                {
                    GenerateEnumFile();
                }
            }
            EditorGUILayout.EndHorizontal();

            GUI.color = Color.white; //원래 색상으로 복귀.

            EditorGUILayout.BeginHorizontal();
            {

                //왼쪽 풀리스트 출력부분
                EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(300f));
                {
                    EditorGUILayout.LabelField("Pooling list");
                    EditorGUILayout.Space(3f);


                    scrollPositions[UtilType.Pool] = EditorGUILayout.BeginScrollView(
                        scrollPositions[UtilType.Pool],
                        false, true, GUIStyle.none, GUI.skin.verticalScrollbar, GUIStyle.none);
                    {

                        foreach (PoolingItemSO item in _poolTable.datas)
                        {

                            //현재 그릴 item이 선택아이템과 동일하면 스타일지정
                            GUIStyle style = selectedItem[UtilType.Pool] == item ?
                                                    _selectStyle : GUIStyle.none;

                            EditorGUILayout.BeginHorizontal(style, GUILayout.Height(40f));
                            {
                                EditorGUILayout.LabelField(item.enumName, GUILayout.Height(40f), GUILayout.Width(240f));

                                EditorGUILayout.BeginVertical();
                                {
                                    EditorGUILayout.Space(10f);
                                    GUI.color = Color.red;
                                    if (GUILayout.Button("X", GUILayout.Width(20f)))
                                    {
                                        //_poolTable.datas 여기서 해당하는 녀석을 삭제해야해
                                        _poolTable.datas.Remove(item);
                                        //Assetdatabase.DeleteAsset기능을 이용해서 완전히 SO도 삭제해야해
                                        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(item));
                                        
                                        // _poolTable 더럽다고 이야기해줘야 해
                                        EditorUtility.SetDirty(_poolTable);
                                        // SaveAsset을 통해서 저장해주면 돼.
                                        AssetDatabase.SaveAssets();
                                    }
                                    GUI.color = Color.white;
                                }
                                EditorGUILayout.EndVertical();
                            }
                            EditorGUILayout.EndHorizontal();

                            //마지막으로 그린 사각형 정보를 알아온다.
                            Rect lastRect = GUILayoutUtility.GetLastRect();
                            //현재 프레임에 처리 중인 이벤트에 Type이 MouseDown이면서
                            //마지막 그려진 UI 위에 마우스가 있다면
                            if (Event.current.type == EventType.MouseDown
                                && lastRect.Contains(Event.current.mousePosition))
                            {
                                //inspectorScroll = Vector2.zero;
                                selectedItem[UtilType.Pool] = item;
                                //중복 처리를 막고 다른 UI에서 처리되지 않도록 방지
                                Event.current.Use();
                            }

                            //삭제된걸 확인하면 break를 걸어주면 돼.
                            if (item == null)
                                break;

                        }
                        //end of foreach

                    }
                    EditorGUILayout.EndScrollView();

                }
                EditorGUILayout.EndVertical();

                //인스펙터를 그려줘야 해.
                if (selectedItem[UtilType.Pool] != null)
                {
                    inspectorScroll = EditorGUILayout.BeginScrollView(inspectorScroll);
                    {
                        //selectedItem[UtilType.Pool]에 에디터를 이미 선언된 Editor에 캐싱하여 재활용
                        EditorGUILayout.Space(2f);
                        Editor.CreateCachedEditor(
                            selectedItem[UtilType.Pool], null, ref _cachedEditor);

                        _cachedEditor.OnInspectorGUI();
                    }
                    EditorGUILayout.EndScrollView();
                }
            }
            EditorGUILayout.EndHorizontal();

        }

        private void GeneratePoolItem()
        {
            Guid guid = Guid.NewGuid(); //고유한 문자열 키를 반환해

            PoolingItemSO item = CreateInstance<PoolingItemSO>(); //이건 메모리에만 생성한거야.
            item.enumName = guid.ToString();

            //실제로 에셋으로 제작했고 리스트도 변경했어.
            AssetDatabase.CreateAsset(item, $"{_poolDirectory}/Pool_{item.enumName}.asset");
            _poolTable.datas.Add(item);

            EditorUtility.SetDirty(_poolTable);  //이 테이블에 변경이 일어났음을 알려줘야 해
            AssetDatabase.SaveAssets(); //변경된 것들을 인식해서 저장을 한다.
        }

        private void GenerateEnumFile()
        {
            //Enumstring을 추가하기 위해StringBuilder를 클래스를 사용한다.
            StringBuilder codeBuilder = new StringBuilder();

            //Pooling할 만큼 반복해서 
            foreach (PoolingItemSO item in _poolTable.datas)
            {
                //enumName을 더하고,를 추가해서 구별할 것이다.
                codeBuilder.Append(item.enumName);
                codeBuilder.Append(",");
            }
            //string.Format은 첫번 째 인자에 {0}{1}과 같은 표시된 자리에 값을 순서대로 넣는다.
            string code = string.Format(CodeFormat.PoolingTypeFormat, codeBuilder.ToString());

            //Asstes폴더 안으로 주소를  설정하기 위해 Application.dataPath를 사용한다.
            string path = $"{Application.dataPath}/01.Scripts/Core/ObjectPool/PoolingType.cs";

            //File.WriteAllText로 path에 code를 입력한다.
            File.WriteAllText(path, code);
            AssetDatabase.Refresh(); //다시 컴파일 시작
        }
    }
}
#endif
