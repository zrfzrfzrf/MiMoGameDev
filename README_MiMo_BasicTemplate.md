# MiMo Basic Template Setup

## 1. 创建场景元素
1. 打开 `Assets/Scenes/MainScene.unity`（或新建一个场景）。
2. 在 Hierarchy 中创建两个空对象：`MiPlayer` 和 `MoPlayer`。
3. 给每个对象添加一个 `SpriteRenderer`，先用简单 `Square` 或 `Circle` 精灵代替火柴人。你也可以先用 `Gizmos` 或占位图。
4. 给两个对象添加 `Rigidbody2D` 组件，并将 `Body Type` 设为 `Dynamic`。
5. 给两个对象添加 `BoxCollider2D`（或 `CircleCollider2D`）。

## 2. 添加控制脚本
1. 将 `Assets/Scripts/PlayerMovement.cs` 添加到 `MiPlayer` 和 `MoPlayer`。
2. 在 `MiPlayer` 的 `PlayerMovement` 组件中，将 `Role` 设为 `Mi`。
3. 在 `MoPlayer` 的 `PlayerMovement` 组件中，将 `Role` 设为 `Mo`。
4. 调整 `Move Speed` 为 `4-6` 之间，先测试顺手。

## 3. 设置摄像机
1. 创建一个 `Main Camera`（如果场景没有）。
2. 给它添加 `CameraFollow` 脚本。
3. 将 `Target` 设为当前你想测试的玩家，例如 `MiPlayer`。
4. 以后可以复制摄像机做分屏，或把一个摄像机改为包含两人视野的镜头。

## 4. 配置输入轴（旧版 Input Manager）
打开 `Edit > Project Settings > Input Manager`，在 `Axes` 下增加以下 4 个轴：

- 名称：`MiHorizontal`
  - 负按钮：`a`
  - 正按钮：`d`
  - Alt 负按钮：`left`
  - Alt 正按钮：`right`
  - 类型：`Key or Mouse Button`

- 名称：`MiVertical`
  - 负按钮：`s`
  - 正按钮：`w`
  - Alt 负按钮： `down`
  - Alt 正按钮： `up`
  - 类型：`Key or Mouse Button`

- 名称：`MoHorizontal`
  - 负按钮：`j`
  - 正按钮：`l`
  - Alt 负按钮：`left`
  - Alt 正按钮：`right`
  - 类型：`Key or Mouse Button`

- 名称：`MoVertical`
  - 负按钮：`k`
  - 正按钮：`i`
  - Alt 负按钮： `down`
  - Alt 正按钮： `up`
  - 类型：`Key or Mouse Button`

## 5. 测试移动
1. 运行场景。
2. 使用 `WASD` 控制 `Mi`，`IJKL` 控制 `Mo`。
3. 如果人物移动不顺畅，可以把 `Rigidbody2D` 的 `Interpolation` 设为 `Interpolate`，`Collision Detection` 设为 `Continuous`。

## 6. 下一步建议
- 先做“空白背景 + 火柴人”场景。
- 再做“可推动/拉动的Twin Object”占位道具，后续可复用 `TwinObject` 逻辑。
- 然后加“光线激活”和“影子互动”的第二层玩法。
- 最后做“碎片触碰触发敌人”战斗逻辑。

---

## 代码说明
- `PlayerMovement.cs`：负责玩家输入和刚体移动。
- `CameraFollow.cs`：负责平滑跟随目标对象。

如果你想，我也可以继续加：
- 两个独立分屏相机模板
- `TwinObject` 联动方块脚本
- 基础战斗/碰撞触发脚本
