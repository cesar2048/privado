using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio.Wave;
using NAudio.Utils;

namespace DemoAudio
{
    class Audio
    {
        private WaveIn ondaEntrada;
        private WaveFileWriter ficheroOndaSalida;
        private double threshdB;	// threshold en dB
        private double thresh;		// threshold linear
        private int flagStart=0;
        private int numMuestras = 0;
        private int sampleRate=16000;
        private float tiempoGrabacion = 15;
        /// <summary>
        /// Constructor por defecto (frecuencua de muestreo: 44100 canales: 2)
        /// </summary>
        public Audio()
        {
            ondaEntrada = new WaveIn(WaveCallbackInfo.FunctionCallback());
            ondaEntrada.WaveFormat = new WaveFormat(sampleRate, 1);
           
        }
        public double Threshold
        {

            get 
            { 
                return threshdB;
                System.Diagnostics.Debug.WriteLine("threshDB" + threshdB);
            }
            set 
            { 
                threshdB = value;
                thresh = Decibels.DecibelsToLinear(value);
                System.Diagnostics.Debug.WriteLine("thresh"+thresh);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="frecuenciaMuestreo">Frecuencia de muestreo</param>
        /// <param name="canales">Canales</param>
        public Audio(int frecuenciaMuestreo, int canales)
        {
            ondaEntrada = new WaveIn(WaveCallbackInfo.FunctionCallback());
            ondaEntrada.WaveFormat = new WaveFormat(sampleRate, 1);
        }

        /// <summary>
        /// Empieza la grabación
        /// </summary>
        /// <param name="fichero">Path absoluto del fichero a guardar la grabación</param>
        public void Grabar(string fichero)
        {
            ficheroOndaSalida = new WaveFileWriter(fichero, ondaEntrada.WaveFormat);
            ondaEntrada.DataAvailable += new EventHandler<WaveInEventArgs>(ondaEntrada_DatosDisponibles);
            int a=ondaEntrada.BufferMilliseconds;
            ondaEntrada.StartRecording();
        }

        /// <summary>
        /// Para la grabación
        /// </summary>
        public void Parar()
        {
            ondaEntrada.StopRecording();
            ondaEntrada.Dispose();
            ondaEntrada = null;
            ficheroOndaSalida.Close();
            ficheroOndaSalida = null;
        }

        /// <summary>
        /// Guarda los datos que va recibiendo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ondaEntrada_DatosDisponibles(object sender, WaveInEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(thresh);
            ficheroOndaSalida.WriteData(e.Buffer, 0, e.BytesRecorded);
            /*for (int index = 0; index < e.BytesRecorded && flagStart==0; index += 2)
            {
                short sample = (short)((e.Buffer[index + 1] << 8) | e.Buffer[index + 0]);
                System.Diagnostics.Debug.WriteLine("volumen de sample:" + sample);
                float sample32 = sample / 32768f;
                System.Diagnostics.Debug.WriteLine("volumen de 0 a 1:"+sample32);
                if (sample32 > 0.001)
                {
                    flagStart = 1;
                }
            }
            if (flagStart == 1)
            {
                numMuestras += e.BytesRecorded/2;
                ficheroOndaSalida.WriteData(e.Buffer, 0, e.BytesRecorded);  
            }
            if (numMuestras >= (sampleRate*tiempoGrabacion))
            {
                flagStart = 0;
                numMuestras = 0;
                Parar();
            }
			 * */
          }

        /// <summary>
        /// Enumera todos los dispositivos (el dispositivo de grabación por defecto es el 0)
        /// </summary>
        /// <returns>Lista de todos los dispositivos</returns>
        public List<WaveInCapabilities> EnumerarDispositivos()
        {
            List<WaveInCapabilities> list = new List<WaveInCapabilities>();
            int dispositivosOndaEntrada = WaveIn.DeviceCount;
            for (int i = 0; i < dispositivosOndaEntrada; i++)
            {
                WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(i);
                list.Add(deviceInfo);
                System.Diagnostics.Debug.WriteLine("Dispositivo {0}: {1}, {2} canales", i, deviceInfo.ProductName, deviceInfo.Channels);
            }
            return list;
        }
    }
}
