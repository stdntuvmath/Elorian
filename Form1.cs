using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Media;

namespace Elorian
{
    public partial class Form1 : Form
    {
        private SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        //private SpeechRecognizer synth = new SpeechRecognizer();
        private System.Speech.Synthesis.SpeechSynthesizer synth = new System.Speech.Synthesis.SpeechSynthesizer();
        private SoundPlayer player = new SoundPlayer(@"C:\Users\14025\source\repos\Voice Recognition Example\StarTrekSounds\computerBeep1.wav");



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //load dictionary


            
            Choices conversationInput = new Choices();

            conversationInput.Add(new string[] {"the","monkey","ran","down","street","a"});

            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.AppendDictation();


            System.Speech.Recognition.Grammar grammar = new System.Speech.Recognition.Grammar(@"C:\Users\"+System.Environment.UserName+@"\source\repos\Elorian\Elorian\GrammerLibrary.xml");




            //recognizer.PauseRecognizerOnRecognition = true;
            recEngine.LoadGrammarAsync(grammar);//not going to load on the main thread therefore it will keep running

            recEngine.SetInputToDefaultAudioDevice();//receive audio commands through laptop microphone

            recEngine.SpeechRecognized += RecEngine_SpeechRecognized; ;

            recEngine.RecognizeAsync(RecognizeMode.Multiple);//recognize multiple commands
        }

        private void RecEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            richTextBox1.AppendText(e.Result.Text + " ");
            //richTextBox1.AppendText("\n");
            synth.SpeakAsync(e.Result.Text);
        }
    }
}
