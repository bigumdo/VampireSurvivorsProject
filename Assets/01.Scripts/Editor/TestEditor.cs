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
            PowerUp, //�켭ó�� ������ ���׷��̵�
            Effect, //�ش� ������ ����Ʈ
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

        [MenuItem("Custom/EditorWindow")] //�ش� ��ư�� ������ Init �Լ��� ����
        private static void Init()
        {
            //Window ����()�ȿ� string���� ���ڸ� ������ â�� ���ڸ� ���ϴ� ��� �ٲ� �� �ִ�.
            //()�� string�� ���� ������ Ŭ���� �̸��� ���.
            TestEditor editorWindow = GetWindow<TestEditor>("BGDTestEditor");
            editorWindow.minSize = new Vector2(700, 500);//�ּ� ����� �����Ѵ�.
            editorWindow.Show(); //Window ����
        }

        private T CreateAssetTable<T>(string path) where T : ScriptableObject
        {
            //AssetDatabase.LoadAssetAtPath ���ϴ� Ÿ�԰� ��θ� �־ ���ϴ� ������ �ε��Ѵ�.
            T table = AssetDatabase.LoadAssetAtPath<T>($"{path}/table.asset");

            //���� �ε� ���� �ʾҴٸ� 
            if (table == null)
            {
                // T Ÿ�Կ� ScriptableObject��ü�� ����ϴ�.
                table = ScriptableObject.CreateInstance<T>();

                //��θ� �ָ� ��ο� ������ �ִ��� Ȯ���ϰ� �ִٸ� ��1�������� ���ذ��鼭 ������ ���������� 1�������� ��� ��ȯ
                string fileName = AssetDatabase.GenerateUniqueAssetPath($"{path}/table.asset");
                //table������ fileName�̶�� ��ο� �����.
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
            _selectTexture = new Texture2D(1, 1); //1�ȼ�¥�� �ؽ��� �׸�
            _selectTexture.SetPixel(0, 0, new Color(0.31f, 0.40f, 0.50f));
            _selectTexture.Apply();

            _selectStyle = new GUIStyle();
            _selectStyle.normal.background = _selectTexture;

            //���忡�� �����ϱ� ���� ���
            _selectTexture.hideFlags = HideFlags.DontSave;

            _toolbarItemNames = Enum.GetNames(typeof(UtilType));

            //Dictionary�⺻ ����
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
            //��ܿ� �޴� 2���� ������.
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

            GUI.color = Color.white; //���� �������� ����.

            EditorGUILayout.BeginHorizontal();
            {

                //���� Ǯ����Ʈ ��ºκ�
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

                            //���� �׸� item�� ���þ����۰� �����ϸ� ��Ÿ������
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
                                        //_poolTable.datas ���⼭ �ش��ϴ� �༮�� �����ؾ���
                                        _poolTable.datas.Remove(item);
                                        //Assetdatabase.DeleteAsset����� �̿��ؼ� ������ SO�� �����ؾ���
                                        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(item));
                                        
                                        // _poolTable �����ٰ� �̾߱������ ��
                                        EditorUtility.SetDirty(_poolTable);
                                        // SaveAsset�� ���ؼ� �������ָ� ��.
                                        AssetDatabase.SaveAssets();
                                    }
                                    GUI.color = Color.white;
                                }
                                EditorGUILayout.EndVertical();
                            }
                            EditorGUILayout.EndHorizontal();

                            //���������� �׸� �簢�� ������ �˾ƿ´�.
                            Rect lastRect = GUILayoutUtility.GetLastRect();
                            //���� �����ӿ� ó�� ���� �̺�Ʈ�� Type�� MouseDown�̸鼭
                            //������ �׷��� UI ���� ���콺�� �ִٸ�
                            if (Event.current.type == EventType.MouseDown
                                && lastRect.Contains(Event.current.mousePosition))
                            {
                                //inspectorScroll = Vector2.zero;
                                selectedItem[UtilType.Pool] = item;
                                //�ߺ� ó���� ���� �ٸ� UI���� ó������ �ʵ��� ����
                                Event.current.Use();
                            }

                            //�����Ȱ� Ȯ���ϸ� break�� �ɾ��ָ� ��.
                            if (item == null)
                                break;

                        }
                        //end of foreach

                    }
                    EditorGUILayout.EndScrollView();

                }
                EditorGUILayout.EndVertical();

                //�ν����͸� �׷���� ��.
                if (selectedItem[UtilType.Pool] != null)
                {
                    inspectorScroll = EditorGUILayout.BeginScrollView(inspectorScroll);
                    {
                        //selectedItem[UtilType.Pool]�� �����͸� �̹� ����� Editor�� ĳ���Ͽ� ��Ȱ��
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
            Guid guid = Guid.NewGuid(); //������ ���ڿ� Ű�� ��ȯ��

            PoolingItemSO item = CreateInstance<PoolingItemSO>(); //�̰� �޸𸮿��� �����Ѱž�.
            item.enumName = guid.ToString();

            //������ �������� �����߰� ����Ʈ�� �����߾�.
            AssetDatabase.CreateAsset(item, $"{_poolDirectory}/Pool_{item.enumName}.asset");
            _poolTable.datas.Add(item);

            EditorUtility.SetDirty(_poolTable);  //�� ���̺� ������ �Ͼ���� �˷���� ��
            AssetDatabase.SaveAssets(); //����� �͵��� �ν��ؼ� ������ �Ѵ�.
        }

        private void GenerateEnumFile()
        {
            //Enumstring�� �߰��ϱ� ����StringBuilder�� Ŭ������ ����Ѵ�.
            StringBuilder codeBuilder = new StringBuilder();

            //Pooling�� ��ŭ �ݺ��ؼ� 
            foreach (PoolingItemSO item in _poolTable.datas)
            {
                //enumName�� ���ϰ�,�� �߰��ؼ� ������ ���̴�.
                codeBuilder.Append(item.enumName);
                codeBuilder.Append(",");
            }
            //string.Format�� ù�� ° ���ڿ� {0}{1}�� ���� ǥ�õ� �ڸ��� ���� ������� �ִ´�.
            string code = string.Format(CodeFormat.PoolingTypeFormat, codeBuilder.ToString());

            //Asstes���� ������ �ּҸ�  �����ϱ� ���� Application.dataPath�� ����Ѵ�.
            string path = $"{Application.dataPath}/01.Scripts/Core/ObjectPool/PoolingType.cs";

            //File.WriteAllText�� path�� code�� �Է��Ѵ�.
            File.WriteAllText(path, code);
            AssetDatabase.Refresh(); //�ٽ� ������ ����
        }
    }
}
#endif
