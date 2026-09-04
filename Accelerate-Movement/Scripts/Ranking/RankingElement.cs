using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankingElement : MonoBehaviour
{
    private const int _sexagesimalSystem = 60;
    [SerializeField] private TextMeshProUGUI _time;
    [SerializeField] private TextMeshProUGUI _date;
    [SerializeField] private Image _yourIcon;
    [SerializeField] private TextMeshProUGUI _rankTMPro;
    [SerializeField] private GameObject _focus;

    void Awake()
    {
        _focus?.SetActive(false);
        _yourIcon?.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void Initialize(
        float time, 
        string date, 
        bool isLastPerson,
        int rankNumber)
    {
        this.gameObject.SetActive(true);
        _time.text = $"{GetTimeString(time)}";
        if (_date is not null)
        {
            _date.text = date;
        }

        if (isLastPerson)
        {
            _yourIcon?.gameObject.SetActive(true);
            _focus?.SetActive(true);
        }

        SetRank(rankNumber);
    }

    /// <summary>
    /// top3以外なら表示する
    /// </summary>
    /// <param name="rank"></param>
    private void SetRank(int rank)
    {
        if(_rankTMPro is null) return;

        if(rank is not 1 or 2 or 3)
            _rankTMPro.SetText(rank.ToString());
    }

    /// <summary>
    /// floatを時間に戻す
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private string GetTimeString(float time)
    {
        var min = Mathf.FloorToInt(time / _sexagesimalSystem);
        var sec = time % _sexagesimalSystem;
        var info = new NumberFormatInfo{NumberDecimalSeparator = ":"};
        var total = $"{min:00}:{sec.ToString("00.00", info)}";
        total.Replace(".", ":");

        return total;
    }
}
