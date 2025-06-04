using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Jolpango.UI.Elements.Containers
{
    public enum Orientation
    {
        Horizontal,
        Vertical
    }
    public class UIStackPanel : UIContainer
    {
        public Orientation Orientation { get; set; }
        public float Gap { get; set; }
        public override void RecalculateLayout()
        {
            var offset = Vector2.Zero;
            foreach(var child in children)
            {
                child.Position += Position + offset;
                offset += Orientation == Orientation.Horizontal
                    ? new Vector2(child.Size.X + Gap, 0)
                    : new Vector2(0, child.Size.Y + Gap);

            }
        }
    }
}
