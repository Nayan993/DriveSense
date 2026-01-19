# DriveSense 🚗🎮

[![Unity](https://img.shields.io/badge/Unity-2021.3%2B-black?logo=unity)](https://unity.com/)
[![Python](https://img.shields.io/badge/Python-3.9%2B-blue?logo=python)](https://www.python.org/)
[![MediaPipe](https://img.shields.io/badge/MediaPipe-Hand%20Tracking-green)](https://mediapipe.dev/)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

> AI-powered gesture-controlled racing game built with Unity and MediaPipe

**Developed for DEVJAM/14-01-2026** by [Nayan Mishra](https://github.com/Nayan993) and [Anuj Sahu](https://github.com/anujsahu8847)

---

## 📖 Table of Contents

- [About](#about)
- [Features](#features)
- [Demo](#demo)
- [Technology Stack](#technology-stack)
- [Architecture](#architecture)
- [Installation](#installation)
- [Usage](#usage)
- [Game Mechanics](#game-mechanics)
- [ML Integration](#ml-integration)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [Roadmap](#roadmap)
- [Team](#team)
- [License](#license)

---

## 🎯 About

**DriveSense** is an innovative desktop racing game where players control their vehicle using **AI-powered hand gesture recognition** through a webcam. Navigate through traffic, avoid collisions, and race to achieve the highest distance using nothing but your hands!

### Key Highlights
- 🎮 **Gesture-based controls** - Drive with natural hand movements
- 🚗 **Interactive garage** - Multiple cars to choose from
- 📊 **Real-time stats** - Speed and distance tracking
- 💥 **Collision system** - Dynamic game over mechanics
- 🤖 **ML-powered** - MediaPipe hand tracking integration

---

## ✨ Features

### 🎮 Gameplay
- [x] Gesture-controlled driving via webcam
- [x] Endless runner-style racing
- [x] Traffic avoidance mechanics
- [x] Real-time speed and distance tracking
- [x] Collision detection and game over system

### 🚗 Garage System
- [x] Multiple car selection
- [x] 3D car preview with rotation
- [x] Vehicle stats display
- [x] Smooth UI transitions

### 📊 UI Components
- [x] Main menu
- [x] Garage interface
- [x] In-game HUD (speed, distance, gesture feedback)
- [x] Game over panel with stats
- [x] Retry and navigation options

### 🤖 AI Features
- [x] Real-time hand gesture recognition
- [x] Low-latency control mapping
- [x] Gesture calibration system
- [x] Visual feedback overlay

---

## 🛠 Technology Stack

### Game Engine
- **Unity** 2021.3 LTS or higher
- **C#** for game logic and mechanics

### AI/ML Backend
- **Python** 3.9+
- **MediaPipe** - Hand landmark detection
- **OpenCV** - Camera input processing
- **NumPy** - Mathematical operations
- **Socket.IO** - Real-time communication

### Additional Tools
- **Unity ProBuilder** - Environment modeling
- **TextMeshPro** - UI text rendering
- **Cinemachine** - Camera management

---

## 🏗 Architecture

### System Overview
```
┌─────────────────┐         ┌──────────────────┐         ┌─────────────────┐
│   Webcam Feed   │────────▶│  Python Backend  │────────▶│  Unity Client   │
│                 │         │  (MediaPipe)     │         │  (Game Logic)   │
└─────────────────┘         └──────────────────┘         └─────────────────┘
                                     │                            │
                                     │                            │
                                     ▼                            ▼
                            ┌─────────────────┐         ┌─────────────────┐
                            │ Gesture Data    │         │ Car Controller  │
                            │ (JSON/Socket)   │         │ Physics Engine  │
                            └─────────────────┘         └─────────────────┘
```

### Data Flow
```
Camera Input → Hand Detection → Landmark Extraction → Gesture Classification
      ↓
Socket Server (Python) → JSON Data → Socket Client (Unity) → Car Controls
```

---

## 📦 Installation

### Prerequisites

- [Unity Hub](https://unity.com/download) with Unity 2021.3 LTS+
- [Python 3.9+](https://www.python.org/downloads/)
- Webcam (built-in or external)
- Git

```

## 📁 Project Structure
```
DriveSense/
│
├── Unity-Project/                 # Unity game project
│   ├── Assets/
│   │   ├── Scenes/
│   │   │   ├── MainMenu.unity
│   │   │   ├── Garage.unity
│   │   │   └── GamePlay.unity
│   │   ├── Scripts/
│   │   │   ├── Core/
│   │   │   │   ├── GameManager.cs
│   │   │   │   └── SceneLoader.cs
│   │   │   ├── Gameplay/
│   │   │   │   ├── CarController.cs
│   │   │   │   ├── TrafficSpawner.cs
│   │   │   │   └── CollisionHandler.cs
│   │   │   ├── Input/
│   │   │   │   ├── GestureInputManager.cs
│   │   │   │   └── SocketClient.cs
│   │   │   ├── UI/
│   │   │   │   ├── UIManager.cs
│   │   │   │   ├── GarageManager.cs
│   │   │   │   └── GameOverPanel.cs
│   │   │   └── Utils/
│   │   │       ├── StatsTracker.cs
│   │   │       └── DataPersistence.cs
│   │   ├── Prefabs/
│   │   │   ├── Cars/
│   │   │   │   ├── SportsCar.prefab
│   │   │   │   ├── SUV.prefab
│   │   │   │   └── Sedan.prefab
│   │   │   ├── Traffic/
│   │   │   │   └── TrafficVehicle.prefab
│   │   │   └── UI/
│   │   │       └── HUDCanvas.prefab
│   │   ├── Materials/
│   │   ├── Textures/
│   │   └── Audio/
│   ├── Packages/
│   └── ProjectSettings/
│
├── Python-Backend/                # ML gesture recognition
│   ├── src/
│   │   ├── gesture_recognition.py
│   │   ├── socket_server.py
│   │   ├── calibration.py
│   │   └── utils.py
│   ├── models/                    # Trained models (if any)
│   ├── config/
│   │   └── settings.json
│   ├── requirements.txt
│   └── README.md
│
├── docs/                          # Documentation
│   ├── screenshots/
│   ├── architecture.md
│   └── api-reference.md
│
├── .gitignore
├── LICENSE
└── README.md
```

## 👥 Team

| Name | Role | GitHub | Contributions |
|------|------|--------|---------------|
| **Nayan Mishra** | Unity Developer | [@Nayan993](https://github.com/Nayan993) | Game mechanics, UI/UX, Physics |
| **Anuj Sahu** | ML Engineer | [@anujsahu8847](https://github.com/anujsahu8847) | Gesture recognition, Python backend |

---

## 🙏 Acknowledgments

- [MediaPipe](https://mediapipe.dev/) for hand tracking solutions
- [Unity Technologies](https://unity.com/) for the game engine
- Weekend of Code Hackathon organizers
- All contributors and testers

---

## 📞 Contact

**Project Repository**: [https://github.com/Nayan993/DriveSense](https://github.com/Nayan993/DriveSense)

**Issues & Bugs**: [Submit an issue](https://github.com/Nayan993/DriveSense/issues)

**Questions**: Reach out via GitHub Discussions

---

<div align="center">

**Built with ❤️ for Weekend of Code Hackathon**

⭐ Star us on GitHub — it motivates us a lot!

</div>