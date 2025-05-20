using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.Graphics.Sprites
{
    public class JAnimation
    {
        public int[] KeyFrames { get; set; }
        private float timePerFrame;
        public float FramesPerSecond
        {
            get
            {
                return 1.0f / timePerFrame;
            }
            set
            {
                timePerFrame = 1.0f / value;
            }
        }
        public float TotalTime { get => KeyFrames.Length * timePerFrame; }
        public float CurrentTime { get; set; } = 0.0f;
        public int CurrentFrame { get => KeyFrames[CurrentFrameIndex]; }
        public int CurrentFrameIndex { get => (int)(CurrentTime / timePerFrame) % KeyFrames.Length; }
        public bool IsActive { get; set; } = false;
        private bool isCompleted = false;
        public bool IsLooping { get; set; } = false;
        public bool IsCompleted
        {
            get => isCompleted;
            protected set
            {
                if (isCompleted != value)
                {
                    isCompleted = value;
                    if (isCompleted)
                    {
                        onCompleteAction?.Invoke();
                    }
                }
            }
        }
        private Action onCompleteAction;
        public JAnimation(JAnimationCycleSettings cycleSettings)
        {
            KeyFrames = cycleSettings.Frames;
            timePerFrame = cycleSettings.FrameDuration;
            IsLooping = cycleSettings.IsLooping;
        }
        public void StartAnimation(Action onComplete = null)
        {
            onCompleteAction = onComplete;
            IsActive = true;
        }

        public void StopAnimation()
        {
            CurrentTime = 0;
            IsActive = false;
        }
        public bool Update(GameTime gameTime)
        {
            if (!IsActive)
                return false;
            CurrentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (IsLooping)
                return false;

            if(CurrentTime >= TotalTime)
                isCompleted = true;
            return IsCompleted;
        }
    }
}
