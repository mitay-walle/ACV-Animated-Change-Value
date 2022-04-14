using UnityEngine;

namespace Plugins.mitaywalle.ACV.Runtime
{
    public struct TimeTrigger
    {
        float endTime;
        public float time;

        public TimeTrigger(float time)
        {
            this.time = time;
            endTime = 0;
        }

        public bool CheckAndRestart()
        {
            bool isComplete = IsReady();
            if (isComplete)
                Restart();
            return isComplete;
        }

        public bool CheckAndCancel()
        {
            bool isComplete = IsReady();
            if (isComplete)
                endTime = float.MaxValue;
            return isComplete;
        }

        public bool IsReady()
        {
            return Time.time >= endTime;
        }

        public void Restart()
        {
            endTime = Time.time + time;
        }

        public void Restart(float time)
        {
            this.time = time;
            endTime = Time.time + time;
        }

        public void Reset()
        {
            endTime = Time.time;
        }

        public void Reset(float time)
        {
            endTime = Time.time + time;
        }

        public float GetTimeLeft()
        {
            return Mathf.Max(0, endTime - Time.time);
        }
    
        public float GetTimeElapsed()
        {
            return Mathf.Max(0, endTime - GetTimeLeft());
        }

        public float GetStartTime()
        {
            return endTime - time;
        }
    }
}
