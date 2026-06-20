# 🗺️ RoadAtlas Mapper

> A modern and user-friendly Windows application for extracting, processing, and generating map data for **Euro Truck Simulator 2** and **American Truck Simulator**.

![Platform](https://img.shields.io/badge/Platform-Windows-blue)
![Framework](https://img.shields.io/badge/.NET-WPF-purple)
![Status](https://img.shields.io/badge/Status-Active%20Development-green)

---

## 🎥 Development Preview

Watch the latest development preview on YouTube:

https://youtu.be/vhmuvfg-eTI

---

## 📖 About

RoadAtlas Mapper was created to simplify map data generation workflows through an intuitive graphical interface.

Instead of relying on command-line tools, users can manage the entire process from a modern Windows desktop application.

---

## ✨ Features

### Current Features

* ✅ Support for **Euro Truck Simulator 2**
* ✅ Support for **American Truck Simulator**
* ✅ Automatic loading of configured game paths
* ✅ Configurable mod and output directories
* ✅ Parser execution directly from the GUI
* ✅ Graph generation directly from the GUI
* ✅ One-click full pipeline execution
* ✅ Integrated log output window
* ✅ Windows Subsystem for Linux (WSL) integration
* ✅ Automatic generation of:

  * `europe-graph.json`
  * `usa-graph.json`

---

## ⚙️ Requirements

### Windows

* Windows 10 or Windows 11
* .NET Desktop Runtime (if not using a self-contained build)

### WSL Environment

RoadAtlas Mapper requires:

* Windows Subsystem for Linux (WSL)
* Node.js
* npm

Verify installation:

```bash
wsl --version
node --version
npm --version
```

---

## 🚀 Getting Started

### 1. Configure Paths

Select:

* Game installation directory
* Mod directory
* Output directory

### 2. Run Parser

The parser extracts and processes map data from the selected game and installed mods.

### 3. Run Generator

The generator creates the final graph file from the exported map data.

### 4. Run Full Pipeline

Execute the complete workflow with a single click:

```text
Parser
   ↓
Map Data Export
   ↓
Graph Generation
   ↓
Final Graph Output
```

---

## 📂 Output Files

### Euro Truck Simulator 2

```text
europe-graph.json
```

### American Truck Simulator

```text
usa-graph.json
```

---

## 🧪 Development Status

### Successfully Tested

* ✅ ETS2 Vanilla
* ✅ ATS Vanilla
* ✅ ETS2 Graph Generation
* ✅ ATS Graph Generation

### Planned Features

* 🎨 Enhanced UI/UX
* 🚀 One-click application updates
* 🔄 Real-time log streaming
* 🔄 Real-time log streaming
* 📊 Progress tracking
* 🔍 Automatic path detection
* 🎨 Enhanced UI/UX
* 📦 Mod profile management
* 🗺️ Advanced map visualization
* 🌍 GeoJSON export support

---

## 📸 Screenshots

Screenshots and additional previews will be added soon.

---

## 🙏 Acknowledgements

RoadAtlas Mapper builds upon the excellent open-source work provided by the truck simulation community.

Special thanks to **TruckerMudgeon** for creating and maintaining the map parsing and graph generation tools that make this project possible.

The parser and generator used by RoadAtlas Mapper are based on:

https://github.com/truckermudgeon/maps

RoadAtlas Mapper provides a Windows-based graphical interface designed to simplify and streamline the workflow for ETS2 and ATS map data generation.

---

## 👨‍💻 Author

**GorillaKeks**

RoadAtlas Mapper is an independent community project focused on making map data generation for ETS2 and ATS more accessible, efficient, and user-friendly.

---

## 📄 License

License information will be added in a future release.
