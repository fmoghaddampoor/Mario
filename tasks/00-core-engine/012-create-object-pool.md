# Task 012 — Create Object Pool

## Description
Implement a generic object pool to reduce allocations in hot paths.

## Steps
1. Create ObjectPool<T> where T: class, new()
2. Implement borrow/return pattern with Get() and Return()
3. Support configurable max pool size
4. Auto-expand pool up to max, then warn on overflow
5. Track pool usage stats (borrows, returns, created, discarded)
6. Add PooledObject<T> disposable wrapper
7. Create specialized pools: BulletPool, ParticlePool, SFXSourcePool

## Files to Create
- src/MarioEngine.Core/Pooling/ObjectPool.cs
- src/MarioEngine.Core/Pooling/PooledObject.cs

## Acceptance Criteria
- Pool returns existing instances when available
- New instances created when pool empty
- Returned instances are reset
- Stats track usage correctly
