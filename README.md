# sakura-prj

A simple Unity 2D grid-based simulation prototype with movable units and obstacle collision.

## Features
- Grid-based 2D environment (configurable width/height).
- Multiple units spawned on grid cells.
- Move selected unit one tile at a time with `WASD` or arrow keys.
- Switch active unit with `Tab`.
- Basic collision detection:
  - Units cannot move outside the grid.
  - Units cannot move into obstacle cells.
  - Units cannot move through or overlap other units.

## Unity Setup (Built-in 2D tools)
1. Create a new **2D Core** Unity project.
2. Copy `Assets/Scripts/*.cs` from this repository into your project's `Assets/Scripts` folder.
3. Create three simple prefabs using SpriteRenderer (square sprite is enough):
   - `FloorPrefab`
   - `ObstaclePrefab`
   - `UnitPrefab` (attach `GridUnit` component)
4. In your scene:
   - Create an empty GameObject `GridGameManager`, attach `GridGameManager.cs`.
   - Assign the three prefabs in inspector fields (`Floor Prefab`, `Obstacle Prefab`, `Unit Prefab`).
   - Optionally edit obstacle and unit spawn coordinates from inspector lists.
   - Create another empty GameObject `InputController`, attach `UnitInputController.cs`.
   - Drag the `GridGameManager` object into the `Game Manager` field on `UnitInputController`.
5. Press Play.

## Controls
- Move selected unit: `W`, `A`, `S`, `D` or arrow keys.
- Cycle selected unit: `Tab`.

## Scripts
- `Assets/Scripts/GridGameManager.cs`: builds the grid, places obstacles, spawns units, and validates movement.
- `Assets/Scripts/GridUnit.cs`: stores unit grid position and visual selected state.
- `Assets/Scripts/UnitInputController.cs`: handles keyboard input and selected-unit switching.
