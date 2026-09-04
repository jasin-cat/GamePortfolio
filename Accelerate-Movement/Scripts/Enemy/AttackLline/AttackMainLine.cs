using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion;
using UnityEngine;
using UnityEngine.UI;

public class AttackMainLine : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sr;
    private MaterialPropertyBlock _materialPropertyBlock;
    private bool _completedFade = false;

    public async UniTask Fade(CancellationToken token, float from, float to, float duration)
    {
        await LMotion.Create(from, to, duration)
            .Bind(x => SetAlfa(x))
            .ToUniTask(token);

        _completedFade = true;
    }

    public async UniTask FadeIn(CancellationToken token, float duration)
    {
        RotationTexture(0f);
        await Fade(token, 0f, 1f, duration);
    }

    public async UniTask FadeOut(CancellationToken token, float duration)
    {
        RotationTexture(180f);
        await Fade(token, 1f, 0f, duration);
    }

    public async UniTask OutLineAnimationAsync(CancellationToken token, float from, float to, float duration)
    {
        await LMotion.Create(from, to, duration)
            .Bind(x => SetOutLine(x))
            .ToUniTask(token);
    }

    public async UniTask AnimationAsync(
            CancellationToken token,
            float fadeDuration, 
            float outLineDuration
            )
    {
        await FadeIn(token, fadeDuration);
        await OutLineAnimationAsync(token, 0.5f, 0f, outLineDuration);
    }

    private void SetAlfa(float alpha)
    {
        if (_materialPropertyBlock is null)
        {
            _materialPropertyBlock = new();
        }

        _sr.GetPropertyBlock(_materialPropertyBlock);
        _materialPropertyBlock.SetFloat("_Alpha", alpha);
        _sr.SetPropertyBlock(_materialPropertyBlock);
    }

    private void SetOutLine(float outLine)
    {
        if (_materialPropertyBlock is null)
        {
            _materialPropertyBlock = new();
        }

        _sr.GetPropertyBlock(_materialPropertyBlock);
        _materialPropertyBlock.SetFloat("_OutLineBeginRange", outLine);
        _sr.SetPropertyBlock(_materialPropertyBlock);
    }

    private void RotationTexture(float rot)
    {
        if (_materialPropertyBlock is null)
        {
            _materialPropertyBlock = new();
        }

        _sr.GetPropertyBlock(_materialPropertyBlock);
        _materialPropertyBlock.SetFloat("_Rotation", rot);
        _sr.SetPropertyBlock(_materialPropertyBlock);
    }
}