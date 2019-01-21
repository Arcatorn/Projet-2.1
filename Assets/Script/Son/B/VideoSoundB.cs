using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoSoundB : MonoBehaviour {

	[FMODUnity.EventRef] public string Select_Sound;
	public FMOD.Studio.EventInstance SoundEvent;
	[FMODUnity.EventRef] public string Select_Sound2;
	public FMOD.Studio.EventInstance SoundEvent2;
	[FMODUnity.EventRef] public string Select_Sound3;
	public FMOD.Studio.EventInstance SoundEvent3;
	[FMODUnity.EventRef] public string Select_Sound4;
	public FMOD.Studio.EventInstance SoundEvent4;

	void Start ()
	{
		SoundEvent = FMODUnity.RuntimeManager.CreateInstance(Select_Sound);
		SoundEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.transform));
		
		SoundEvent2 = FMODUnity.RuntimeManager.CreateInstance(Select_Sound2);
		SoundEvent2.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.transform));
		
		SoundEvent3 = FMODUnity.RuntimeManager.CreateInstance(Select_Sound3);
		SoundEvent3.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.transform));
		
		SoundEvent4 = FMODUnity.RuntimeManager.CreateInstance(Select_Sound4);
		SoundEvent4.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.transform));

	}

	public void SituationUne()
	{
		FMOD.Studio.PLAYBACK_STATE fmodPbState;
        SoundEvent.getPlaybackState(out fmodPbState);
		SoundEvent.start();		
	}

	public void SituationDeux()
	{
		FMOD.Studio.PLAYBACK_STATE fmodPbState;
        SoundEvent2.getPlaybackState(out fmodPbState);
		SoundEvent2.start();
	}

	public void SituationTrois()
	{
		FMOD.Studio.PLAYBACK_STATE fmodPbState;
        SoundEvent3.getPlaybackState(out fmodPbState);
		SoundEvent3.start();
	}

	public void SituationQuatre()
	{
		FMOD.Studio.PLAYBACK_STATE fmodPbState;
        SoundEvent4.getPlaybackState(out fmodPbState);
		SoundEvent4.start();
	}

	public void StopAll()
	{
		SoundEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        SoundEvent2.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		SoundEvent3.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        SoundEvent4.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
	}
}
