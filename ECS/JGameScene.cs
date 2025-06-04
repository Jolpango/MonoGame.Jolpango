using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.Input;
using MonoGame.Jolpango.UI;
using System;

namespace MonoGame.Jolpango.ECS
{
    public class JGameScene
    {
        private JEntityWorld entityWorld;
        private UIScene uiManager;
        private JServiceInjector serviceInjector;
        private JKeyboardInput keyboardInput;
        private JMouseInput mouseInput;
        private Game game;
        public JGameScene(Game game, JMouseInput mouseInput = null, JKeyboardInput keyboardInput = null)
        {
            entityWorld = new JEntityWorld();
            uiManager = new UIScene();
            serviceInjector = new JServiceInjector();
            this.mouseInput = mouseInput ?? new JMouseInput();
            this.keyboardInput = keyboardInput ?? new JKeyboardInput();
            this.game = game ?? throw new ArgumentNullException(nameof(game));
            RegisterService(this.mouseInput);
            RegisterService(this.keyboardInput);
            RegisterService(this.game.Content);
        }
        public void RegisterService<T>(T service)
        {
            serviceInjector.RegisterService(service);
        }
        public void SetPhysicsSystem(JPhysicsSystem physicsSystem)
        {
            entityWorld.SetPhysicsSystem(physicsSystem);
        }
        public void AddEntity(JEntity entity)
        {
            serviceInjector.InjectAll(entity);
            entityWorld.AddEntity(entity);
        }
        public void LoadContent()
        {
            entityWorld.LoadContent();
            uiManager.LoadContent();
        }
        public void Update(GameTime gameTime)
        {
            keyboardInput.Update();
            mouseInput.Update();
            uiManager.Update(gameTime);
            entityWorld.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            uiManager.Draw(spriteBatch);
            entityWorld.Draw(spriteBatch);
        }
        public void UnloadContent()
        {
            entityWorld.UnloadContent();
        }
    }
}
