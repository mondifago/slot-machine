# Slot Machine

A console-based slot machine game built in C# (.NET 7).

## How to Play

The game starts you with a **$50 balance** and presents a 3×3 grid of random symbols (`a`, `b`, `c`). Choose a game mode, pay the entry cost, spin the reels, and win $40 for each matching line.

## Game Modes

| Mode | Cost | Win Condition |
|------|------|---------------|
| 1 | $10 | All matching rows |
| 2 | $10 | All matching columns |
| 3 | $20 | Matching rows + both diagonals |
| 4 | $20 | Matching columns + both diagonals |
| 5 | $50 | Any line (rows, columns, diagonals) + **$200 jackpot** if all rows and columns match |

Winning lines are highlighted in green after each spin.

## Requirements

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)

## Running the Game

```bash
git clone <repo-url>
cd slot_machine
dotnet run
```

## Project Structure

| File | Description |
|------|-------------|
| `Program.cs` | Entry point and main game loop |
| `SmLogic.cs` | Win calculation logic |
| `SmUiMethods.cs` | Console display and user input handling |
| `SmConstants.cs` | Game configuration constants |
| `SmEnum.cs` | `GameMode` enum |
