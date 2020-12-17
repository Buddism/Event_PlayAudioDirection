# Event_PlayAudioDirection
This mod only works with newer OpenAL driver versions
# How to use it
there are two events `setMusicDirection` `playSoundDirection`, they both operate nearly identical

Output Parameter # | Description | Usage
------------ | ------------- | -------------
1 | Sound DB | `[DB list]`<br>
2 | coneDirection | `brickName to face to OR a (3d vector)`<br>
3 | coneSettings in Degrees | `coneInsideAngle coneOutsideAngle coneVolume0-1`<br>
4 | audioSettings | `referenceDistance maxDistance volume`

reference distance is the distance where the audio is played at 100% volume<br>
maxDistance is how far you will hear the audio played<br>
<br>
example:
`setMusicDirection` -> `After School Special` -> `musicDirectionBrick` -> `45 120 0` -> `10 32 1`
