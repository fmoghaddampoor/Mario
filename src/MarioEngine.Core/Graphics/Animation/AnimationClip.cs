namespace MarioEngine.Core.Graphics.Animation;

using System.Collections.Generic;

/// <summary>
/// Defines a named animation clip consisting of timed frames.
/// Can be looped or play once. Events can fire at specific frames.
/// </summary>
public sealed class AnimationClip
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AnimationClip"/> class.
    /// </summary>
    /// <param name="name">Animation name (e.g. "mario_run").</param>
    /// <param name="frames">Ordered list of animation frames.</param>
    /// <param name="loop">Whether the animation loops.</param>
    public AnimationClip(string name, IReadOnlyList<AnimationFrame> frames, bool loop = true)
    {
        Name = name;
        Frames = frames;
        Loop = loop;
    }

    /// <summary>Gets the animation name.</summary>
    public string Name { get; }

    /// <summary>Gets the ordered list of frames.</summary>
    public IReadOnlyList<AnimationFrame> Frames { get; }

    /// <summary>Gets a value indicating whether the animation loops.</summary>
    public bool Loop { get; }

    /// <summary>Gets the total duration of all frames in seconds.</summary>
    public float TotalDuration
    {
        get
        {
            float total = 0;
            for (var i = 0; i < Frames.Count; i++)
            {
                total += Frames[i].Duration;
            }

            return total;
        }
    }

    /// <summary>Gets the number of frames.</summary>
    public int FrameCount => Frames.Count;
}
