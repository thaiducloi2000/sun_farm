using Sirenix.OdinInspector;
using UnityEngine;

namespace Score
{

    public abstract class BaseConfig<T> : SerializedScriptableObject,IConfig
    {
        protected T[] Data;
        public abstract void Load();
    }
}