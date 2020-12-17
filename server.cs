registerOutputEvent(fxDTSBrick, "setMusicDirection", "datablock Music" TAB "string 64 100" TAB "string 64 100" TAB "string 64 100");
function fxDTSBrick::setMusicDirection(%obj, %musicDB, %coneVectorData, %coneSettings, %audioSettings, %client)
{
	if (isObject(%obj.AudioEmitter))
		%obj.AudioEmitter.delete();

	%obj.AudioEmitter = 0;
	if(%obj.getDataBlock().specialBrickType !$= "Sound" || !isObject(%musicDB))
		return;

	%desc = %musicDB.getDescription();
	if(%musicDB.getClassName() !$= "AudioProfile" || %musicDB.uiName $= "" || !%desc.isLooping || %musicDB.fileName $= "")
		return;

	if(isFunction(filterVariableString))
		%coneVectorData = filterVariableString(%coneVectorData, %obj, %client, %client.player);

	if(isFunction(filterVariableString))
		%coneSettings   = filterVariableString(%coneSettings,   %obj, %client, %client.player);

	if(isFunction(filterVariableString))
		%audioSettings  = filterVariableString(%audioSettings,  %obj, %client, %client.player);

	%coneVectorBrick = %obj.getGroup().NTObject_[%coneVectorData, 0];
	if(isObject(%coneVectorBrick))
		%coneVector = vectorNormalize(vectorSub(%coneVectorBrick.getPosition(), %obj.getPosition()));
	else
		%coneVector = vectorNormalize(%coneVectorData);

	//cone settings
	%CIA = getWord(%coneSettings, 0);
	if(%CIA $= "")
		%CIA = %desc.coneInsideAngle;

	%COA = getWord(%coneSettings, 1);
	if(%COA $= "")
		%COA = %desc.coneOutsideAngle;

	%COV = getWord(%coneSettings, 2);
	if(%COV $= "")
		%COV = %desc.coneOutsideVolume;

	//regular audio settings
	%refDist = getWord(%audioSettings, 0);
	if(%refDist $= "")
		%refDist = %desc.ReferenceDistance;

	%maxDist = getWord(%audioSettings, 1);
	if(%maxDist $= "")
		%maxDist = %desc.maxDistance;

	%volume = getWord(%audioSettings, 2);
	if(%volume $= "")
		%volume = %desc.volume;

	%audioEmitter = new AudioEmitter("")
	{
		profile = %musicDB;
		useProfileDescription = 0;

		coneVector 		   = %coneVector;
		coneInsideAngle    = mClamp(%CIA , 0  , 360);
		coneOutsideAngle   = mClamp(%COA , 0  , 360);
		coneOutsideVolume  = mClampF(%COV, 0.0, 3.0);

		ReferenceDistance  = mClampF(%refDist, 0.0, 128.0);
		maxDistance 	   = mClampF(%maxDist, 0.0, 128.0);
		volume 			   = mClampF(%volume , 0.0, 3.0  );


		isLooping = %desc.isLooping;
		is3D 	  = %desc.is3D;
		type 	  = %desc.type;
	};

	MissionCleanup.add(%audioEmitter);
	%obj.AudioEmitter = %audioEmitter;
	%audioEmitter.setTransform(%obj.getTransform());
	%audioEmitter.coneVector = %coneVector;
}

registerOutputEvent(fxDTSBrick, "playSoundDirection", "datablock Sound" TAB "string 64 100" TAB "string 64 100" TAB "string 64 100");
function fxDTSBrick::playSoundDirection(%obj, %soundDB, %coneVectorData, %coneSettings, %audioSettings, %client)
{
	if (isObject(%obj.SoundAudioEmitter)) //feels a little dumb to be doing it like this
		%obj.SoundAudioEmitter.delete();

	%obj.SoundAudioEmitter = 0;
	if(!isObject(%soundDB))
		return;

	%desc = %soundDB.getDescription();
	if(%soundDB.getClassName() !$= "AudioProfile" || %desc.isLooping || %soundDB.fileName $= "")
		return;

	if(isFunction(filterVariableString))
		%coneVectorData = filterVariableString(%coneVectorData, %obj, %client, %client.player);

	if(isFunction(filterVariableString))
		%coneSettings   = filterVariableString(%coneSettings,   %obj, %client, %client.player);

	if(isFunction(filterVariableString))
		%audioSettings  = filterVariableString(%audioSettings,  %obj, %client, %client.player);

	%coneVectorBrick = %obj.getGroup().NTObject_[%coneVectorData, 0];
	if(isObject(%coneVectorBrick))
		%coneVector = vectorNormalize(vectorSub(%coneVectorBrick.getPosition(), %obj.getPosition()));
	else
		%coneVector = vectorNormalize(%coneVectorData);

	//cone settings
	%CIA = getWord(%coneSettings, 0);
	if(%CIA $= "")
		%CIA = %desc.coneInsideAngle;

	%COA = getWord(%coneSettings, 1);
	if(%COA $= "")
		%COA = %desc.coneOutsideAngle;

	%COV = getWord(%coneSettings, 2);
	if(%COV $= "")
		%COV = %desc.coneOutsideVolume;

	//regular audio settings
	%refDist = getWord(%audioSettings, 0);
	if(%refDist $= "")
		%refDist = %desc.ReferenceDistance;

	%maxDist = getWord(%audioSettings, 1);
	if(%maxDist $= "")
		%maxDist = %desc.maxDistance;

	%volume = getWord(%audioSettings, 2);
	if(%volume $= "")
		%volume = %desc.volume;

	%audioEmitter = new AudioEmitter("")
	{
		profile = %soundDB;
		useProfileDescription = 0;

		coneVector 		   = %coneVector;
		coneInsideAngle    = mClamp(%CIA , 0  , 360);
		coneOutsideAngle   = mClamp(%COA , 0  , 360);
		coneOutsideVolume  = mClampF(%COV, 0.0, 3.0);

		ReferenceDistance  = mClampF(%refDist, 0.0, 128.0);
		maxDistance 	   = mClampF(%maxDist, 0.0, 128.0);
		volume 			   = mClampF(%volume , 0.0, 3.0  );


		isLooping = %desc.isLooping;
		is3D 	  = %desc.is3D;
		type 	  = %desc.type;
	};


	MissionCleanup.add(%audioEmitter);
	%obj.SoundAudioEmitter = %audioEmitter;
	%audioEmitter.setTransform(%obj.getTransform());
	%audioEmitter.coneVector = %coneVector;
}


package Event_playAudioDirectionm
{
	function fxDTSBrick::onDeath (%obj)
	{
		if(isObject(%obj.SoundAudioEmitter))
			%obj.SoundAudioEmitter.delete();

		return parent::onDeath (%obj);
	}
};
activatePackage(Event_playAudioDirection);
