using System;
using UnityEngine;

namespace Plugins.mitaywalle.ACV.Runtime
{
    public class ACV_CanvasGroupLine : ACV_Line<CanvasGroup>
    {
        protected override CanvasGroup GetTarget(Transform target)
        {
            return target.GetComponent<CanvasGroup>();
        }

        protected override bool GetState(CanvasGroup target)
        {
            return Math.Abs(target.alpha - 1) < .001f;
        }

        protected override void SetState(CanvasGroup target, bool state)
        {
            target.alpha = state ? 1 : 0;
        }

    }
}
