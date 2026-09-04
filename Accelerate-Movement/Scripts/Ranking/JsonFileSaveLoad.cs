using System.IO;
using UnityEngine;

namespace sugi
{
    public static class RankingDataToJson
    {
        private static string _path = Path.Combine(
                Application.persistentDataPath,
                "Ranking.json"
            );

# if UNITY_EDITOR
        public static string RankingPath => _path;
# endif

        /// <summary>
        /// セーブする
        /// </summary>
        /// <param name="saveData">セーブするデータ</param>
        public static void Save(RankingData saveData)
        {
            string json = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(_path, json);
        } 

        /// <summary>
        /// ロードするif文で行う
        /// </summary>
        /// <param name="data">ロードしたデータ</param>
        /// <returns></returns>
        public static bool TryLoad(out RankingData data)
        {
            data = default;

            if (!File.Exists(_path))
            {
                return false;
            }
            var loadText = File.ReadAllText(_path);
            data =  JsonUtility.FromJson<RankingData>(loadText);
            return data != null;
        }

    # if UNITY_EDITOR
        public static void Reset()
        {
            string json = JsonUtility.ToJson(null);
            File.WriteAllText(_path, json);
        }
    # endif
    }
}