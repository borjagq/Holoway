MANDATORILY FOLLOW THIS FOLDER STRUCTURE.

```
Root Directory
	|- Art
		|- Animations	(Everything related to animations go in this folder)
		|- Materials	(Material related file)
		|- Models		(.fbx, .obj, .glb, etc... 3D Models)
		|- Textures
	|- Audio
		|- Music
		|- Sound
	|- Code
		|- Scripts
		|- Shaders
	|- Docs
	|- Level
		|- Prefabs		(All the objects created later on, for reuse)
		|- Scenes		(All the scene related material will go here)
			|- Tests
		|-
	|- Tests
		|- PlayMode
		|- EditMode
	|- ThirdParty
```

Scene nomenclature:

Rules:
1. Follow CamelCase naming convention.
2. Use underscore sparingly.
3. Name the scenes with this kind of convention:
```
Purpose + Group
```
Example:
```
Login + Menu

Login 	-> 	Purpose
Menu 	-> 	Group
```

# Scenes
1. LoginMenu
2. MainMenu
3. SettingsMenu
4. AvatarModification
5. RoomCreationMenu
6. RoomSelectionMenu
7. SmallRoom
8. MediumRoom
9. LargeRoom


Music/Audio File Nomenclature:

Rules:

```
	Agent + Trigger
```

Example
```
	Button + Click => ButtonClick
	Button + Hover => ButtonHover
```

# ThirdParty Libraries
1. UMA (Very large library)
2. Oculus (Very large library)
3. 