# Task 013 — Create Disposable Base Class

## Description
Implement a base class for proper unmanaged resource cleanup.

## Steps
1. Create DisposableBase abstract class
2. Implement IDisposable with standard Dispose pattern
3. Add DisposeManaged() and DisposeUnmanaged() virtual methods
4. Add ThrowIfDisposed() guard method
5. Add finalizer as safety net
6. Add logging for dispose operations

## Files to Create
- src/MarioEngine.Core/DisposableBase.cs

## Acceptance Criteria
- Managed and unmanaged resources cleaned up properly
- Double dispose is safe
- Finalizer catches missing Dispose calls
- Logs warn on finalizer invocation (missed Dispose)
