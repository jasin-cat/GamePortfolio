using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using sugi;
using System.Threading;
using Cysharp.Threading.Tasks;

public class RankingBackButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CancellationTokenSource _cts;
    [SerializeField] private RankingFade _fade;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _on;
    [SerializeField] private GameObject _off;
    private bool _onClick;

    void Awake()
    {
        if (_cts is null)
        {
            _cts = new();
        }

        _off?.SetActive(true);
        _on?.SetActive(false);

        _button.onClick.AddListener(async () =>
        {
            _cts.Cancel();
            _cts = new();
            _button.enabled = false;
            _onClick = true;

            await _fade.FadeOut(_cts.Token);
        });
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_onClick) return;

        _off?.SetActive(false);
        _on?.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_onClick) return;

        _off?.gameObject.SetActive(true);
        _on?.gameObject.SetActive(false);
    }
}