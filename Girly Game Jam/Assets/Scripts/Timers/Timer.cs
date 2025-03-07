using System;
using UnityEngine;

namespace TigerFrogGames
{
    public abstract class Timer : IDisposable
    {
        public float CurrentTime { get; protected set; }
        public bool IsRunning { get; private set; }

        protected float initialTime;

        public float Progress => Mathf.Clamp(CurrentTime / initialTime, 0, 1);

        public Action OnTimerStart = delegate { };
        public Action OnTimerStop = delegate { };
        
        public Action OnTimerPause = delegate { };
        public Action OnTimerResume = delegate { };

        protected Timer(float value)
        {
            {
                initialTime = value;
            }
        }

        ~Timer()
        {
            Dispose(false);
        }

        private bool disposed;
        //Call Despose to ensure deregistraion of the timer from the TimerManager
        //when the consumer is done with the timer or being destroyed
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposed) return;

            if (disposing)
            {
                TimerManager.DeRegisterTimer(this);
            }
        }

        public void Start()
        {
            CurrentTime = initialTime;
            if (!IsRunning)
            {
                IsRunning = true;
                TimerManager.RegisterTimer(this);
                OnTimerStart.Invoke();
            }
        }

        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                TimerManager.DeRegisterTimer(this);
                OnTimerStop.Invoke();
            }
        }
        
        public abstract void Tick();
        public abstract bool IsFinished { get; }

        public void Resume()
        {
            IsRunning = true;
            OnTimerResume.Invoke();
        }

        public void Pause()
        {
            IsRunning = false;
            OnTimerPause.Invoke();
        } 

        public virtual void Reset() => CurrentTime = initialTime;

        public virtual void Reset(float newTime)
        {
            initialTime = newTime;
            Reset();
        }
    }
}