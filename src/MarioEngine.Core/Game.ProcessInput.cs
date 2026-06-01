namespace MarioEngine.Core;

/// <summary>
/// Contains the <see cref="ProcessInput"/> method for the <see cref="Game"/> class.
/// Called every frame to handle input before updating game state.
/// </summary>
public partial class Game
{
    /// <summary>
    /// Called every frame before <see cref="Update"/> to process input.
    /// </summary>
    /// <param name="dt">Delta time in seconds for the current frame.</param>
    public virtual void ProcessInput(float dt)
    {
    }
}
