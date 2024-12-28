#if UNITY_EDITOR
using BGD.ObjectPooling;
using System;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace BGD.Editor
{
    public class TestEditor : EditorWindow
    {

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

        private void DrawPoolItems()
        {

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
