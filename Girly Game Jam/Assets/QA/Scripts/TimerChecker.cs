using UnityEngine;

namespace TigerFrogGames
{
    public class TimerChecker : MonoBehaviour
    {
        /* ------- Variables ------- */

        private CountdownTimer timer;
        private FrequencyCountDownTimer timerCountDownFreq;

        [SerializeField] private float currentTime = 0f;
        [SerializeField] private float startTime = 0f;
        [SerializeField] private float endTime = 0f;
        
        /* ------- Unity Methods ------- */

        private void Start()
        {

            timer = new CountdownTimer(10f);
            
            timer.OnTimerStart += () => startTime = Time.realtimeSinceStartup;
            timer.OnTimerStop += () => endTime = Time.realtimeSinceStartup;

            timer.OnTimerPause += () => startTime = Time.realtimeSinceStartup;
            timer.OnTimerResume += () => endTime = Time.realtimeSinceStartup;
            
            
            timerCountDownFreq = new FrequencyCountDownTimer(10f, 2);
            
            timerCountDownFreq.OnTimerStart += () => startTime = Time.realtimeSinceStartup;
            timerCountDownFreq.OnTimerStop += () =>  endTime = Time.realtimeSinceStartup;

            timerCountDownFreq.OnTick += () => Debug.Log("Firing Main Cannon");// currentTime = Time.realtimeSinceStartup;

            timerCountDownFreq.OnTimerPause += () =>  startTime = Time.realtimeSinceStartup;
            timerCountDownFreq.OnTimerResume += () =>  endTime = Time.realtimeSinceStartup;
        }

        private void Update()
        {
            if (timer.IsRunning)
            {
                //Debug.Log($"Timer - {timer.CurrentTime}");
                currentTime = timer.CurrentTime;
            }
            
            if (timerCountDownFreq.IsRunning)
            {
                //Debug.Log($"Timer - {timer.CurrentTime}");
                currentTime = timerCountDownFreq.CurrentTime;
            }
            
            
        }

        /* ------- Methods ------- */

        
        [ContextMenu("Start Timer")]
        private void StartTimer()
        {
            timer.Start();
        }

        [ContextMenu("Start Frequent Timer")]
        private void StartTimerFreq()
        {
            timerCountDownFreq.Start();
        }
        
        private void OnDestroy()
        {
            timer.Dispose();
        }
        
    }
}