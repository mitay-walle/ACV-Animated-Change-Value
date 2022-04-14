using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.mitaywalle.ACV.Runtime
{
    public static class CanvasGroupExtensions
    {
        public static bool IsVisible(this CanvasGroup cGroup)
        {
            return cGroup.alpha == 1;
        }
        public static void Hide(this CanvasGroup cGroup)
        {
            cGroup.alpha = 0;
        }
        public static void Show(this CanvasGroup cGroup, bool state)
        {
            if (state)
            {
                cGroup.Show();
            }
            else
            {
                cGroup.Hide();
            }
        }
    
        public static void Show(this TMP_Text target, bool state = true)
        {
            target.alpha = state ? 1 : 0;
        }
        public static void Show(this Text target, bool state = true)
        {
            Color color = target.color;
        
            color.a = state ? 1 : 0;
            target.color = color;
        }
        public static void Show(this CanvasGroup cGroup)
        {
            cGroup.alpha = 1;
        }
    }
}
