using UnityEngine;
using System.Collections;
using System;

public class Sinus : MonoBehaviour {
	//unoptimized version
	private static System.Random RandomNumber = new System.Random();
	public double frequency;
	public double frequency2 = 220;
	public double gain = 0.02;
	public static int[] pitchClass = new int[] {0, 2, 4, 5, 7, 9, 11};

	public double skew;
	public double swellRate;
	private double increment;
	private double increment2;
	private double increment3;
	private double phase;
	private double phase2;
	private double phase3;
	private double sampling_frequency = 44100;

	void Awake() {
		frequency = Sinus.midiToFreq((UnityEngine.Random.Range(1, 3) * 12) + UnityEngine.Random.Range(0, pitchClass.Length - 1));
		skew = (float)Math.Sin((float)RandomNumber.NextDouble()*2.0f*Math.PI) * 2.0f;
		swellRate = (float)Mathf.Clamp((float)(RandomNumber.NextDouble()), 0.1f, 1.0f);
		//frequency2 = skew + frequency;
		phase = 0;
		phase2 = 0;
		phase3 = 0;

	}

	void OnAudioFilterRead(float[] data, int channels) {
		// update increment in case frequency has changed
		increment = (frequency + skew + (100.0 * Math.Sin(phase2))) * 2 * Math.PI / sampling_frequency;
		increment2 = frequency2 * 2 * Math.PI / sampling_frequency;
		increment3 = swellRate * 2 * Math.PI / sampling_frequency;
		for (var i = 0; i < data.Length; i = i + channels) {
			phase = phase + increment;
			phase2 = phase2 + increment2;
			phase3 = phase3 + increment3;
			// this is where we copy audio data to make them 
			// "available" to Unity
			data[i] = data[i] * (float)(gain * Math.Sin(phase));
			data[i] = data[i] * (float)(Math.Abs(Math.Sin(phase3)) * Math.Abs(Math.Sin(phase3)));
			// if we have stero, we copy the mono data to each channel
			if (channels == 2) data[i + 1] = data[i];
			if (phase > 2 * Math.PI) phase = 0;
			if (phase2 > 2 * Math.PI) phase2 = 0;
			if (phase3 > 2 * Math.PI) phase3 = 0;
		}
	}

	static float midiToFreq(int midi) {
		return Mathf.Pow(2.0f, (float)((midi * 1.0f) / 12.0f)) * 440.0f;
	}
}
