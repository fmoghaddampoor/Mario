namespace MarioEngine.Core;

/// <summary>
/// Contains the <see cref="Render"/> method for the <see cref="Game"/> class.
/// Called every frame to draw the current game state.
/// </summary>
public partial class Game
{
    /// <summary>
    /// Called every frame after <see cref="Update"/>.
    /// Override to render the current game state.
    /// </summary>
    /// <param name="interpolation">Interpolation factor (0-1) between fixed updates.</param>
    public virtual void Render(float interpolation)
    {
    }
}
