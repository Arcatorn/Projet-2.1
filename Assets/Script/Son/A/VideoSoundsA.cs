using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoSoundsA : MonoBehaviour 
{
	[FMODUnity.EventRef] public string Select_Sound;
	public FMOD.Studio.EventInstance SoundEvent;
	[FMODUnity.EventRef] public string Select_Sound2;
	public FMOD.Studio.EventInstance SoundEvent2;
	[FMODUnity.EventRef] public string Select_Sound3;
	public FMOD.Studio.EventInstance SoundEvent3;
	[FMODUnity.EventRef] public string Select_Sound4;
	public FMOD.Studio.EventInstance SoundEvent4;
	[FMODUnity.EventRef] public string Select_Sound5;
	public FMOD.Studio.EventInstance SoundEvent5;
	[FMODUnity.EventRef] public string Select_Sound6;
	public FMOD.Studio.EventInstance SoundEvent6;
	[FMODUnity.EventRef] public string Select_Sound7;
	public FMOD.Studio.EventInstance SoundEvent7;
	
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
		
		SoundEvent5 = FMODUnity.RuntimeManager.CreateInstance(Select_Sound5);
		SoundEvent5.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.transform));

		SoundEvent6 = FMODUnity.RuntimeManager.CreateInstance(Select_Sound6);
		SoundEvent6.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.transform));

		SoundEvent7 = FMODUnity.RuntimeManager.CreateInstance(Select_Sound6);
		SoundEvent7.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.transform));
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

	public void SituationCinq()
	{
		FMOD.Studio.PLAYBACK_STATE fmodPbState;
        SoundEvent5.getPlaybackState(out fmodPbState);
		SoundEvent5.start();
	}

	public void SituationSix()
	{
		FMOD.Studio.PLAYBACK_STATE fmodPbState;
        SoundEvent6.getPlaybackState(out fmodPbState);
		SoundEvent6.start();
	}

	public void SituationSept()
	{
		FMOD.Studio.PLAYBACK_STATE fmodPbState;
        SoundEvent7.getPlaybackState(out fmodPbState);
		SoundEvent7.start();
	}

	public void StopAll()
	{
		SoundEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        SoundEvent2.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		SoundEvent3.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        SoundEvent4.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		SoundEvent5.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        SoundEvent6.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		SoundEvent7.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
	}
}
