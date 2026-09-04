
namespace sugi
{
    /// <summary>
    /// ランキングのセーブ
    /// </summary>
    public static class RankingSave
    {

        /// <summary>
        /// セーブ
        /// </summary>
        /// <param name="time">タイムを入れる</param>
        public static void Save(float time)
        {
            if(!RankingDataToJson.TryLoad(out RankingData data) || data is null)
            {
                data = new();
            }

            data.SetData(time);
            data.TopTwentyDataSortData();
            RankingDataToJson.Save(saveData: data);
        }
    }
}