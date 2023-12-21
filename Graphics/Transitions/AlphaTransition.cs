using Microsoft.Xna.Framework;
using MonoGame.Jolpango.Graphics.Particles;
using MonoGame.Jolpango.Utilities;

namespace MonoGame.Jolpango.Graphics.Transitions
{
    public class AlphaTransition : IParticleTransition
    {
        public float StartAlpha { get; set; } = 1.0f;
        public float EndAlpha { get; set; } = 0.0f;
        public void Update(GameTime gameTime, float weight, Particle particle)
        {
            particle.Alpha = JMath.Lerp(StartAlpha, EndAlpha, weight);
        }
    }
}
