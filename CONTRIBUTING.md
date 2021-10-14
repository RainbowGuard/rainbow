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

The Discord project reflects this model, with most interactions happening through [slash commands](https://github.com/RainbowGuard/rainbow/tree/main/src/Rainbow.Discord/Internal/SlashCommands) and using as little state as possible.
Slash command names are declared in a [separate file](https://github.com/RainbowGuard/rainbow/blob/main/src/Rainbow.Discord/Internal/SlashCommandNames.cs) in order
to minimize human error between writing a slash command and [registering it](https://github.com/RainbowGuard/rainbow/blob/d92735979e9778ecaeb76fd327500cc906249a8f/src/Rainbow.Discord/Internal/Events/ClientReady.cs#L16-L29).

### I don't get these APIs.
Neat, work on the core project. All you need to know is:
 - `Actions` describe what the system needs to do.
 - Each `Action` takes a corresponding `Command` that has the `Action`'s parameters.
 - Actions take interfaces in their constructors. Don't worry about interface implementations - if you need a piece of functionality for an action, just create an interface representing that functionality in the project root and someone else will implement it.
 - The `CommandHandler` class actually runs the actions. Create a `Handle()` overload for your command and action.
