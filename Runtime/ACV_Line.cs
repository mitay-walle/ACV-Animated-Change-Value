using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

namespace Plugins.mitaywalle.ACV.Runtime
{
    public abstract class ACV_Line<T> : ACV_Generic<T[]>
    {
        private const string TOOLTIP = "filter to search only objects with 'name.Contains(_childObjectNameToSearch)' , ignoring case";
        private const string TOOLTIP_DIRECTION = "Default - left-to-right slider\nInvert - right-to-left slider\n";

        [SerializeField, Tooltip(TOOLTIP_DIRECTION), Header("Line")] private DirectionPattern _direction = DirectionPattern.Default;
        [SerializeField] private HorizontalOrVerticalLayoutGroup lGroup = default;
        [SerializeField, Tooltip(TOOLTIP)] private string _childObjectNameToSearch = default;
        [SerializeField] private bool _resetLayoutOnEnable = true;
        [SerializeField] private bool _resetLayoutOnSet = false;

        protected abstract T GetTarget(Transform target);
        protected abstract bool GetState(T target);
        protected abstract void SetState(T target, bool state);

        public override void DisableTarget() => gameObject.SetActive(false);

        protected override void OnEnable()
        {
            base.OnEnable();
            if (_resetLayoutOnEnable) ResetLayout();
        }

        public override void SetValue()
        {
            base.SetValue();
            int count = _target.Length;

            for (int i = 0; i < count; i++)
            {
                bool state = false;

                switch (_direction)
                {
                    case DirectionPattern.Default:
                        state = i * (_maxValue - _minValue) / count <= _value && _value > _minValue;

                        break;

                    case DirectionPattern.Invert:
                        float range = _maxValue - _minValue;
                        int invertIndex = count - i;

                        state = !(invertIndex * range / count <= _value);

                        //if (Debugging) Debug.Log($"{invertIndex}    { range }    { range / count}    {invertIndex * range / count } >= {value}  {state}");
                        break;

                    case DirectionPattern.RightHand:
                        state = (count - i) * (_maxValue - _minValue) / count <= _value && _value > _minValue;

                        break;

                    case DirectionPattern.Handle:
                        float normValue = Mathf.InverseLerp(_minValue, _maxValue, _value);
                        int index = (int)(normValue * count);
                        state = index == i;

                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (GetState(_target[i]) != state) SetState(_target[i], state);
            }

            if (_resetLayoutOnSet) ResetLayout(false);
        }

        public override void ResetLayout(bool setValue)
        {
            if (lGroup)
            {
                lGroup.enabled = true;

                for (int i = 0; i < _target.Length; i++)
                {
                    if (!GetState(_target[i])) SetState(_target[i], true);
                }

                LayoutRebuilder.ForceRebuildLayoutImmediate(lGroup.transform as RectTransform);
                lGroup.enabled = false;
            }

            if (setValue) SetValue();
        }


        protected override void Reset()
        {
#if UNITY_EDITOR

            base.Reset();

            Debug.Log("reset");
            
            Transform[] transforms = GetComponentsInChildren<Transform>(true);

            List<Transform> targets = new List<Transform>();

            string targetName = _childObjectNameToSearch.ToLowerInvariant();

            for (int i = 0; i < transforms.Length; i++)
                if (transforms[i].name.ToLowerInvariant().Contains(targetName))
                    targets.Add(transforms[i]);

            _target = targets.Select(GetTarget).ToArray();
            if (!lGroup) lGroup = GetComponent<HorizontalOrVerticalLayoutGroup>();
#endif
        }
    }
}
