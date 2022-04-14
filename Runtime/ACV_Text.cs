using TMPro;
using UnityEditor;
using UnityEngine;

namespace Plugins.mitaywalle.ACV.Runtime
{
    public sealed class ACV_Text : ACV_Generic<TMP_Text>
    {
        [SerializeField] private RectTransform _resizeTr = default;

        [SerializeField, Header("Format")] private string Key = "{0}";
        [SerializeField] private string _format = "N0";
        [SerializeField] private bool _showMax = false;
        [SerializeField] private bool _calcSizeOnChange = false;
        [SerializeField] private bool _roundToInt = true;

        public override void SetValue()
        {
            if (_ifChangeOnly && _value == _lastValue && _lastMax == _maxValue) return;

            _lastMax = _maxValue;

            base.SetValue();

#if UNITY_EDITOR
            if (_isDebugging) Debug.LogError($"SetValue {_value}");

            if (!Application.isPlaying)
            {
                try
                {
                    SetTextValues();
                }
                catch
                {
                    _target.SetText(Key);
                }
            }
            else
            {
                SetTextValues();
            }
#else
			SetTextValues();
#endif

            if (_isDebugging) Debug.Log("SetValue");

            ResetLayout(false);

#if UNITY_EDITOR
            if (!Application.isPlaying && _target) EditorUtility.SetDirty(_target);
#endif
        }

        private void SetTextValues()
        {
            float valueToShow = _roundToInt ? (int)(_animatedValue + .5f) : _animatedValue;

            if (_showMax)
            {
                string val = valueToShow.ToString(_format);
                string max = _maxValue.ToString(_format);

                _target.text = string.Format(Key, val, max);

                if (_isDebugging) Debug.LogError($"set {val} / {max}");
            }
            else
            {
                string val = valueToShow.ToString(_format);
                _target.text = string.Format(Key, val);

                if (_isDebugging) Debug.LogError($"set {val}");
            }
        }

        public override void ResetLayout(bool setValue)
        {
            base.ResetLayout(setValue);

            if (_calcSizeOnChange)
            {
                if (_resizeTr == null)
                {
                    _resizeTr = transform as RectTransform;
                }

                float width = _target.preferredWidth;
            }
        }

        public override void DisableTarget()
        {
            _target.gameObject.SetActive(false);
        }

        protected override void Reset()
        {
#if UNITY_EDITOR

            base.Reset();

            _target = GetComponent<TMP_Text>();

            EditorUtility.SetDirty(this);
#endif
        }
    }
}
