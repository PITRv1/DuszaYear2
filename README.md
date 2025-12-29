General stuff for the team:
- Naming convention:
# Step 1. Use any static linters, that enforce conventions. Writing stuff into readmes will break after a while.
# Step 2. Setup github actions, which auto runs such linters at every commit.
# Step 3. PRs cannot merged (set it at github) till the branch has any build/test issues unresolved.
So just to keep things organised during development I thought it would be good to have a naming convension to keep scripts and assets consistent throughout the project

1. Use snake_case for => variable names, function names, file names (such as scenes and scripts and resources)
2. Use camelCase for => signal names
3. Use PascalCase for => class, singeltons

Trello:
    https://trello.com/b/anyCp2yv/woryn


# | Logbook |
# Instead of manually writing logs like this, use an another github action that auto generates ON PR MERGE a CHANGELOG.md file and it will keep it up2date through commits. This requires clear, [conventional](https://www.conventionalcommits.org/en/v1.0.0/) github commits. Post-commit hooks can also enforce if a commit complies with the conventional commit rules.
Increment with a new line if something notable happens

2025.07.28 - Repo created: current game is a Wizard themed card game with mechanics from Buckshot rulett and Liar's bar. 

2025.09.26 - Project idea reworked, project preperations started

2025.12.06 - Dani made the backend for the secound time for the műhely and he didn't sleep sh#t
