# Kick Ball Game

Dou Di Zhu is a Unity-based project utilizing free assets and AI sprites. All external assets used in the game can be found in:

```
./Assets/Resources/Packages
```

At the moment, the project contains the basic mechanics of laying out cards on the gaming table, as well as the basics for working on the project.

## Core Components

### Main Scripts

- `Scripts/Game/GameManager` - The primary scene object that controls and manages key events, manages cards in deck and main game rules.
- `Scripts/GameElements/HandManager` - Manages cards and  actions in players hand.
- `Scripts/GameElements/TableManager` - Manages cards and  actions on a table.
- `Scripts/Game/IngameMenuManager` - Contains scripts for main user settings and activating panels in ingame IU.

### Prefabs

- `Cards` - There are many prefab of each card that used to instantiate cards in game field.

## Scenes

The project consists one main scene:
**Game Field**

## WebGL Build Settings

To ensure proper WebGL functionality across all devices, the following settings should be applied:

### Canvas Settings

- Each **Canvas** should have a reference resolution of **1920x1080**.
- Textures should be in **Sprite format** and **Override for WebGL** enabled.
- Large textures should not exceed **5MB** and should be pre-compressed in an image editor.
- Texture format: **.png**

### Player Settings

```
Resolution and Presentation:
- Resolution: 1920x1080

Other:
- Color Space: Gamma
- MSAA Fallback: Downgrade
- Auto Graphics API: Disabled
- Graphics API: WebGL2 and WebGL1
- Texture Compression Format: ASTC
- Quality: Normal
- Lightmap Streaming: Enabled
- Default Chunk Size: 16MB
- Strip Engine Code: High
- Optimize Mesh Data: Enabled
- Texture MipMap Stripping: Disabled (can be tested)

Publishing:
- Compression Format: Disabled
- Data Caching: Enabled
- Initial Memory Size: 32MB
- Growth Mode: Geometric
- Power Reference: High Performance
- Maximum Memory Size: 2048MB
- Step: 0.2
- Cap: 96
```

### Quality Settings for WebGL

- **Medium**

## WebGL Index Template for Telegram Mini Apps

To ensure proper functionality within Telegram Mini Apps, replace `index.html` with the following template:

```html
<!DOCTYPE html>
<html lang="en-us">
<head>
    <meta charset="utf-8">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <title>Unity WebGL Player | Kick Ball Project</title>
    <link rel="shortcut icon" href="TemplateData/favicon.ico">
    <link rel="stylesheet" href="TemplateData/style.css">
    <link rel="manifest" href="manifest.webmanifest">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no, orientation=landscape">
    <script src="https://telegram.org/js/telegram-web-app.js"></script>
    <style>
        body { overflow: hidden; margin: 0; padding: 0; }
        #unity-container { position: fixed; width: 100vw; height: 100vh; }
        #unity-canvas { width: 100% !important; height: 100% !important; }
    </style>
</head>
<body>
    <div id="unity-container">
        <canvas id="unity-canvas" tabindex="-1"></canvas>
        <div id="unity-loading-bar">
            <div id="unity-logo"></div>
            <div id="unity-progress-bar-empty">
                <div id="unity-progress-bar-full"></div>
            </div>
        </div>
        <div id="unity-warning"></div>
    </div>
    <script>
        window.addEventListener("load", function () {
            if (window.Telegram && Telegram.WebApp) {
                Telegram.WebApp.expand();
                setTimeout(() => Telegram.WebApp.expand(), 1000);
            }
            if ("serviceWorker" in navigator) {
                navigator.serviceWorker.register("ServiceWorker.js");
            }
        });
    </script>
</body>
</html>
```

Replace `PROJECT_FOLDER_NAME` with the actual build folder name.

## Deployment

### Backend

- Located in: `/Builds` along with the latest build.
- Final deployment path: `/var/www/DouDiZhu/backend/`

### Unity Frontend

- Final deployment path: `/var/www/DouDiZhu/`
- If paths need to be changed, update `app.py` accordingly.

## Server Requirements

To run the project on a Linux server, ensure the following are installed:

- **Python**
- **Aiogram 2.x** (Aiogram 3.x is not supported)

---

This document serves as a structured README for better project understanding, setup, and deployment.




