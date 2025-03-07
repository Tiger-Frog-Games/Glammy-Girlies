using System;
using UnityEngine;

namespace TigerFrogGames
{
    public class FrequencyCountDownTimer : Timer
    {
        public int TicksPerSecond { get; private set; }
        
        public Action OnTick = delegate { };

        private float countDownTime;
        
        float timeThreshold;

        public FrequencyCountDownTimer(float countDown, int ticksPerSecond) : base(0) {
            CalculateTimeThreshold(ticksPerSecond);
            countDownTime = countDown;
        }

        public override void Tick() {
            if (IsRunning && CurrentTime >= timeThreshold) {
                CurrentTime -= timeThreshold;
                OnTick.Invoke();
            }

            if (IsRunning && CurrentTime < timeThreshold) {
                CurrentTime += Time.deltaTime;
            }
            
            if (IsRunning && countDownTime > 0)
            {
                countDownTime -= Time.deltaTime;
            }
            
            if (IsRunning && countDownTime <= 0)
            {
                Stop();
            }
        }

        public override bool IsFinished => countDownTime <= 0;

        public override void Reset() {
            CurrentTime = 0;
        }
        
        public void Reset(int newTicksPerSecond) {
            CalculateTimeThreshold(newTicksPerSecond);
            Reset();
        }
        
        void CalculateTimeThreshold(int ticksPerSecond) {
            TicksPerSecond = ticksPerSecond;
            timeThreshold = 1f / TicksPerSecond;
        }
    }
}