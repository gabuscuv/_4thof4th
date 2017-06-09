using Microsoft.Xna.Framework.Audio; // PCMengine
using System; // BeeperEngine
// Buscar libreria para hacer sonar el Midi Synth Integrado de Windows

using System.Threading;
namespace _4thof4th {
    class NeoRetroAudioEngine {
        AudioEngine audioEngine;
        SoundBank soundBank;
        WaveBank waveBank;
        static Thread beeper;
        public static void beeperengine(int[,] sheet) {
          beeper=  new Thread(new ThreadStart(new BeepThreading(sheet).start));
            beeper.Start();
        }
        private static void PCMengine() {

        }
        private static void SynthEngine() {

        }

    }
    internal class BeepThreading{
        int[,] sheet;
        internal BeepThreading(int[,] sheet) {
            this.sheet = sheet;
        }
        internal void start() {
            for(int i = 0; i < sheet.GetLength(0); i++){
                Console.Beep(sheet[i,0], sheet[i,1]);
            }
        }
    }
}