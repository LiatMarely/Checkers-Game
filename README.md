# ♟️ Checkers Game – C# .NET Windows Forms

A desktop checkers (draughts) game developed as part of an academic course using C# and Windows Forms.

Supports:
- 🎮 **Two-player mode**  
- 🤖 **Player vs Computer** (with random-move AI)

Designed using clean OOP principles, with clear separation between UI, logic, and game state.

---

## 🚀 Features

- 🕹️ **Game Modes**:  
  - Player vs Player  
  - Player vs Computer

- 📏 **Standard Rules**:  
  Diagonal movement, capturing, kinging, and turn enforcement

- 🤖 **Computer Opponent**:  
  Uses `RandomMoveStrategy` – selects valid or mandatory moves randomly

- 🪟 **Windows Forms UI**:  
  Real-time board updates and dialog messages

- 🧱 **Object-Oriented Architecture**:  
  Logic, game state, and move strategies in separate components

---

## 🧠 Technologies

- 💻 C#
- 🧩 .NET Framework 4.7.2  
- 🪟 Windows Forms  
- 🛠️ Visual Studio

---

## 📂 Project Structure (Simplified)
📁 FormUI
│   ├── Form1.cs
│   ├── FormBoardGame.cs
│   ├── ShowMessages.cs
│   └── Program.cs
📁 Logic
│   ├── Models/
│   │   ├── Board.cs, Move.cs, Piece.cs, Player.cs, Position.cs
│   ├── States/
│   │   ├── IGameState.cs, PlayerTurnState.cs, GameOverState.cs
│   ├── Strategies/
│   │   ├── IStrategy.cs, RandomMoveStrategy.cs
│   └── Game.cs


## 📦 How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/LiatMarely/Checkers-Game.git
2. Open the solution file .sln in Visual Studio
3. Build and run the project – the game window will open!

🎓 Developed as part of a C# .NET academic project – showcasing object-oriented design, Windows Forms, and basic AI strategy implementation.




