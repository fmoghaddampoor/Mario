# Task 014 — Create Coroutine System

## Description
Implement a lightweight coroutine system for timed/sequenced operations.

## Steps
1. Create CoroutineManager class
2. Support IEnumerator<ICoroutineInstruction> pattern
3. Implement instructions: WaitForSeconds, WaitForFrames, WaitUntil
4. Support parallel and sequential coroutine chains
5. Support coroutine cancellation
6. Update coroutines in Game.Update()
7. Add logging for coroutine start/stop

## Files to Create
- src/MarioEngine.Core/Coroutines/CoroutineManager.cs
- src/MarioEngine.Core/Coroutines/ICoroutineInstruction.cs
- src/MarioEngine.Core/Coroutines/WaitForSeconds.cs
- src/MarioEngine.Core/Coroutines/WaitForFrames.cs
- src/MarioEngine.Core/Coroutines/WaitUntil.cs

## Acceptance Criteria
- Coroutines execute after specified delay
- Multiple coroutines run in parallel
- Coroutines can be cancelled
- No memory allocations per frame (reusable instructions)
