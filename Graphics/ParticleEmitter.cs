using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Jolpango.Graphics
{
    public class ParticleEmitter
    {
        public List<Particle> Particles { get; set; }
        public Texture2D Texture { get; set; }
        public ParticleEmitter(Texture2D texture)
        {
            Texture = texture;
            Particles = new List<Particle>();
        }

        public void Emit(Vector2 position)
        {

        }

        public void Emit(Vector2 position, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Particles.Add(Particle.CreateParticle(Texture, position));
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
