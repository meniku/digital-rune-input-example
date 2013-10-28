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
  // A window that blocks just every input besides BACK
  public class BlockingWindow : Window
  {
    public BlockingWindow()
    {
        Title = "Some Overlayed Window";
      Width = 320;
      Height = 240;
      X = 220;
      Y = 140;
      CanResize = true;

      this.Content = new TextBlock
      {
          Text = "Press escape to go back to the main screen.",
          Margin = new Vector4F(4),
          Foreground = Color.White
      };
    }

    protected override void OnUpdate(System.TimeSpan deltaTime)
    {
        //Set the is*Handled flags on the InputService to true, this will prevent the InputDemoComponent from quitting the game when we press the BACK-Button.
        this.InputService.IsMouseOrTouchHandled = true;
        this.InputService.IsKeyboardHandled = true;

        if (this.InputService.Commands[Commands.BACK].Value > 0)
        {
            this.Close();
        }

        base.OnUpdate(deltaTime);
    }
  }
}
