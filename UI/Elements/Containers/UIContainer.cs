using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonoGame.Jolpango.UI.Elements.Containers
{
    public class UIContainer : UIElement
    {
        protected List<UIElement> children;

        public override bool IsMouseOver(Vector2 mousePosition)
        {
            foreach (var child in children)
            {
                if (child.IsMouseOver(mousePosition)) return true;
            }
            return false;
        }

        public UIContainer()
        {
            children = new List<UIElement>();
        }

        public void AddChild(UIElement child)
        {
            child.Parent = this;
            children.Add(child);
            RecalculateLayout();
        }

        public void RemoveChild(UIElement child)
        {
            child.Parent = null;
            children.Remove(child);
            RecalculateLayout();
        }

        public virtual void RecalculateLayout()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(IsVisible)
            {
                foreach(var child in children)
                {
                    child.Draw(spriteBatch);
                }
            }
        }
    }
}
