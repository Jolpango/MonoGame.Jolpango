using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Jolpango.Input;
using MonoGame.Jolpango.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.UI.Elements
{
    public class UIButton : UIElement
    {
        private Texture2D texture;
        public event Action<bool> OnClick;
        public UIButton(Texture2D texture = null)
        {
            this.texture = texture ?? JTextureCache.White;
        }

        public override void Update(GameTime gameTime, JMouseInput mouseInput, JKeyboardInput keyboardInput)
        {
            if (IsMouseOver(mouseInput.Position) && mouseInput.IsLeftButtonClicked())
            {
                OnClick?.Invoke(true);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, BoundingBox, Color.White);
        }
    }
}
