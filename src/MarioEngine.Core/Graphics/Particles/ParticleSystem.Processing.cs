namespace MarioEngine.Core.Graphics.Particles;

/// <summary>
/// Contains update and cleanup methods for <see cref="ParticleSystem"/>.
/// </summary>
public sealed partial class ParticleSystem
{
    /// <summary>
    /// Updates all alive particles. Applies gravity, interpolates color/size over lifetime.
    /// Removes dead particles. Call once per frame before rendering.
    /// </summary>
    /// <param name="dt">Delta time in seconds.</param>
    public void Update(float dt)
    {
        for (var i = _particles.Count - 1; i >= 0; i--)
        {
            var p = _particles[i];
            p.Lifetime -= dt;

            if (p.IsDead)
            {
                _particles.RemoveAt(i);
                continue;
            }

            p.Velocity += p.Gravity * dt;
            p.Position += p.Velocity * dt;

            var t = 1f - (p.Lifetime / p.InitialLifetime);
            p.Size = Lerp(p.StartSize, p.EndSize, t);
            p.Color = ColorLerp(p.StartColor, p.EndColor, t);

            _particles[i] = p;
        }
    }

    /// <summary>Removes all particles and resets accumulator.</summary>
    public void Clear()
    {
        _particles.Clear();
        _accumulator = 0f;
    }
}
