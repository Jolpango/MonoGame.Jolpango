using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Utilities;
using System;

namespace MonoGame.Jolpango.Graphics
{
    public class Particle
    {
        public Texture2D Texture {  get; set; }
        public float Rotation { get; set; } = 0.0f;
        public float Speed { get; set; } = 0.0f;
        public Vector2 Direction { get; set; } = Vector2.Zero;
        public Vector2 Position { get; set; }
        public float MinAlpha { get; set; } = 1.0f;
        public float MaxAlpha { get; set; } = 1.0f;
        public float Alpha { get; set; } = 1.0f;
        public float LayerDepth { get; set; } = 1.0f;
        public float TimeToLive { get; set; } = 1.0f;
        public float Timer { get; set; } = 0.0f;
        public Color Color { get; set; } = Color.White;
        public Color StartColor { get; set;} = Color.White;
        public Color EndColor { get; set; } = Color.White;
        public float MinScale { get; set; } = 1.0f;
        public float MaxScale { get; set; } = 1.0f;
        public float Scale { get; set; } = 1.0f;
        public EasingFunction Easing { get; set; } = EasingFunction.EaseInOutQuad;
        public Func<double, double, double> EasingDelegate { get; set; } = JMath.EaseInOutQuad;
        public Particle(Texture2D texture, Vector2 origin)
        {
            Texture = texture;
            Position = origin;
        }
        public Vector2 Origin 
        {
            get
            {
                return new Vector2(Texture.Width / 2 * Scale, Texture.Height / 2 * Scale);
            }
        }
        public void Update(GameTime gameTime)
        {
            float easeFactor = (float)EasingDelegate(Timer, TimeToLive);
            Position = Position + (Direction * Speed * easeFactor);

            Alpha = JMath.Lerp(MinAlpha, MaxAlpha, easeFactor);
            Scale = JMath.Lerp(MinScale, MaxScale, easeFactor);
            Color = Color.Lerp(StartColor, EndColor, easeFactor);

            Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color * Alpha, Rotation, Vector2.Zero, Vector2.One * Scale, SpriteEffects.None, LayerDepth);
        }

        public static Particle CreateParticle(
            Texture2D texture,
            Vector2 origin,
            Vector2 direction,
            Color startColor,
            Color endColor,
            float directionFreedom = 360f,
            float minSpeed = 1,
            float maxSpeed = 10,
            float layerDepth = 1.0f,
            float minScale = 1.0f,
            float maxScale = 1.0f,
            float minAlpha = 0.0f,
            float maxAlpha = 1.0f,
            EasingFunction easing = EasingFunction.EaseInOutQuad)
        {
            Particle particle = new Particle(texture, origin);
            particle.Color = startColor;
            particle.StartColor = startColor;
            particle.EndColor = endColor;
            float rand = (float)Random.Shared.NextDouble() * 2.0f - 1;
            float randDir = directionFreedom * rand;
            particle.Direction = JMath.RotateVector(direction, MathHelper.ToRadians(randDir));
            particle.Direction.Normalize();
            particle.Direction.Normalize();
            particle.Speed = JMath.Lerp(minSpeed, maxSpeed, (float)Random.Shared.NextDouble());
            particle.LayerDepth = layerDepth;
            particle.MinScale = minScale;
            particle.MaxScale = maxScale;
            particle.Easing = easing;
            particle.MinAlpha = minAlpha;
            particle.MaxAlpha = maxAlpha;
            particle.Alpha = minAlpha;
            particle.Scale = minScale;

            switch (particle.Easing)
            {
                case EasingFunction.EaseInOutQuad:
                    particle.EasingDelegate = JMath.EaseInOutQuad;
                    break;
                case EasingFunction.EaseOutBack:
                    particle.EasingDelegate = JMath.EaseOutBack;
                    break;
            }
            return particle;
        }
    }
}
