using UnityEngine;

namespace Plugins.mitaywalle.ACV.Runtime
{
    public abstract class ACV_Generic<T> : ACV_Base
    {
        [SerializeField,Header("Targets")] protected T _target;
    }
}
