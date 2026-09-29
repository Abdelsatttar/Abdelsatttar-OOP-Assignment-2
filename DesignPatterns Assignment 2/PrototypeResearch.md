# Prototype Pattern Research

## What problem does Prototype solve?

Prototype is useful when creating a new object is expensive or when client code should create copies without knowing the concrete class. Instead of constructing a new object from scratch, we copy an existing configured object.

## What is a prototype?

A prototype is an existing object that is kept as a template. A clone is created from that object when another similar object is needed.

## What does Clone() do?

Clone() creates a new object with the same relevant state as the prototype. In this lab, Clone() is implemented by each concrete enemy, while the client only works with the Enemy base type.

## Shallow copy vs. deep copy

A shallow copy creates a new top-level object but keeps references to the same nested reference-type objects. That means changing a shared Weapon or list can affect both objects.

A deep copy also creates independent copies of the nested reference objects. In this lab, the cloned Enemy gets a new Weapon and a new Abilities list, so changing the clone does not change the prototype.

## Sources

- Refactoring.Guru — Prototype in C#: https://refactoring.guru/design-patterns/prototype/csharp/example
- Microsoft Learn — ICloneable.Clone: https://learn.microsoft.com/dotnet/api/system.icloneable.clone
- Microsoft Learn — Object.MemberwiseClone: https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone
