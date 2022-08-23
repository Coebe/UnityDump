# Game series description

## Obstacle 躲避障碍物  

### Player Experience 玩家体验  

feel self Careful? Clever?  
冲刺和躲避  

### Core Mechanic 核心玩法机制  

Move & dodge obstacles 移动和躲避障碍物  

### Game Loop  

1. 游戏的博弈逻辑是什么，什么可以让游戏一遍又一遍地运行  
2. 整体的体验是什么

游戏的逻辑  
  从开始A走到终点B  

### Game Content

player name: flappy  

### References  

[Unity Docs: Cinemachine](https://docs.unity3d.com/Packages/com.unity.cinemachine@2.3/manual/index.html)

## Object Boost  

// Brain Idea: 设计一个火箭从地球飞上太空然后作为这个游戏的开始  
// Brain Idea: 整个世界是灰白的，需要拾取颜色来让世界恢复（或者拾取后自己定义每个东西的颜色）

### Player Experience

Precision, Skillful  

### Core Mechanic  

Skillful fly spaceship and avoid environmental hazards  

### Core game loop

Get from A to B to complete the level, then progress to the next level  

### Onion Design

![Onion Design](..\Scripts\OnionDesign.png)  

- The single most important feature.  
  让玩家可以动起来，然后从开始位置到达终点  
- The next most important.  
  1. 有碰撞  
  2. 有对玩家“灵活”表现的奖励道具  
    a. 表现在移动上：提高移动速度，或者以位移方式移动  
    b. 增加生命  
  3. 有惩罚道具  
    a. 降低移动速度  
    b. 减少生命  
- The next most important.  
  a. 有一些障碍物阻碍玩家到达终点  
  b. 到达终点后可以进入下一关卡  

- What Features should I include in my game?  
- Where should I start development?  
- What are my priorities?  
- What if I run out of time?  
- When should I stop?  

### Movement

[Input-UnityEngine API](https://docs.unity3d.com/2020.1/Documentation/ScriptReference/Input.html)  

`// TODO: Rocket 旋转很诡异需要改`  

- 改变引擎全局的重力方向来模拟场景中的风或者其他阻力现象  
