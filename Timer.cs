using UnityEngine;
using System.Collections;

namespace UberTools
{
    public class Timer
    {
        //Starts a timer
        public void Start(float duration)
        {
            StartTime = Time.time;
            Duration = duration;
        }

        //Returns a value from 0.0 to 1.0 
        public float GetCompletionAmount()
        {
            if (Duration <= 0.0f)
                return 0.0f;

            float runningTime = Time.time - StartTime;
            var amount = runningTime / Duration;

            if (amount >= 1.0f)
            {
                return 1.0f;
            }
            return amount;
        }

        //Returns whether or not the timer has elapsed
        public bool Finished
        {
            get
            {
                return StartTime < 0 || Time.time - StartTime > Duration;
            }
        }

        private float StartTime = -1.0f;
        private float Duration = 0;
    }
}
