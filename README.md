# Space Rewritten

![Unity](https://img.shields.io/badge/Unity-6.4-black)
![Status](https://img.shields.io/badge/status-playable%20prototype-orange)
![License](https://img.shields.io/badge/license-all%20rights%20reserved-red)

**Space Rewritten** is a Unity space survival prototype about staying alive aboard a failing ship after becoming stranded in deep space.

The player must manage limited fuel, oxygen, hunger, fatigue, ship systems, and a mining drone while trying to gather enough resources to produce thruster fuel and reach a nearby station.

This is an old personal project originally started around 2019, later revived and upgraded for modern Unity. It is rough, experimental, and still being actively reworked.

---

## Current Gameplay

You are lost in space with no fuel.

To survive, you must use a drone to gather materials from nearby debris. These materials can be processed into food, generator fuel, upgrades, and eventually thruster fuel.

The main goal is to keep the ship operational long enough to craft enough thruster fuel and move on to a station.

---

## Core Loop

1. Check the ship’s current status.
2. Send the drone to gather materials.
3. Choose which materials to prioritise.
4. Craft food, generator fuel, and thruster fuel.
5. Maintain oxygen, power, hunger, and fatigue.
6. Repair or respond to ship system failures.
7. Produce enough thruster fuel to escape.

---

## Materials

| Material      | Purpose                                                  |
| ------------- | -------------------------------------------------------- |
| **Satonium**  | Used to craft food products.                             |
| **Fuelium**   | Used to create generator fuel for oxygen and ship power. |
| **Thrustium** | Rare material used to create thruster fuel.              |

---

## Drone System

The drone is used to collect materials while the player remains aboard the ship.

Current drone features include:

* Material collection
* Mining mode selection
* Battery management
* Drone damage
* Individual drone part condition
* Upgradeable efficiency and performance

The drone can currently prioritise different collection modes:

| Mode                  | Description                                    |
| --------------------- | ---------------------------------------------- |
| **Everything Mode**   | Collects all available materials.              |
| **Satonium Priority** | Prioritises food-related material gathering.   |
| **Fuelium Priority**  | Prioritises generator fuel material gathering. |

---

## Ship Systems

The ship has several systems the player must monitor and maintain.

Current systems include:

* Generator fuel
* Oxygen generation
* Ship battery
* Hunger
* Fatigue
* Lights
* Random ship errors
* Physical generator refuelling
* Ship status displays
* Interactive control screens

When the generator fails or runs out of fuel, ship systems such as oxygen and lighting are affected. The player must physically refuel the generator using canisters and respond to issues inside the ship.

---

## Ship Areas

### Helm

The helm contains the main ship control screens, including drone controls, status displays, crafting/shop menus, and navigation-related systems.

### Mid Room

The mid room acts as the main survival and utility area, containing systems such as the fabricator, equipment, posters, and access to other ship areas.

### Engine Room

The engine room contains the generator, fuel systems, switches, and emergency response equipment. The player must go here to physically refuel the generator and respond to system problems.

---

## Current Features

* First-person ship exploration
* Drone mining system
* Drone battery and damage systems
* Individual drone part condition
* Material banking
* Crafting and resource conversion
* Hunger, fatigue, and oxygen management
* Generator fuel system
* Physical generator refuelling with canisters
* Ship status screens
* Upgrade system
* Random ship system errors
* Interactive light and power systems
* Settings menu
* Save/load support
* Basic ship interior with functional rooms

---

## Development Status

This project is a playable prototype.

It is not polished, balanced, or content-complete. Many systems are functional but still rough, and several areas are being reworked as the project is modernised.

---

## Planned Improvements

Planned or considered improvements include:

* A clearer destination and launch screen
* Better thruster fuel progression
* More meaningful drone mission choices
* More physical repair interactions
* Better ship ambience and feedback
* Improved UI presentation
* More readable ship warnings
* Expanded crafting and upgrade progression
* Better room purpose and navigation
* More polished models and ship layout
* A clearer ending or station transition

---

## Project Notes

This is a personal learning and development project.

The goal is not currently to create a polished commercial game, but to continue developing an old prototype into something more playable, readable, and fun.

---

## License

This repository is public for viewing, portfolio, and development reference purposes only.

All original code, assets, models, scenes, writing, UI, gameplay systems, and project files are copyright © 2026 Nathan Douglas. All rights reserved.

No part of this project may be copied, modified, redistributed, sublicensed, or used in another project without explicit written permission from the copyright holder.

Third-party assets, packages, tools, libraries, and plugins remain subject to their own respective licenses.
