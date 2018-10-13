using NAudio.Wave;

namespace XiaLM.FormTest.XfSpeechSDK.Audio
{
    public class NaudioRealize
    {
        private WaveCallbackInfo wavInCallBack;

        public void Test()
        {
            WaveIn waveIn = new WaveIn(WaveCallbackInfo.FunctionCallback());
            IWaveIn waveIn1 = new WaveInEvent();
        }
    }
}
