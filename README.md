Hi,

now I got my MilkVRLauncher running as simple as I can imagine. Simple to use for everybody. Stream you Movies directly to the giant curve screen im MilkVR. Host you 360° Content on your windows PC or NAS or RasberyPi or what ever ... 

The Key is emby.media as simple server solution. It comes with a own Android client. And this client is now accepting my MilkVRLauncher as external mediaplayer. This will bring all your content to your GearVR. You can build up your own archive in the download section of MilkVR without having a single video dumping your expensive sdCard memory ... 

Here is how you need to go:

1.) Download and install the MilkVRLauncher

2.) Download and install Emby on a Mashine of your choice. Download Emby Windows, Linux, NAS, Mac, FreeBSD is supported.

3.) Launch Emby on your Server and set it up.

3.1) In windows: Open a WebBrowser on open the link: http://localhost:8096/web/dashboard.html

3.2) go to Libary

3.3) Add a directory that contains your content.

4.) Download the official Emby Android App

4.1) Enable the external player in the Emby Android App. ("Settings" -> "Player Options / Playback Options" -> "Enable external Videoplayer" (at the bottom)

5.) Chose your Movie and select Play.

6.) The Emby app should now give you a chose of available player. Chose MilkVRLauncher.

7.) Enter a title for your Movie (by default it is "MilkVRLauncher") and chose the Format of the Video.

(this is the title the link will have in the download section in milkVR)

8.) Click "Create Link-File in MilkVR"

9.) HAVE FUN! :)

FAQ:

Emby app does not show MilkVRLauncher. Only VLC and MX-Player :(

-> The Video is not an .mp4. Pls. remux/convert to .mp4 -> Othere people also having this problem. Instead of the Emby app you can use anDLNA

The video plays, but there is no Sound :(

-> Android only supports MP3, AAC, WMA, WAV, AC3 Audio

The screen stays black :(

-> Is it realy a mp4 or did you only rename the video? Android should support MPEG4, H.264, VC-1, DivX/XviD, H.263, Sorenson H.263. I am not shure what MilkVR realy supports.

MilkVRLauncher crashes when I try to create a link

-> Is there a "milkVR" folder on the root of your internal storrage? No? Pls. create one. The app needs this folder to create the link-files :)

I have got a NAS and emby.media is not available. But my NAS has a DLNA Server, can I use that?

-> Some people did use bubbleupnp to browse through DLNA content. I can confim that anDLNA works too :)

I did try to open a video with ESFileExplorer from a network share. But the screen stays black.

-> Have a look into this thread: MilkVR streams from SMB (ES File Explorer)

I use a dlna client with emby or plex media server. The file plays only for a couple of seconds, and then milkVR keeps loading.

-> The video/audio codec is not compatible with android. The media server trys to mux, what milkVR dosent like. Check video codecs ...

-> Or the bitrate is to very high what triggers emby to transcode to lower bitrate: In Emby Server go to DLNA -> Profiles -> (Default or BubbleUpnP because thats the one im using) -> Playback -> Max streaming bitrate . Increase this value.

edit: 09.01.16 Did add link to current version of MilkVRLauncher and to anDLNA

edit: 16.01.16 Did recognize anDLNA as primary alternative client when emby client does not work.

edit: 18.01.16 Did add new solution to faq
