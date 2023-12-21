using Microsoft.Xna.Framework;
using MonoGame.Jolpango.Graphics.Particles;
using MonoGame.Jolpango.Utilities;

namespace MonoGame.Jolpango.Graphics.Transitions
{
    public class ScaleTransition : IParticleTransition
    {
        public float StartScale { get; set; } = 1.0f;
        public float EndScale { get; set; } = 2.0f;
        public void Update(GameTime gameTime, float weight, Particle particle)
        {
            particle.Scale = JMath.Lerp(StartScale, EndScale, weight);
        }
    }
}
