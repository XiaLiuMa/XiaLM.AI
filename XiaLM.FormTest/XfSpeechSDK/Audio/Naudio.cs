using NAudio.Wave;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace XiaLM.FormTest.XfSpeechSDK.Audio
{
    /// <summary>
    /// Naudio音频处理类
    /// </summary>
    public class Naudio
    {
        public WaveIn waveIn { get; set; }  //音频录入类
        public WaveOut waveOut { get; set; }    //音频输出类
        public WaveFileWriter wfw { get; set; }    //音频文件写入类
        public string fileName { get; set; }    //接收录音数据的临时保存文件
        public event Action PlayEnd;
        private static readonly object lockObj = new object();
        private static Naudio naudio;
        public static Naudio GetInstance()
        {
            if (naudio == null)
            {
                lock (lockObj)
                {
                    if (naudio == null)
                    {
                        naudio = new Naudio();
                    }
                }
            }
            return naudio;
        }
        public Naudio()
        {
            waveIn = new WaveIn();
            waveOut = new WaveOut();
            fileName = AppDomain.CurrentDomain.BaseDirectory + @"\Temp.wav";
        }

        /// <summary>
        /// 将文字通过语音播放出来
        /// </summary>
        /// <param name="txt"></param>
        public void PlayText(string txt)
        {
            //waveIn.StopRecording();
            //Task.Factory.StartNew(() =>
            //{
            //    if (!string.IsNullOrEmpty(txt))
            //    {
            //        var bytes = XFwebApi.GetInstance().XunFeiTTS(txt);
            //        PlayAsBytes(bytes);
            //        PlayEnd();
            //    }
            //});
        }

        /// <summary>
        /// 根据字节数组播放
        /// </summary>
        /// <param name="bytes">要播放的字节数组</param>
        public void PlayAsBytes(byte[] bytes)
        {
            try
            {
                //waveIn.StopRecording();
                using (var wfr = new WaveFileReader(new MemoryStream(bytes)))
                using (WaveStream wavStream = WaveFormatConversionStream.CreatePcmStream(wfr))
                using (var baStream = new BlockAlignReductionStream(wavStream))
                using (waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
                {
                    waveOut.Init(baStream);
                    waveOut.Play();
                    while (waveOut.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(100);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 根据文件播放
        /// </summary>
        /// <param name="fPath">要播放的文件路径</param>
        public void PlayAsPath(string fPath)
        {
            try
            {
                waveIn.StopRecording();
                using (var wfr = new WaveFileReader(fPath))
                using (WaveStream wavStream = WaveFormatConversionStream.CreatePcmStream(wfr))
                using (var baStream = new BlockAlignReductionStream(wavStream))
                using (waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
                {
                    waveOut.Init(baStream);
                    waveOut.Play();
                    while (waveOut.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(100);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 开始录音
        /// </summary>
        public void StartRecord()
        {
            Task.Factory.StartNew(() =>
            {
                waveIn.WaveFormat = new WaveFormat(16000, 16, 1);   //16KHz,16bit,Mono的录音格式
                waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(WaveIn_DataAvailable);
                wfw = new WaveFileWriter(fileName, waveIn.WaveFormat);
                waveIn.StartRecording();
                Thread.Sleep(8 * 1000);
                StopRecord();
            });
        }

        /// <summary>
        /// 停止录音
        /// </summary>
        public void StopRecord()
        {
            waveIn.StopRecording();
        }

        /// <summary>
        /// 开始录音回调函数
        /// 【验证数据可用后开始录音】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (wfw != null)
            {
                wfw.Write(e.Buffer, 0, e.BytesRecorded);
                wfw.Flush();
            }
        }


    }
}
