﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.ECS.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MonoGame.Jolpango.ECS
{
    public class JEntityWorld
    {
        private List<JEntity> entities = new();
        private JPhysicsSystem physicsSystem;

        public List<JEntity> Entities
        {
            get { return entities; }
        }

        public void SetPhysicsSystem(JPhysicsSystem physicsSystem)
        {
            this.physicsSystem = physicsSystem;
        }

        public void LoadContent()
        {
            foreach (JEntity entity in Entities)
            {
                entity.LoadContent();
            }
        }

        public void AddEntity(JEntity e)
        {
            entities.Add(e);
            var collider = e.GetComponent<JColliderComponent>();
            if (collider is null)
                return;
            try
            {
                physicsSystem.RegisterEntity(e);
            }
            catch
            {
                Debug.WriteLine("Unable to add entity to physics system: " + e.ToString());
            }
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            foreach (var e in entities) e.Update(gameTime);
            if(physicsSystem is not null)
            {
                physicsSystem.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var e in entities) e.Draw(spriteBatch);
        }
    }
}
