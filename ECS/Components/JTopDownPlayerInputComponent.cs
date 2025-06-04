using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.ECS.Components
{
    public class JTopDownPlayerInputComponent : JInputComponent
    {
        public Keys LeftKey { get; set; } = Keys.A;
        public Keys RightKey { get; set; } = Keys.D;
        public Keys UpKey { get; set; } = Keys.W;
        public Keys DownKey { get; set; } = Keys.S;

        public override void UpdateIntent(GameTime gameTime)
        {
            var k = Keyboard.GetState();
            MoveIntent = Vector2.Zero;
            if (k.IsKeyDown(UpKey)) MoveIntent.Y -= 1;
            if (k.IsKeyDown(LeftKey)) MoveIntent.X -= 1;
            if (k.IsKeyDown(DownKey)) MoveIntent.Y += 1;
            if (k.IsKeyDown(RightKey)) MoveIntent.X += 1;
        }
    }
}
