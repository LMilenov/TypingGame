# Typing Master Game ⌨️

A console-based typing speed tester with multiple difficulty levels, built with C#.

## ✨ Features

- **3 Difficulty Modes**:
  - 🟢 Easy - Short everyday sentences (8-12 words)
  - 🟡 Medium - Technical programming terms (12-18 words)
  - 🔴 Hard - Complex computer science concepts (18-25 words)

- **Smart Feedback**:
  - Color-coded error highlighting
  - Missing character indicators
  - Session statistics tracking

## 🛠️ Installation

1. **Prerequisites**:
   - .NET 6.0+ Runtime
   - Visual Studio 2022 (optional)

2. **Run from source**:
   ```bash
   git clone https://github.com/LMilenov/TypingGame.git
   cd TypingGame
   dotnet run

   # Typing Master Game ⌨️

📊 Scoring System
Component	Calculation
Base Score	Easy:100 Medium:200 Hard:350
Error Penalty	-5 points per incorrect character
Speed Bonus	+10 points per second saved
Final Score	(Base - Errors) + Speed Bonus
Example:
Medium difficulty with 3 errors in 15 seconds →
(200 - 15) + (50/15*10) ≈ 185 + 33 = 218 points


📝 Credits
Developed by Lyudmil Milenov
Created as part of programming practice
Inspired by classic typing tutors

⚖️ License
MIT License - Free for educational and personal use

