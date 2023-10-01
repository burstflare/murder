# IMurderGame

**Namespace:** Murder \
**Assembly:** Murder.dll

```csharp
public abstract IMurderGame
```

This is the main loop of a murder game. This has the callbacks to relevant events in the game.

### ⭐ Properties
#### HasCursor
```csharp
public virtual bool HasCursor { get; }
```

**Returns** \
[bool](https://learn.microsoft.com/en-us/dotnet/api/System.Boolean?view=net-7.0) \
#### Name
```csharp
public abstract virtual string Name { get; }
```

This is the name of the game, used when creating assets and loading save data.

**Returns** \
[string](https://learn.microsoft.com/en-us/dotnet/api/System.String?view=net-7.0) \
### ⭐ Methods
#### CreateGamePreferences()
```csharp
public virtual GamePreferences CreateGamePreferences()
```

Creates a custom game preferences for the game.

**Returns** \
[GamePreferences](../Murder/Save/GamePreferences.html) \

#### CreateGameProfile()
```csharp
public virtual GameProfile CreateGameProfile()
```

Creates a custom game profile for the game.

**Returns** \
[GameProfile](../Murder/Assets/GameProfile.html) \

#### CreateSoundPlayer()
```csharp
public virtual ISoundPlayer CreateSoundPlayer()
```

Creates the client custom sound player.

**Returns** \
[ISoundPlayer](../Murder/Core/Sounds/ISoundPlayer.html) \

#### CreateRenderContext(GraphicsDevice, Camera2D, bool)
```csharp
public virtual RenderContext CreateRenderContext(GraphicsDevice graphicsDevice, Camera2D camera, bool useCustomShader)
```

Creates a custom render context for the game.

**Parameters** \
`graphicsDevice` [GraphicsDevice](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.GraphicsDevice.html) \
`camera` [Camera2D](../Murder/Core/Graphics/Camera2D.html) \
`useCustomShader` [bool](https://learn.microsoft.com/en-us/dotnet/api/System.Boolean?view=net-7.0) \

**Returns** \
[RenderContext](../Murder/Core/Graphics/RenderContext.html) \

#### CreateSaveData(string)
```csharp
public virtual SaveData CreateSaveData(string name)
```

Creates save data for the game.

**Parameters** \
`name` [string](https://learn.microsoft.com/en-us/dotnet/api/System.String?view=net-7.0) \

**Returns** \
[SaveData](../Murder/Assets/SaveData.html) \

#### LoadContentAsync()
```csharp
public virtual Task LoadContentAsync()
```

This loads all the content within the game. Called after [IMurderGame.Initialize](../Murder/IMurderGame.html#initialize).

**Returns** \
[Task](https://learn.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task?view=net-7.0) \

#### Initialize()
```csharp
public virtual void Initialize()
```

Called once, when the executable for the game starts and initializes.

#### OnDraw()
```csharp
public virtual void OnDraw()
```

Called after each draw.

#### OnExit()
```csharp
public virtual void OnExit()
```

Called once the game exits.

#### OnSceneTransition()
```csharp
public virtual void OnSceneTransition()
```

Called before a scene transition.

#### OnUpdate()
```csharp
public virtual void OnUpdate()
```

Called after each update.



⚡