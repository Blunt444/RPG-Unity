## Dual Stance
Dual Stance is made by the help of Unity Engine. It is a 2D game.

# Story
The game starts off with a letter from the crown saying the 3 mines which supports the economy of the kingdom has been captured.
As a player you are given the task of reclaming the 3 mines in order to save the crippling economy of the kingdom. The path to the mines are occupied by the enemies and friends. Inorder to reach the mines you need to slay down the enemies.

# Game Mechanism
The game offers you these :
- As suggested by the name two combat modes - **Archery** and **Sword**.
- **Skill Tree** - which lets you upgrade the respective mode of combat to overwhelm the enemies.
- **NPC Convo** - You can interact with the NPCs scattered around the map to trigger the convos.
- **Quest** - NPCs offers Quests to advance the story or for help.
- **Inventory** - Player can hold three different items or same items over different slots(only when stack size is reached). Gold is a separate slot which only holds gold.
- **Loot** - When a enemy is slawn it drops loot which can be pickedup automatically when the player is within reach.
- **Respawn Point** - When a player dies, the player respawns from the last activated respawn point.
- **Level System** - Slaying a enemy rewards you exp to the mode that is currently active meaning you can kill a enemy using your sword and instanlty switch the stance and gain the exp to the switched stance but you can't do it fast enough.
- **Save/Load** - Player can click the save button and save the important aspects of the game which then can be loaded back from the main menu.
- **Health** - Player has health so when taking damage the player loses his total health and eventually die if the bar goes 0 or negative.

## In Detail Explanations of the Combat Mechanism

**Combat :**<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The features two combat modes. The first one is **Sword** which is the default one, it allows the player to swing the sword when the **Left Mouse** button is clicked. It deals damage to enemies who are within the range of the sword. *Note* The swinging collision box is not a sphere it is a Box shaped so it allows the player to deal damage only to the enemies who are in the sword range and infront of him, so he never hits enemy behind his back.The second one is **Archery** which can only be obtained when the player completes a certain quest from the npc. This stance have a arrow holder which holds the quantity of the arrows which can be fired after firing all of them you can't no more shoot. Either kill more diffilcult enemies which will drop arrows as loot or buy from the shop npc. Once in the archery mode the **direction of the arrow** is decided by the **mouse**, so move your mouse accordingly. Now to the **drawback** of this mode when aiming the bow the player moves slowly if he tries to go to the opposite direction of the aim. So always have your aim where you are heading. *Note* the arrow kinda travels in a downward parabola so aim your precisely.

**Guard/Shield :**<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; This is a default action only can be triggered in **Sword** mode. When pressing the **Left Shift** button. While in this mode the player can't move but any slash damage is effectlively neglected.
*Note* to be in shield/guard mode the player must keep holding the left shift button for however long he wishes. If the player let it go before the slash damage is negleted the player gets **damaged**. You can only withstand 3 hits normally after getting hit for 3 times the guard mode gets locked temporarily until the cooldown is finished for it. You can upgrade the number of hits the shield can withstand in the skill tree(if i add one in the skill tree).

# Enemies

## Enemy Type - I(Torch)

## Enemy Type - II(TNT)

# Destroyables

## Tree

## TNT Barrels

# Effects when an Item is used

# How to run locally

# Bug or Improvements

# Credits to the art person
