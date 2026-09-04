using UnityEngine;

namespace sugi
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if(_instance is null)
                {
                    _instance = FindAnyObjectByType<T>();

                    if(_instance is null)
                    {
                        var s = new GameObject(typeof(T).Name);
                        _instance = s.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if(_instance is null)
            {
                _instance = this as T;
                DontDestroyOnLoad(this.gameObject);
            }
            else if(_instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }
}