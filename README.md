# Iso2God
Decompiled Iso2God for quality of life improvements

This mod adds:
- select or drag and drop multiple ISO files at once, all files are added automatically to the list, you can edit them after as needed
- choose FTP upload destination path
- delete the source ISO and / or the GOD package after conversion / FTP upload
- use correct game names from game list files
- output as ISO only when you want to rebuild an ISO with full padding removed
- resizable main window
- ui "optimization" stuff
- various fixes for xbox (og): redump files are now supported, thumbnail images should read correctly

This uses code or assets from:
 - https://github.com/Ernegien/UIXTool - code for XPR formats not supported originally
 - https://github.com/IronRingX/xbox360-gamelist - xbox 360 game list
 - https://www.mobcat.zip/XboxIDs/ - xbox game list

<p align="center"><img width="986" height="393" alt="image" src="https://github.com/user-attachments/assets/d942054e-24cf-47e6-b779-0d3af342170b" /></p>
<p align="center"><img width="496" height="474" alt="image" src="https://github.com/user-attachments/assets/40b1669f-7779-44e5-8945-d8edc5cc40fd" /></p>

# Compatibility / Installation Notes

| Game | Disk | Method | Instructions |
|------|------|------------|--------------|
| Alien Isolation | Disk 1 | No GOD | Extract ISO and copy "content\\0000000000000000\\5345085E\\00000002" directory to HDD |
| Alien Isolation | Disk 2 | GOD | - |
| Assassin's Creed IV - Black Flag | Disk 1 | GOD | - |
| Assassin's Creed IV - Black Flag | Disk 2 | No GOD | Multiplayer disk |
| Batman - Arkham City - Game of the Year Edition | Disk 1 | GOD | - |
| Batman - Arkham City - Game of the Year Edition | Disk 2 (DLC only) | No GOD | Extract ISO and copy "content\\0000000000000000\\57520802\\00000002" directory to HDD |
| Batman - Arkham Origins | Disk 1 | GOD | - |
| Batman - Arkham Origins | Disk 2 | No GOD | Extract ISO, rename "content\\0000000000000000\\57520828\\FFFFFFFF" directory to "Content\\0000000000000000\\57520828\\00000002" and copy to HDD |
| Battlefield 4 | Disk 1 | No GOD | Extract ISO and copy "content\\0000000000000000\\454109BA\\00000002" directory to HDD |
| Battlefield 4 | Disk 2 | GOD | - |
| Bioshock | Disk 1 | GOD | - |
| Bioshock | Disk 2 (Bonus content) | No GOD | Extract ISO, rename "content\\0000000000000000\\FFED2000\\FFFFFFFF" directory to "Content\\0000000000000000\\545407D8\\00000002" and copy to HDD |
| Bioshock 2 | Disk 1 | GOD | - |
| Bioshock 2 | Disk 2 (Bonus content) | No GOD | Extract ISO, rename "content\\0000000000000000\\FFED2000\\FFFFFFFF" directory to "Content\\0000000000000000\\54540861\\00000002" and copy to HDD |
| Bioshock Infinite | Disk 1 | GOD | - |
| Bioshock Infinite | Disk 2 (Bonus content) | No GOD | Extract ISO, rename "content\\0000000000000000\\FFED2000\\FFFFFFFF" directory to "Content\\0000000000000000\\5454085D\\00000002" and copy to HDD |
| Blue Dragon | Disk 1 | GOD | - |
| Blue Dragon | Disk 2 | GOD | - |
| Blue Dragon | Disk 3 | GOD | - |
| Call of Duty - Advanced Warfare | Disk 1 | GOD | - |
| Call of Duty - Advanced Warfare | Disk 2 | No GOD | Extract ISO and copy "content\\0000000000000000\\41560914\\00000002" directory to HDD |
| Call of Duty - Black Ops III | - | - | Copy A92CACF8DF6D9DB1C32970035A6B8B63B09C2F1641 and 4FD64D1A99FF002F54CEE0FE922808E1D2D21A5341 to "Content/0000000000000000/4156091D/00000002" |
| Call of Duty - Ghosts | Disk 1 | GOD | - |
| Call of Duty - Ghosts | Disk 2 | No GOD | Extract ISO and copy "content\\0000000000000000\\415608FC\\00000002" directory to HDD |
| Call of Duty - World at War | - | GOD | Disable fakelive |
| Dark Souls II - Scholar of the First Sin | Disk 1 | GOD | - |
| Dark Souls II - Scholar of the First Sin | Disk 2 | No GOD | Extract ISO, rename "content\\0000000000000000\\FFED2000\\FFFFFFFF" directory to "Content\\0000000000000000\\465307E4\\00000002" and copy to HDD |
| Dead Space 2 | Disk 1 | GOD | - |
| Dead Space 2 | Disk 2 | GOD | - |
| Dead Space 3 | Disk 1 | GOD | - |
| Dead Space 3 | Disk 2 | GOD | - |
| Dishonored - Game of the Year Edition | Disk 1 | GOD | - |
| Dishonored - Game of the Year Edition | Disk 2 | No GOD | Extract ISO, rename "content\\0000000000000000\\FFED2000\\FFFFFFFF" directory to "Content\\0000000000000000\\425307E3\\00000002" and copy to HDD |
| Dragon's Dogma - Dark Arisen | Disk 1 | GOD | - |
| Dragon's Dogma - Dark Arisen | Disk 2 | No GOD | Extract ISO, rename "content\\0000000000000000\\FFED2000\\FFFFFFFF" directory to "Content\\0000000000000000\\43430814\\00000002" and copy to HDD |
| Elder Scrolls IV, The - Oblivion - Game of the Year Edition | Disk 1 | GOD | - |
| Elder Scrolls IV, The - Oblivion - Game of the Year Edition | Disk 2 | No GOD | Extract ISO, rename "content\\0000000000000000\\FFED2000\\FFFFFFFF" directory to "Content\\0000000000000000\\425307D1\\00000002" and copy to HDD |
| Elder Scrolls V, The - Skyrim - Legendary Edition | Disk 1 | GOD | - ||
| Elder Scrolls V, The - Skyrim - Legendary Edition | Disk 2 | No GOD | Extract ISO, rename "content\\0000000000000000\\FFED2000\\FFFFFFFF" directory to "Content\\0000000000000000\\425307E6\\00000002" and copy to HDD |
| Fallout 3 - Game of the Year Edition | Disk 1 | GOD | - |
| Fallout 3 - Game of the Year Edition | Disk 2 | No GOD | Extract ISO, rename "content\\0000000000000000\\FFED2000\\FFFFFFFF" directory to "Content\\0000000000000000\\425307D5\\00000002" and copy to HDD |
| Fallout - New Vegas - Ultimate Edition | Disk 1 | GOD | - |
| Fallout - New Vegas - Ultimate Edition | Disk 2 | No GOD | Extract ISO, rename "content\\0000000000000000\\FFED2000\\FFFFFFFF" directory to "Content\\0000000000000000\\425307E0\\00000002" and copy to HDD |
| Forza Motorsport 2 | Disk 1 | GOD | - |
| Forza Motorsport 2 | Disk 2 (Bonus content) | No GOD | Extract ISO, rename "content\\0000000000000000\\FFED2000\\FFFFFFFF" directory to "Content\\0000000000000000\\4D5307EA\\00000002" and copy to HDD |
| Forza Motorsport 3 | Disk 1 | GOD | - |
| Forza Motorsport 3 | Disk 2 (Bonus content) | No GOD | Extract ISO and copy "content\\0000000000000000\\4D53084D\\00000002" directory to HDD |
| Grand Theft Auto V | Disk 1 | No GOD | Extract ISO and copy "content\\0000000000000000\\545408A7\\00000002" directory to HDD |
| Grand Theft Auto V | Disk 2 | GOD | - |
| Mafia 2 | Disk 1 | GOD | - |
| Mafia 2 | Disk 2 | No GOD | Extract ISO, rename "content\\0000000000000000\\FFED2000\\FFFFFFFF" directory to "Content\\0000000000000000\\545407E6\\00000002" and copy to HDD |
| Mass Effect | Disk 1 | GOD | - |
| Mass Effect | Disk 2 | No GOD | Extract ISO, rename "content\\0000000000000000\\FFED2000\\FFFFFFFF" directory to "Content\\0000000000000000\\4D5307E8\\00000002" and copy to HDD |
| Metal Gear Solid V: The Phantom Pain | Disk 1 | No GOD | Extract ISO and copy "content\\0000000000000000\\4B4E085E\\00000002" directory to HDD |
| Metal Gear Solid V: The Phantom Pain | Disk 2 | GOD | - |
| Saints Row - The Third - The Full Package | Disk 1 | No GOD | Extract ISO, rename "content\\0000000000000000\\FFED2000\\FFFFFFFF" directory to "Content\\0000000000000000\\5451086D\\00000002" and copy to HDD |
| Saints Row - The Third - The Full Package | Disk 2 | GOD | - |
| Saints Row IV - National Treasure Edition | Disk 1 | No GOD | Extract ISO, rename "content\\0000000000000000\\FFED2000\\FFFFFFFF" directory to "Content\\0000000000000000\\4B4D07F6\\00000002" and copy to HDD |
| Saints Row IV - National Treasure Edition | Disk 2 | GOD | - |
| Tetris - The Grand Master Ace | - | GOD | ⚠️ Will not work with Padding "Remove all", use "Partial" |
| Tom Clancy's Splinter Cell - Blacklist | Disk 1 | GOD | - |
| Tom Clancy's Splinter Cell - Blacklist | Disk 2 | Mix | Install as GOD plus extract ISO and copy "Content\\0000000000000000\\555308B6\\00000002" directory to HDD |
| Ultra Street Fighter IV | - | GOD | Disable fakelive |
| Watch_Dogs | Disk 2     | No GOD     | Extract all Disk 2 contents |
| Watch_Dogs | Disk 1     | No GOD     | Copy contents of "installation1" and "installation2" from Disk 1 into the extracted Disk 2 |
| Watch_Dogs | Disk 2 bis | Custom GOD  | Build a new ISO and GOD from the combined Disk 2 folder (~10GB) |
| Wolfenstein - The New Order | Disk 1 | GOD | Install as GOD, and run to install, can be deleted afterwards |
| Wolfenstein - The New Order | Disk 2 | GOD | - |
| Wolfenstein - The New Order | Disk 3 | GOD | - |
| Wolfenstein - The New Order | Disk 4 | GOD | - |
