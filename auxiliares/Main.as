package  {
	import flash.media.Microphone;
	import flash.display.MovieClip;
	import flash.events.StatusEvent;
	import flash.events.ActivityEvent;
	import flash.events.SampleDataEvent;
	import flash.utils.ByteArray;
	import flash.events.Event;
	import flash.display.Graphics;
	import flash.media.SoundMixer;
	import flash.media.Sound;
	import flash.media.SoundChannel;
	import flashx.textLayout.formats.Float;
	import flash.net.Socket;
	import flash.utils.Endian;
	import flash.text.TextField;
	import flash.errors.IOError;
	import flash.events.IOErrorEvent;
	import flash.utils.Timer;
	import flash.events.TimerEvent;
	
	public class Main extends MovieClip {
		
		private var mic:Microphone;
		private var _micBytes:ByteArray;
		private var _socket:Socket;
		private var _encoder:WaveEncoder;
		private var _timer:Timer;
		
		public var txtConsola:TextField;
		public var txtLevel:TextField;		//	For input, for setSilenceLevel
		public var txtActivity:TextField;	//	For output, show current activity level
		
		public function Main() {
			// texto
			txtConsola.text = "inicializado\n";
			txtLevel.addEventListener(Event.CHANGE, onTxtLevelChange);
			this._micBytes = new ByteArray();
			var level:Number = int ( txtLevel.text );
			
			// microphone init
			mic = Microphone.getMicrophone();
			mic.gain = 60;//60;
			mic.rate = 44;//11;
			//mic.setUseEchoSuppression(true);
			mic.setLoopBack(false);
			mic.setSilenceLevel(5, 1000);
			mic.addEventListener(ActivityEvent.ACTIVITY, this.onMicActivity);
			mic.addEventListener(StatusEvent.STATUS, this.onMicStatus);
			mic.addEventListener(SampleDataEvent.SAMPLE_DATA, onMicSampleData);
			
			// socket
			this._socket = new Socket();
			this._socket.addEventListener(IOErrorEvent.IO_ERROR, this.onIOError);
			this._encoder = new WaveEncoder(1);
			
			// timer
			this._timer = new Timer(100, 0);
			this._timer.addEventListener(TimerEvent.TIMER, this.onTimer);
			this._timer.start();
		}
		
		function onMicSampleData(event:SampleDataEvent):void
		{
			this._micBytes.writeBytes(event.data);
		}
		
		function onMicActivity(event:ActivityEvent):void
		{
			var msg:String = "activating=" + event.activating + ", activityLevel=" + mic.activityLevel;
			trace(msg);
			txtConsola.appendText(msg + "\n");
			
			if ( !event.activating ) {
				try {
					this._socket.connect("localhost", 7070);
					this._micBytes.position = 0;
					
					msg = "writting " + this._micBytes.bytesAvailable + " bytes";
					trace(msg);
					txtConsola.appendText(msg + "\n");
					
					var datos:ByteArray = _encoder.encode(this._micBytes, 1, 16, 44100 );
					this._socket.writeBytes(datos);
					this._socket.flush();
					this._socket.close();
					
					this._micBytes.clear();
				} catch( error:Error ) {
					txtConsola.appendText("Error: " + error.message + "\n");
				}
			}
		}
		
		function onIOError(event:IOErrorEvent):void
		{
			txtConsola.appendText("Error al conectar " + event.text + "\n");
		}
		
		function onMicStatus(event:StatusEvent):void
		{
			if (event.code == "Microphone.Unmuted")
			{
				trace("Microphone access was allowed.");
			}
			else if (event.code == "Microphone.Muted")
			{
				trace("Microphone access was denied.");
			}
		}
		
		function onTxtLevelChange(evt:Event):void{
			this.mic.setSilenceLevel( int(txtLevel.text), 1000);
		}
		
		function onTimer(evt:TimerEvent):void {
			txtActivity.text = this.mic.activityLevel + "";
		}
	}
}
