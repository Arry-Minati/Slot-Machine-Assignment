# Slot Machine Game

A 2D slot machine game built with **Unity and C#**, featuring animated reels, selectable bets, a credit system, symbol matching, and an interactive lever. The project includes a WebGL build that can be played in a web browser.

## Overview

The goal of this project was to create an interactive slot machine experience with smooth reel animations, clear symbol presentation, responsive controls, and organized gameplay logic.

Players select a bet, pull the lever to spin the reels, and check the resulting symbols. The game evaluates the result and updates the credit balance according to the implemented game logic.

## Features

- **Animated Reels** — Reels spin and display slot symbols.
- **Randomized Results** — Reel outcomes are generated using random selection.
- **Win Detection** — The game checks for matching symbols across the slots.
- **Bet Selection** — Choose from three betting options: 10G, 50G, and 100G.
- **Credit Management** — Track credits and account for the selected bet.
- **Interactive Lever** — Pull the lever to trigger a spin, with a visual animation between its normal and pulled states.
- **Bet Validation** — A bet must be selected before a spin can begin, and another bet must be selected before the next spin.
- **Betting Popup** — A dedicated menu provides betting options and an exit option.
- **Frame Rate Target** — The game targets 60 FPS.
- **WebGL Build** — Play the game through a browser using a local web server.

## Gameplay

1. Start the game.
2. Click the **BET** button to open the betting menu.
3. Select 10G, 50G, or 100G.
4. Click the lever to start the spin.
5. Wait for the reels to display their results.
6. Check the symbols and updated credit balance.
7. Select another bet before spinning again.

Matching symbols are evaluated by the game's win-checking logic.

## Controls

| Action | Control |
|---|---|
| Open betting menu | BET button |
| Select wager | 10G, 50G, or 100G button |
| Spin the reels | Click the lever |
| Exit from betting menu | Exit button |

## Technical Implementation

The project uses separate scripts for the main slot machine logic, betting menu interactions, and lever animation.

### Slot Machine Controller

`SlotMachineController.cs` manages the core gameplay, including:

- Bet selection and validation.
- Credit balance management.
- Reel spinning.
- Randomized symbol outcomes.
- Win-condition evaluation.
- Frame rate configuration.

### Lever Controller

`SlotLever.cs` handles the lever interaction. It checks whether a valid bet has been selected, prevents overlapping lever animations, switches between the normal and pulled lever visuals, and triggers the slot machine spin.

The lever logic is attached to a separate controller GameObject so its coroutine can continue while the lever's visual objects are switched on and off.

### Betting Menu

`BettingMenuController` handles the betting popup and its button interactions. The player must select a wager before the lever can initiate a spin.

## Project Structure

```text
Slot-Machine-Assignment/
├── Assets/
│   ├── Animations/
│   ├── Prefabs/
│   ├── Scenes/
│   │   └── SlotMachine.unity
│   ├── Scripts/
│   │   ├── SlotMachineController.cs
│   │   ├── SlotLever.cs
│   │   └── BettingMenuController.cs
│   ├── Sprites/
│   ├── UI/
│   └── Sounds/
├── Build/
│   └── WebGL/
├── Packages/
├── ProjectSettings/
└── README.md
```

*Note: Some folders may be empty or contain additional files depending on the final project contents.*

## How to Run the Game

### Play the WebGL Build

1. Clone or download this repository.
2. Open the project in Visual Studio Code.
3. Open the `Build/WebGL` folder.
4. Start a local web server, such as the **Live Server** extension in Visual Studio Code.
5. Launch the local URL provided by Live Server in your browser.

**Important:** Run the game through a web server instead of opening `index.html` directly as a local file.

The WebGL build uses disabled compression to avoid the compressed-file serving issue encountered during browser testing.

### Run in Unity

1. Open Unity Hub.
2. Add or open the downloaded project folder.
3. Open the project with a compatible Unity version.
4. Open `Assets/Scenes/SlotMachine.unity`.
5. Press **Play** in the Unity Editor.

## Development Approach

The project was developed with an emphasis on separating responsibilities and keeping player interactions straightforward.

- Core gameplay and reel results are managed independently from the lever's visual interaction.
- Betting controls are handled through a dedicated menu controller.
- Bet validation prevents spins from starting without a selected wager.
- The lever uses separate normal and pulled visual states to provide immediate feedback.
- The game targets 60 FPS for a consistent gameplay experience.
- The WebGL build was tested locally in a browser using Live Server.

## Technologies Used

- Unity
- C#
- Unity UI
- WebGL
- Visual Studio Code
- Git and GitHub

## Author

Developed as a Unity game development assignment.

## Repository

The repository contains the Unity project files and the WebGL build for browser testing.
