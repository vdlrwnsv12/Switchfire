# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**Switchfire** is a Unity 3D game project in early development. It was initialized from the `com.unity.template.3d@8.1.1` template.

- **Unity version:** 2022.3.17f1 (LTS)
- **Target resolution:** 1920×1080
- **Input system:** Legacy Input Manager

## Opening & Running

Open the project in Unity Hub by pointing it to this directory. Unity will import assets on first open.

To open via CLI:
```sh
# macOS
/Applications/Unity/Hub/Editor/2022.3.17f1/Unity.app/Contents/MacOS/Unity -projectPath /Users/vdlrwnsv/Desktop/development/Unity/Game/Switchfire
```

To build headlessly (example for macOS standalone):
```sh
Unity -batchmode -quit -projectPath . -buildTarget StandaloneOSX -executeMethod BuildScript.Build -logFile build.log
```

To run tests via CLI:
```sh
Unity -batchmode -runTests -projectPath . -testPlatform EditMode -testResults results.xml -logFile test.log
```

## Project Structure

```
Assets/
  Scenes/
    SampleScene.unity   # Default scene (entry point)
Packages/
  manifest.json         # Package dependencies
ProjectSettings/        # Unity project settings (version control these)
```

## Key Packages

| Package | Version | Purpose |
|---|---|---|
| `com.unity.textmeshpro` | 3.0.6 | Text rendering |
| `com.unity.timeline` | 1.7.6 | Cinematic sequences |
| `com.unity.ugui` | 1.0.0 | UI system |
| `com.unity.visualscripting` | 1.9.1 | Visual scripting |
| `com.unity.feature.development` | 1.0.1 | Dev tools (profiler, etc.) |

## Git / Version Control

Ignore the `Library/`, `Temp/`, `Obj/`, `Build/`, and `Logs/` directories. Only commit `Assets/`, `Packages/`, `ProjectSettings/`, and `UserSettings/` (optional).

The project uses Unity Collaborate (`com.unity.collab-proxy` 2.12.4) — ensure you have the Plastic SCM / Unity Version Control client if you intend to use it, or switch to standard Git.
