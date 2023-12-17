using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.Graphics
{
    public class ParticleEmitter
    {
        public static ParticleEmitter Shared { get; set; }
        public List<Particle> Particles { get; set; }
        public Texture2D Texture { get; set; }
        public float LayerDepth { get; set; } = 1.0f;
        public float MinSpeed { get; set; } = 0.5f;
        public float MaxSpeed { get; set; } = 1.0f;
        public Color Color { get; set; } = Color.White;
        public Color StartColor { get; set; } = Color.White;
        public Color EndColor { get; set; } = Color.White;
        public float TimeToLive { get; set; } = 1.0f;
        public int MinRadius { get; set; } = 1;
        public int MaxRadius { get; set; } = 1;
        public float MaxScale { get; set; } = 1.0f;
        public float MinScale { get; set; } = 1.0f;
        public float MinAlpha { get; set; } = 0.0f;
        public float MaxAlpha { get; set; } = 1.0f;
        public Vector2 Direction { get; set; } = new Vector2(0, -1);
        public float DirectionFreedom { get; set; } = 30f;
        public EasingFunction Easing { get; set; } = EasingFunction.EaseInOutQuad;
        public ParticleEmitter(Texture2D texture)
        {
            Texture = texture;
            Particles = new List<Particle>();
        }
        public void Emit(Vector2 position)
        {
            Particles.Add(Particle.CreateParticle(
                Texture,
                JMath.GetRandomPosition(position, Random.Shared.Next(MinRadius, MaxRadius)),
                Direction,
                StartColor,
                EndColor,
                DirectionFreedom,
                MinSpeed,
                MaxSpeed,
                LayerDepth,
                MinScale,
                MaxScale,
                MinAlpha,
                MaxAlpha,
                Easing));
        }

        public void Emit(Vector2 position, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Emit(position);
            }
        }

        public void Emit(Particle particle)
        {
            Particles.Add(particle);
        }


        public void Update(GameTime gameTime)
        {
            Particles.RemoveAll(x =>
            {
                x.Update(gameTime);
                return x.Timer >= x.TimeToLive;
            });
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var particle in Particles)
            {
                particle.Draw(spriteBatch);
            }
        }
    }
}
