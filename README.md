# UnityCheatConsole

This is a personal project that aims to have an easy way to input "cheats" into your unity game for easy test of different methods and components.

## How to install
Just download the unitypackage and open it from unity or double click to get Unity open it for you.

## How to use
First you need to add the prefab called: "CheatsConsole" to your scene.

Then assuming you have a script called "Character" for this example, in the OnEnable method you'll need to add:

```csharp
private void OnEnable()
{
   CheatController.AddCheatsFromBehaviour(this);
}
```

and for OnDisable:

```csharp
private void OnDisable()
{
  CheatController.RemoveCheatBehaviour(this);
}
```
**Then you need to declare which methods can be used as "cheat" codes, to do so you'll need to add an attribute of "CheatCode":**

```csharp
[CheatCode("add_exp", "add experience to character", "add_exp.{instance}#intAmount")]
public void GainExperience(int amount)
{
    currentExperience += amount;
}
```

this will make them show up in the Unity UI Cheat Console:

![image](https://user-images.githubusercontent.com/96312200/188337227-cb340c3b-7d67-4ce1-ab16-c19b41a3af20.png)

*By default the CheatsConsole prefab includes a script with the "help" cheat code, you can use it to get information on all the cheats that can be used as in the screenshot above.*

breaking down the meaning of the attribute:

1. name of the cheat code
2. breif description
3. format to call it

If you want to call the add_exp cheat code for your "Player" GameObject and you want to add 1000 exp, this is how you do it:

**add_exp.Player#1000**

if your "Character" script is **attached to multiple game objects**, for example a GameObject named Enemy and you run the above command, exp will be only granted to the "Player"
GameObject, however if you want to add experience to all the GameObjects with a "Character" script attached to them you can do it by running:

**add_exp#1000**

**Instance is optional and should match the name of a GameObject with the script that includes the Cheat attached to it.**

*Everything after the '#' symbol will be considered as parameters.*

### To toggle the console on/off 
you can use a 'back tick': "`" to Toggle on and off the console, this can be configured in the prefab:
![image](https://user-images.githubusercontent.com/96312200/188338682-03e80327-94ae-46d5-a88b-9bd36e52e63e.png)

Also you can modify the theme by creating new Scriptable Objects from the one included in the Themes folder:
![image](https://user-images.githubusercontent.com/96312200/188338718-a157f119-7252-4714-87b4-6d63fecb9f8c.png)

The theme will be passed on to the GameObjects with the following component attached to them: 
![image](https://user-images.githubusercontent.com/96312200/188338740-b33f05d6-4e3d-4c2e-adca-b267ef24ea1f.png)



