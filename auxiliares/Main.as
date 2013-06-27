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
	import flash.events.SecurityErrorEvent;
	import flash.system.Security;
	
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
			dbg("inicializando");
			txtLevel.addEventListener(Event.CHANGE, onTxtLevelChange);
			this._micBytes = new ByteArray();
			Security.allowDomain("*"); 
			
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
			this._encoder = new WaveEncoder(1);
			
			// timer
			this._timer = new Timer(100, 0);
			this._timer.addEventListener(TimerEvent.TIMER, this.onTimer);
			this._timer.start();
		}
		
		
		//
		// --- microphone callbacks ---
		//
		function onMicSampleData(event:SampleDataEvent):void
		{
			this._micBytes.writeBytes(event.data);
		}
		
		function onMicStatus(event:StatusEvent):void
		{
			if (event.code == "Microphone.Unmuted") {
				dbg("Microphone access was allowed.");
			}
			else if (event.code == "Microphone.Muted") {
				dbg("Microphone access was denied.");
			}
		}
		
		function onMicActivity(event:ActivityEvent):void
		{
			dbg("activating=" + event.activating + ", activityLevel=" + mic.activityLevel);
			
			if ( !event.activating ) {
				this._socket = new Socket();
				this._socket.addEventListener(IOErrorEvent.IO_ERROR, this.onSocketIOError);
				this._socket.addEventListener(SecurityErrorEvent.SECURITY_ERROR, this.onSocketSecurityError);
				this._socket.addEventListener(Event.CONNECT, this.onSocketConnect);
				this._socket.connect("localhost", 7070);
			}
		}
		
		//
		// --- Socket callbacks ---
		//
		
		function onSocketIOError(event:IOErrorEvent):void
		{
			dbg("Error al conectar " + event.text);
			dbg("\n");
		}
		
		function onSocketSecurityError(evt:SecurityErrorEvent):void {
			dbg("OnSecError: " + evt.text);
		}
		
		function onSocketConnect(evt:Event):void {
			this._micBytes.position = 0;
			var datos:ByteArray = _encoder.encode(this._micBytes, 1, 16, 44100 );
			
			dbg("writting " + datos.bytesAvailable + " bytes");
			dbg("\n");
			
			this._socket.writeBytes(datos);
			this._socket.flush();
			
			this._socket.close();
			this._micBytes.clear()
		}
		
		//
		// other callbacks
		//
		
		function onTxtLevelChange(evt:Event):void{
			this.mic.setSilenceLevel( int(txtLevel.text), 1000);
		}
		
		function onTimer(evt:TimerEvent):void {
			txtActivity.text = this.mic.activityLevel + "";
		}
		
		
		//
		// --- auxiliary functions
		//
		function dbg(msg:String):void{
			trace(msg);
			txtConsola.appendText(msg + "\n");
		}
	}
}
