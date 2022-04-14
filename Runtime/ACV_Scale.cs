using UnityEngine;

namespace Plugins.mitaywalle.ACV.Runtime
{
    public class ACV_Scale : ACV_Component<Transform>
    {
        [SerializeField] private Vector3 from;
        [SerializeField] private Vector3 to;

        public override void SetValue()
        {
            if (_ifChangeOnly && _value == _lastValue) return;
            
            if (_hideIfZeroGameObject && !_objectToHide)
                _objectToHide = _target.gameObject;

            base.SetValue();

            if (_isDebugging) Debug.LogError("set scale!", gameObject);
        
            _target.localScale = Vector3.Lerp(from,to,_animatedValue/_maxValue);
        }

        public override void DisableTarget()
        {
            _target.gameObject.SetActive(false);
        }
        protected override void Reset()
        {
            base.Reset();

            if (from == default && to == default && _target)
            {
                to = from = _target.localScale;
                from.x = 0;
            }
        }
    }
}
