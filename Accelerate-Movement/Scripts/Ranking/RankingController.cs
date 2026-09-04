using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

namespace sugi
{
    public class RankingController : MonoBehaviour
    {
        CancellationTokenSource _cts = new();
        [SerializeField] private RankingView _view;
        [SerializeField] private List<float> _time = new();
        //private RankingSave _rankingSave;

        async void Start()
        {
            //_rankingSave = new();
    # if UNITY_EDITOR
            Debug.Log(RankingDataToJson.RankingPath);
    # endif
            if(RankingDataToJson.TryLoad(out RankingData rd))
            {
                _view.SetRankingElements(rd);
            }

            await FadeController.Instance.FadeOut(1.0f, _cts.Token);
        }

    # if UNITY_EDITOR
        [Button]
        void DataReset()
        {
            RankingDataToJson.Reset();
        }

        [Button]
        void Save()
        {
            foreach(var time in _time) RankingSave.Save(time);
        }

        [Button]
        void Load()
        {
            if(RankingDataToJson.TryLoad(out RankingData rd))
            {
                _view.SetRankingElements(rd);
            }
        }
    # endif
    }

    [System.Serializable]
    public class RankingData
    {
        public List<RankingPersonData> TopTwentyData = new();
        public RankingPersonData LastTimePerson = new();

        /// <summary>
        /// データを入れる
        /// </summary>
        /// <param name="time"></param>
        public void SetData(float time)
        {
            RankingPersonData data = new();
            data.Time = time;
            var yyyy = System.DateTime.Now.Year.ToString();
            var MM = System.DateTime.Now.ToString("MM");
            var dd = System.DateTime.Now.ToString("dd");
            var hh = System.DateTime.Now.ToString("hh");
            var mm = System.DateTime.Now.ToString("mm");
            var ss = System.DateTime.Now.ToString("ss");
            Debug.Log(yyyy);
            data.Date = $"{yyyy} / {MM} / {dd}  {hh}:{mm}:{ss}";

            LastTimePerson = data;
            TopTwentyData.Add(data);
        }

        /// <summary>
        /// トップテンをソートして作る11位以降を消す
        /// </summary>
        public void TopTwentyDataSortData()
        {
            TopTwentyData.Sort((a,b) => a.Time.CompareTo(b.Time));
            while(TopTwentyData.Count > 20)
            {
                TopTwentyData.RemoveAt(TopTwentyData.Count - 1);
            }
        }
    }

    [System.Serializable]
    public class RankingPersonData
    {
        public float Time;
        public string Date;
    }
}