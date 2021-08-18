# Cabinet \[Unity Packages\]
***Handle your local cabinets network!***\
\
These packages were designed to be plugged into Unity developed games (mainly jam-made) so they could be put onto the cabinets in the game room at a game development school.

## Packages

### Core
Main dependency for all other packages. Provides SQLite database communication and settings remote synchronization. Start from `DatabaseTable` template class to create your table.

### Logging
Track the usage of your cabinets adding entries to a log table whenever you need to.

### Score Management
Wanna add some competition? This package lets you reflect every score reached in your games among all the cabinets. No more loss of precious scores!

## Installation
Requires Unity **2019.4** or higher.

### Local package
[Clone](https://docs.github.com/en/github/creating-cloning-and-archiving-repositories/cloning-a-repository-from-github/cloning-a-repository#cloning-a-repository-to-github-desktop) repo and install each package individually as a [local package](https://docs.unity3d.com/Manual/upm-ui-local.html).
Useful if you want to be up-to-date with repo and freely modify a package.

### Git URL
Using the [Git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html) you can install an exact version directly via Unity Package Manager.
- [Core v0.1.0](https://github.com/alessandrobrizio/Cabinet.git?path=/Core#v0.1.0)
- [Logging v0.1.0](https://github.com/alessandrobrizio/Cabinet.git?path=/Logging#v0.1.0)
- [Score Management v0.1.0](https://github.com/alessandrobrizio/Cabinet.git?path=/ScoreManagement#v0.1.0)
