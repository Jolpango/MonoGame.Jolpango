﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.ECS
{
    public class JEntity
    {
        private Dictionary<Type, JComponent> components = new();
        public string Name { get; set; } = "Unnamed Entity";
        public HashSet<string> Tags {  get; set; }

        public void AddComponent<T>(T component) where T : JComponent
        {
            component.Parent = this;
            components[typeof(T)] = component;
            component.LoadContent();
        }

        public T GetComponent<T>() where T : JComponent
        {
            if (components.TryGetValue(typeof(T), out var c))
                return (T)c;

            // Fallback: search all components to find derived matches
            foreach (var comp in components.Values)
            {
                if (comp is T tComp)
                    return tComp;
            }
            return null;
        }
        public void Update(GameTime gameTime)
        {
            foreach (var c in components.Values) c.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var c in components.Values) c.Draw(spriteBatch);
        }
    }

}
