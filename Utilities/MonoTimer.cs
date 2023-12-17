using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Jolpango.Utilities
{
    public delegate void MonoTimerCallback();
    // UpdateSharedTimers needs to be called for shared timers to work
    public class MonoTimer
    {
        private static List<MonoTimer> timers = new List<MonoTimer>();

        private MonoTimerCallback callback;
        private float time;
        public bool Done { get { return time < 0; } }
        public MonoTimer(float time, MonoTimerCallback callback)
        {
            this.time = time;
            this.callback = callback;
        }

        public void Update(GameTime gameTime)
        {
            time -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (time < 0)
            {
                callback();
            }
        }
        public static MonoTimer AddNewSharedTimer(float time, MonoTimerCallback callback)
        {
            timers.Add(new MonoTimer(time, callback));
            return timers.Last();
        }
        public static void UpdateSharedTimers(GameTime gameTime)
        {
            timers.RemoveAll(timer =>
            {
                timer.Update(gameTime);
                return timer.Done;
            });
        }
    }
}
