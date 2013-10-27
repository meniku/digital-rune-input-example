using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using DigitalRune.Game.Input;
using DigitalRune.Game.UI;
using DigitalRune.Game.UI.Controls;
using DigitalRune.Game.UI.Rendering;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRunesInput
{
    class InputDemoComponent : DrawableGameComponent
    {
        private const String BACK_COMMAND = "back";

        private readonly IInputService _inputService;
        private readonly IUIService _uiService;

        public InputDemoComponent(Game game)
          : base(game)
        {
          // Get the services that this component needs regularly.
          _inputService = (IInputService)game.Services.GetService(typeof(IInputService));
          _uiService = (IUIService)game.Services.GetService(typeof(IUIService));
        }

        public override void Initialize()
        {
            AddMappedKeyboardCommand(BACK_COMMAND, Keys.Escape);

            base.Initialize();

        }

        public override void Update(GameTime gameTime)
        {
            if (!_inputService.IsKeyboardHandled)
            {
                if (_inputService.Commands[BACK_COMMAND].Value > 0)
                {
                    this.Game.Exit();
                }
            }

            base.Update(gameTime);
        }

        #region Helpers

        private void AddMappedKeyboardCommand(String name, Keys key)
        {
            ConfigurableInputCommand command = new ConfigurableInputCommand(name);
            InputMapping mapping = new InputMapping();
            mapping.PositiveKey = key;
            command.PrimaryMapping = mapping;
            _inputService.Commands.Add(command);
        }

        #endregion

    }
}
