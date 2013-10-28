using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using DigitalRune.Game.Input;
using DigitalRune.Game.UI;
using DigitalRune.Game.UI.Controls;
using DigitalRune.Game.UI.Rendering;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRunesInput
{
    class InputDemoComponent : DrawableGameComponent
    {
        private readonly IInputService _inputService;
        private readonly IUIService _uiService;
        private readonly IUIRenderer _uiRenderer;

        private UIScreen _screen;

        public InputDemoComponent(Game game)
          : base(game)
        {
          _inputService = (IInputService)game.Services.GetService(typeof(IInputService));
          _uiService = (IUIService)game.Services.GetService(typeof(IUIService));
          _uiRenderer = (IUIRenderer)game.Services.GetService(typeof(IUIRenderer));
        }

        public override void Initialize()
        {
            AddMappedKeyCommand(Commands.BACK, Keys.Escape, PressType.Press);
            AddMappedMouseButtonCommand(Commands.ENTER, MouseButtons.Left, PressType.Press);

            CreateScreen();
           
            base.Initialize();
        }

        private void CreateScreen()
        {
            _screen = new UIScreen("Menu", _uiRenderer);
            {
                var stackPanel = new StackPanel { Margin = new Vector4F(40) };
                {
                    stackPanel.Children.Add(new TextBlock
                    {
                        Text = "- Press escape to quit the demo.",
                        Margin = new Vector4F(4),
                        Foreground = Color.White
                    });

                    stackPanel.Children.Add(new TextBlock
                    {
                        Text = "- Click the mouse to open an overlay window.",
                        Margin = new Vector4F(4),
                        Foreground = Color.White
                    });
                }
                _screen.Children.Add(stackPanel);
            }
            _uiService.Screens.Add(_screen);
        }

        protected override void Dispose(bool disposing)
        {
            _uiService.Screens.Remove(_screen);

            base.Dispose(disposing);
        }

        public override void Update(GameTime gameTime)
        {
            if (!_inputService.IsKeyboardHandled &&  !_inputService.IsMouseOrTouchHandled)
            {
                if (_inputService.Commands[Commands.BACK].Value > 0)
                {
                    this.Game.Exit();
                }

                if (_inputService.Commands[Commands.ENTER].Value > 0)
                {
                    BlockingWindow window = new BlockingWindow();
                    window.Show(_screen);
                }

                _inputService.IsKeyboardHandled = true;
                _inputService.IsMouseOrTouchHandled = true;
            }

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _screen.Draw(gameTime);

            base.Draw(gameTime);
        }

        #region Helpers

        private void AddMappedKeyCommand(String name, Keys key, PressType pressType)
        {
            ConfigurableInputCommand command = new ConfigurableInputCommand(name);
            InputMapping mapping = new InputMapping();
            mapping.PositiveKey = key;
            mapping.PressType = pressType;
            command.PrimaryMapping = mapping;
            _inputService.Commands.Add(command);
        }

        private void AddMappedMouseButtonCommand(String name, MouseButtons button, PressType pressType)
        {
            ConfigurableInputCommand command = new ConfigurableInputCommand(name);
            InputMapping mapping = new InputMapping();
            mapping.PositiveMouseButton = button;
            mapping.PressType = pressType;
            command.PrimaryMapping = mapping;
            _inputService.Commands.Add(command);
        }

        #endregion

    }
}
