Add a click to continue in the dialogue menu.
Add space interaction button so player can easily advance convo.
Add a guard since it is a singleton nearby npc may get advance their index value by adding a isInRange bool.
Add a activeTalker in dialgoue manager so when click to continue is implemented it can know which npc's talk script to move.

Player bow shoot method dir can be changed after shooting which shouldn't happen.
When player shoot method called in y axis x = 0 or x ~ 1 to 2 the script get confused which animation to play.