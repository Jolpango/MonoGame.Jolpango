using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Content;
using MonoGame.Jolpango.Graphics.Sprites;
using System;

namespace MonoGame.Jolpango.ECS.Components
{
    public class JSpriteComponent : JComponent
    {
        private JSprite sprite = new JSprite();
        private string spritePath;
        public JSpriteComponent(string spritePath)
        {
            this.spritePath = spritePath;
        }

        public override void LoadContent()
        {
            sprite.LoadContent(JSharedContent.Instance.Game.Content, spritePath);
        }

        public override void UnloadContent()
        {
            sprite.UnloadContent();
        }

        public void PlayAnimation(string name, bool loop = false, Action onComplete = null)
        {
            sprite.StartAnimation(name, onComplete, loop);
        }

        public override void Update(GameTime gameTime)
        {
            var transform = Parent.GetComponent<JTransformComponent>();
            if(transform is not null)
            {
                sprite.Position = transform.Position;
                sprite.Scale = transform.Scale;
                sprite.Rotation = transform.Rotation;
            }
            sprite.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);    
        }
    }
}
