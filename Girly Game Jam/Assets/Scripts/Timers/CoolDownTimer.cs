using System;
using UnityEngine;

namespace TigerFrogGames
{
    public class CoolDownTimer : Timer
    {
        public Action OnTick = delegate { };
        
        float timeThreshold;

        public CoolDownTimer(float coolDown) : base(0) {
            
                timeThreshold = coolDown;
                
        }

        public override void Tick() {
            if (IsRunning && CurrentTime >= timeThreshold) {
                CurrentTime -= timeThreshold;
                OnTick.Invoke();
            }

            if (IsRunning && CurrentTime < timeThreshold) {
                CurrentTime += Time.deltaTime;
            }
        }

        public override bool IsFinished => !IsRunning;

        public override void Reset() {
            CurrentTime = 0;
        }
        
        public override void Reset(float newCoolDown)
        {
            timeThreshold = newCoolDown;
            base.Reset();
        }
    }
}