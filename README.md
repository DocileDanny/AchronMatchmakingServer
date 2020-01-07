This project is an Achron Matchmaker Server emulator.

A while ago, the site achrongame.com went offline.
That was a very sad thing, and left achron the time-traveling-rts entirely unable to be played!

This is a server emulator that allows games to be created.

Current Status:
The server emulator works - you can create and join games.

However, games never expire; and remain listed forever.
Additionally games never display the correct number of players in the games.

Also, as far as we are aware UPnP does not work; and so all users should forward ports 7014, 7013, and 7614.

How to use the server:
1) Download the source code, and build it using Visual Studio.
2) Alter the included file named hosts and replace 1.1.1.1 with the ip address of the server.
3) each player will need to replace their hosts file (C:\Windows\System32\drivers\etc\hosts) with this new altered host file.
4) the player hosting the server should ensure port 80 is forwarded, in addition to the other ports mentioned before.
5) run the server.
6) players may now run achron, and create a game, and join that game.
7) once the game has been finished, ideally the server should be restarted if more games are required.
