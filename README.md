# sakura-prj

一个简单的 Unity 2D 网格模拟原型，包含可移动单位与障碍物碰撞限制。

## 功能特性
- 基于网格的 2D 场景（可配置宽度和高度）。
- 在网格格子中生成多个单位。
- 使用 `WASD` 或方向键让当前选中单位每次移动一格。
- 使用 `Tab` 切换当前可控制单位。
- 基础碰撞检测：
  - 单位不能移动到网格外。
  - 单位不能移动到障碍物所在格子。
  - 单位不能穿过或重叠到其他单位。

## Unity 搭建步骤（使用内置 2D 工具）
1. 创建一个新的 **2D Core** Unity 项目。
2. 将本仓库中的 `Assets/Scripts/*.cs` 复制到你的项目 `Assets/Scripts` 目录。
3. 使用 SpriteRenderer 创建 3 个简单预制体（方块精灵即可）：
   - `FloorPrefab`
   - `ObstaclePrefab`
   - `UnitPrefab`（挂载 `GridUnit` 组件）
4. 在场景中：
   - 创建空物体 `GridGameManager`，挂载 `GridGameManager.cs`。
   - 在 Inspector 中赋值 3 个预制体字段（`Floor Prefab`、`Obstacle Prefab`、`Unit Prefab`）。
   - 可按需要在 Inspector 中修改障碍物与单位出生点坐标列表。
   - 再创建空物体 `InputController`，挂载 `UnitInputController.cs`。
   - 将 `GridGameManager` 物体拖到 `UnitInputController` 的 `Game Manager` 字段。
5. 点击 Play 运行。

## 操作方式
- 移动当前选中单位：`W`、`A`、`S`、`D` 或方向键。
- 切换选中单位：`Tab`。

## 脚本说明
- `Assets/Scripts/GridGameManager.cs`：构建网格、放置障碍、生成单位并校验移动合法性。
- `Assets/Scripts/GridUnit.cs`：保存单位网格坐标并控制选中状态显示。
- `Assets/Scripts/UnitInputController.cs`：处理键盘输入并切换当前控制单位。
