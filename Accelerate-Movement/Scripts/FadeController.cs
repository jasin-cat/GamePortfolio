using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion;
using UnityEngine;
using UnityEngine.UI;

namespace sugi
{
    public class FadeController : Singleton<FadeController>
    {
        [SerializeField] private Image _image;
        [SerializeField] private float _duraiton;
        [SerializeField] private Texture _ruleTex;
        private Material _material;

        protected override void Awake()
        {
            base.Awake();
            SetMaterialTexture();
        }

        /// <summary>
        /// テクスチャを入れる
        /// </summary>
        private void SetMaterialTexture()
        {
            if (_material is null)
            {
                _material = _image.material;
            }
            _material.SetTexture("_RuleTex", _ruleTex);
        }

        public async UniTask WaitFadeIn(float fadeDuration, float waitTime, CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(waitTime), cancellationToken: token);
            FadeIn(fadeDuration, token).Forget();
        }

        public async UniTask FadeIn(float duration, CancellationToken token)
        {
            if(duration is not 0)
            {
                _duraiton = duration;
            }

            _image.raycastTarget = true;

            await LMotion.Create(0f, 1f, _duraiton)
                .Bind(x =>
                {
                    _material.SetFloat("_Alpha", x);
                }).ToUniTask(token);
            
            _image.raycastTarget = false;
        }

        public async UniTask FadeOut(float duration, CancellationToken token)
        {
            if(duration is not 0)
            {
                _duraiton = duration;
            }

            _image.raycastTarget = true;

            await LMotion.Create(1f, 0f, _duraiton)
                .WithOnComplete(() => _image.raycastTarget = true)
                .Bind(x =>
                {
                    _material.SetFloat("_Alpha", x);
                }).ToUniTask(token);

            _image.raycastTarget = false;
        }
    }
}