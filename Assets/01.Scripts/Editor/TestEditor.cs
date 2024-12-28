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

        private void DrawPoolItems()
        {

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
