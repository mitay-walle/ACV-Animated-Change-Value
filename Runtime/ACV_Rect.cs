using UnityEngine;

namespace Plugins.mitaywalle.ACV.Runtime
{
    public class ACV_Rect : ACV_Component<RectTransform>
    {
        [SerializeField] private Vector2 from = default;
        [SerializeField] private Vector2 to = default;

        public override void SetValue()
        {
            if (_hideIfZeroGameObject && !_objectToHide)
                _objectToHide = _target.gameObject;

            base.SetValue();

            if (_isDebugging) Debug.LogError("set rect!", gameObject);

            _target.sizeDelta = Vector2.Lerp(from, to, Mathf.InverseLerp(_minValue, _maxValue, _animatedValue));
        }

        public override void DisableTarget()
        {
            _target.gameObject.SetActive(false);
        }
    }
}
