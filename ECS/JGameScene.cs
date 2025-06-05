﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.Input;
using MonoGame.Jolpango.UI;
using MonoGame.Jolpango.UI.Elements;
using System;

namespace MonoGame.Jolpango.ECS
{
    public class JGameScene
    {
        private JEntityWorld entityWorld;
        private UIManager uiManager;
        private JServiceInjector serviceInjector;
        private JKeyboardInput keyboardInput;
        private JMouseInput mouseInput;
        private Game game;
        public bool IsLoaded { get; private set; } = false;
        public bool IsInjected { get; private set; } = false;
        public JGameScene(Game game, JMouseInput mouseInput = null, JKeyboardInput keyboardInput = null)
        {
            entityWorld = new JEntityWorld();
            uiManager = new UIManager();
            serviceInjector = new JServiceInjector();
            this.mouseInput = mouseInput ?? new JMouseInput();
            this.keyboardInput = keyboardInput ?? new JKeyboardInput();
            this.game = game ?? throw new ArgumentNullException(nameof(game));
            RegisterService(this.mouseInput);
            RegisterService(this.keyboardInput);
            RegisterService(this.game.Content);
        }
        public virtual void RegisterService<T>(T service)
        {
            serviceInjector.RegisterService(service);
        }
        public virtual void SetPhysicsSystem(JPhysicsSystem physicsSystem)
        {
            entityWorld.SetPhysicsSystem(physicsSystem);
        }
        public virtual void AddEntity(JEntity entity)
        {
            if (IsInjected)
                serviceInjector.InjectAll(entity);
            if (IsLoaded)
                entity.LoadContent();
            entityWorld.AddEntity(entity);
        }
        public virtual void AddUIElement(UIElement element)
        {
            uiManager.AddElement(element);
        }
        public virtual void LoadContent()
        {
            IsLoaded = true;
            InjectAllServices();
            entityWorld.LoadContent();
            uiManager.LoadContent();
        }
        public virtual void Update(GameTime gameTime)
        {
            keyboardInput.Update();
            mouseInput.Update();
            uiManager.Update(gameTime);
            entityWorld.Update(gameTime);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            DrawEntityWorld(spriteBatch);
            DrawUI(spriteBatch);
        }
        public virtual void UnloadContent()
        {
            entityWorld.UnloadContent();
        }

        private void InjectAllServices()
        {
            IsInjected = true;
            serviceInjector.InjectAll(entityWorld.Entities);
            serviceInjector.Inject(uiManager);
        }

        private void DrawUI(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            uiManager.Draw(spriteBatch);
            spriteBatch.End();
        }
        private void DrawEntityWorld(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            entityWorld.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
