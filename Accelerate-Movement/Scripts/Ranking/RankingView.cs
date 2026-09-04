using UnityEngine;
using System.Collections.Generic;

namespace sugi
{
    public class RankingView : MonoBehaviour
    {
        [SerializeField] private List<RankingElement> _topTenRankingElements = new();

        /// <summary>
        /// rankingElementをセットする
        /// </summary>
        /// <param name="rd"></param>
        public void SetRankingElements(RankingData rd)
        {
            for(int i = 0; i < rd.TopTwentyData.Count; i++)
            {
                var element = _topTenRankingElements[i];
                var data = rd.TopTwentyData[i];
                bool isLastPerson = 
                    rd.LastTimePerson.Time == data.Time
                    && rd.LastTimePerson.Date == data.Date;

                element.Initialize(
                    data.Time, 
                    data.Date, 
                    isLastPerson,
                    i + 1);
            }
        }
    }
}