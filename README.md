# se-component-daemon
A Space Engineers Ingame Script to help keep a minimum of components at all times.

# Usage
Install the minified script into a programmable block on the same grid as an assembler and some form of cargo.  
Alter the list of component minimums to your liking, then enter the name of your assembler as an argument and hit run.

Every 10 seconds, the script will automatically pull the inventories of all blocks from the grid, including assembler queues.  
It will then check these numbers against the minimum amount of components needed, and if a deficit is found, queue the remaining needed components in the assembler specified.  

# Dev Notes
This was my first venture into the world of Space Engineers Ingame Scripts, and I needed a lot of help with a lot of this. Luckily, the creator of the SDK for Visual Studio, Malware-Dev on Github, along with multiple others from the official Space Engineers Discord server, gave me a lot of guidance on my journey. I am in no way proficient in C#, let alone in using it with Space Engineers, but I feel as if I've gained a good understanding over what to do, and more importantly, what not to do. This project has been a lot of fun to develop, and I can't wait to see what more I can accomplish with Ingame Scripts.
