# Contributing
Thanks for helping out! Let's take a moment to talk about project (or rather, solution) structure.

## Solution structure
The solution is broken down into a number of different projects, with dependencies between projects described with project references.
Combined with marking most implementations as internal, this prevents spaghettness by mostly restricting projects from accessing implementations
outside of themselves.

`Rainbow.Core` takes this to an extreme, referencing no projects and exposing only interfaces, exceptions, and some
data object classes. Everything within the core project is business logic, and should not reference any external APIs for any reason. Anything
that uses an external API should have its implementation located in a different appropriate project and implement an interface from the core
project.

### So how do things get done?
[Commands](https://github.com/RainbowGuard/rainbow/tree/main/src/Rainbow.Core/Commands) and [actions](https://github.com/RainbowGuard/rainbow/tree/main/src/Rainbow.Core/Internal/Actions).
Actions take interfaces of services in their constructors, and use interface methods to do things. If external services
want the core to do something, they request the `CoreService` instance and use its `HandleCommand` method to indirectly execute an action.
