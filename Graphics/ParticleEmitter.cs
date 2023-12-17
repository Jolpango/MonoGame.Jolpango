using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public ParticleEmitter(Texture2D texture)
        {
            Texture = texture;
            Particles = new List<Particle>();
        }

        public void Emit(Vector2 position, Color color)
        {
            Particles.Add(Particle.CreateParticle(Texture, position, color));
        }

        public void Emit(Vector2 position, int amount, Color color)
        {
            for (int i = 0; i < amount; i++)
            {
                Emit(position, color);
            }
        }

        public void Emit(Vector2 position, int amount, int minSpeed, int maxSpeed, Color color)
        {
            for (int i = 0; i < amount; i++)
            {
                Emit(Particle.CreateParticle(Texture, position, color, minSpeed, maxSpeed));
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
                return x.Timer <= 0;
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
