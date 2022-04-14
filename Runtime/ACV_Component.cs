using UnityEditor;
using UnityEngine;

namespace Plugins.mitaywalle.ACV.Runtime
{
    public abstract class ACV_Component<T> : ACV_Generic<T> where T : Component
    {
        protected override void Reset()
        {
#if UNITY_EDITOR
            base.Reset();
            if (!_target) _target = GetComponentInChildren<T>(true);
            if (!_target) _target = GetComponentInParent<T>();
            if (!_target) _target = gameObject.AddComponent<T>();
            EditorUtility.SetDirty(this);
#endif
        }
    }
}