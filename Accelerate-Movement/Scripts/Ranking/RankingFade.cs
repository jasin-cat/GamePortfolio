using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using NaughtyAttributes;
using sugi;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankingFade : MonoBehaviour
{
    CancellationTokenSource _cts = new();
    [SerializeField] private float _duration;
    [SerializeField] private CanvasGroup _cg;
    [SerializeField] private Animator _characterAnim;
    [SerializeField] private float _fadeBegin;

    void Start()
    {
        FadeIn(_cts.Token).Forget();
    }

    [Button]
    void FadeIn()
    {
        _cts.Cancel();

        _cts = new();
        FadeIn(_cts.Token).Forget();
    }

    [Button]
    void FadeOut()
    {
        _cts.Cancel();

        _cts = new();
        FadeOut(_cts.Token).Forget();
    }

    public async UniTask FadeIn(CancellationToken token)
    {
        await LMotion.Create(0f, 1f, _duration)
            .WithOnCancel(() => _cg.alpha = 1f)
            .BindToAlpha(_cg).ToUniTask(token);
    }

    public async UniTask FadeOut(CancellationToken token)
    {
        await LMotion.Create(1f, 0f, _duration)
            .WithOnCancel(() => _cg.alpha = 0f)
            .BindToAlpha(_cg).ToUniTask(token);

        _characterAnim.SetTrigger("char_on");
        await UniTask.Delay(TimeSpan.FromSeconds(_fadeBegin));
        await FadeController.Instance.FadeIn(duration: 0, token);

        SceneManager.LoadScene("Title");
        FadeController.Instance.FadeOut(duration: 0, token).Forget();
    }
}