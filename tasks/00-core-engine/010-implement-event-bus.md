# Task 010 — Implement Event Bus

## Description
Create a typed event bus for decoupled communication between systems.

## Steps
1. Create EventBus static class
2. Implement Subscribe<T>(Action<T> handler) returning subscription token
3. Implement Publish<T>(T event) to notify all subscribers
4. Implement Unsubscribe(SubscriptionToken token)
5. Support weak references to prevent memory leaks
6. Create base GameEvent class
7. Add logging for published events (Debug level)

## Files to Create
- src/MarioEngine.Core/Events/EventBus.cs
- src/MarioEngine.Core/Events/GameEvent.cs
- src/MarioEngine.Core/Events/SubscriptionToken.cs

## Acceptance Criteria
- Events can be published and received
- Subscribers don't cause memory leaks
- Events are logged at Debug level
