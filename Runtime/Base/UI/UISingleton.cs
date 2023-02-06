using UnityEngine;
using System.Collections;

namespace X
{
    public abstract class UISingleton<T> : MonoBehaviour where T : UISingleton<T>
    {
        static private T instance = null;
        static public T Instance
        {
            get
            {
                return instance;
            }
        }

        private void Awake()
        {
            Debug.Log(name);
            if ( instance == null )
            {
                instance = this as T;
                instance.InitSingleton();
            }
        }

        protected virtual void InitSingleton()
        {

        }

        public bool IsShow { get { return gameObject.activeSelf; } }

        public virtual void Show()
        {
            gameObject.SetActive( true );
        }

        public virtual void UnShow()
        {
            gameObject.SetActive( false );
        }
    }
}

