using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevExperience
{
    public class Timer
    {
        public EventHandler<EventArgs> TimerAlertEvent;

        public TimerUnit Unit;
        public float Interval { get; private set; }
        double timer = 0;

        public Timer()
        {
            Interval = 0;
            Unit = TimerUnit.Milliseconds;
        }

        public Timer(float interval)
        {
            Interval = interval;
            Unit = TimerUnit.Milliseconds;
        }

        public Timer(TimerUnit unit)
        {
            Interval = 0;
            Unit = unit;
        }

        public Timer(TimerUnit unit, float interval)
        {
            Unit = unit;
            Interval = interval;
        }

        public void Update(GameTime gameTime)
        {
            if (Interval <= 0) return;
            if (timer >= Interval)
            {
                TimerAlertEvent?.Invoke(this, new EventArgs());
                timer -= Interval;
            }
            else
            {
                if (Unit == TimerUnit.Milliseconds) timer += gameTime.ElapsedGameTime.TotalMilliseconds;
                else if (Unit == TimerUnit.Seconds) timer += gameTime.ElapsedGameTime.TotalSeconds;
                else if (Unit == TimerUnit.Minutes) timer += gameTime.ElapsedGameTime.TotalMinutes;
                else if (Unit == TimerUnit.Hours) timer += gameTime.ElapsedGameTime.TotalHours;
            }
        }

        /// <summary>
        /// Updates the timer interval and resets the timer
        /// </summary>
        /// <param name="interval">The interval to change the timer to</param>
        public void UpdateIndicator(float interval)
        {
            Interval = interval;
            timer = 0;
        }
    }
    public enum TimerUnit
    {
        Milliseconds,
        Seconds,
        Minutes,
        Hours
    }
}
