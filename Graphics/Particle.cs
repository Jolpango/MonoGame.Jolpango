using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Jolpango.Graphics
{
    public class Particle
    {
        public Texture2D Texture {  get; set; }
        public float Rotation { get; set; } = 0.0f;
        public float Speed { get; set; } = 0.0f;
        public Vector2 Direction { get; set; } = Vector2.Zero;
        public Vector2 Position { get; set; }
        public float FadeOut { get; set; } = 1.0f;
        public float Alpha { get; set; } = 1.0f;
        public float LayerDepth { get; set; } = 1.0f;
        public float Timer { get; set; } = 1.0f;

        public Particle(Texture2D texture, Vector2 origin)
        {
            Texture = texture;
            Position = origin;
        }
        public void Update(GameTime gameTime)
        {
            // TODO: Use fancy function to lerp or something
            Position = Position + (Direction * (Speed * gameTime.ElapsedGameTime.Seconds));
            // TODO: Use fancy function to alpha it
            Alpha = Alpha - FadeOut * gameTime.ElapsedGameTime.Seconds;
            Timer -= gameTime.ElapsedGameTime.Seconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White * Alpha, Rotation, Vector2.Zero, Vector2.One, SpriteEffects.None, LayerDepth);
        }

        public static Particle CreateParticle(Texture2D texture, Vector2 origin)
        {
            Particle particle = new Particle(texture, origin);
            Vector2 direction = new Vector2(Random.Shared.Next(-10, 10), Random.Shared.Next(-10, 10));
            direction.Normalize();
            particle.Direction = direction;
            particle.Speed = Random.Shared.Next(1, 10);
            return particle;
        }
    }
}
