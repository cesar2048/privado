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
	
	public class Main extends MovieClip {
		
		private var mic:Microphone;
		private var _micBytes:ByteArray;
		private var _son:Sound;
		private var _sc:SoundChannel;
		private var _socket:Socket;
		private var _encoder:WaveEncoder;
		
		private const PLOT_HEIGHT:int = 200;
		private const CHANNEL_LENGTH:int = 256;

		public function Main() {
			this._micBytes = new ByteArray();
			
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
			
			// sound init
			this._son = new Sound();
			this._son.addEventListener(SampleDataEvent.SAMPLE_DATA, onSoundSampleData);
			this._sc = _son.play();
			
			this._encoder = new WaveEncoder(1);
			
			// display init
			this.addEventListener(Event.ENTER_FRAME, onEnterFrame);
		}
		
		function onMicSampleData(event:SampleDataEvent):void
		{
			this._micBytes.writeBytes(event.data);
		}
		
		function onSoundSampleData(event:SampleDataEvent):void
		{
			var available:Number = ((_micBytes != null ) ? _micBytes.bytesAvailable : 0), i:Number;
			for (i = 0; i < available / 4; i++) {
				var sample:Number=_micBytes.readFloat();
				event.data.writeFloat(sample);
				event.data.writeFloat(sample);
			}
			for (i = 0; i < 2048 - available/2; i++) {
				event.data.writeFloat(0);
				event.data.writeFloat(0);
			}
		}
		
		function onMicActivity(event:ActivityEvent):void
		{
			trace("activating=" + event.activating + ", activityLevel=" + mic.activityLevel);
			if ( !event.activating ) {
				// socket
				this._socket = new Socket("localhost", 7070);
				this._micBytes.position = 0;
				
				trace("writting " + this._micBytes.bytesAvailable + " bytes");
				
				var datos:ByteArray = _encoder.encode(this._micBytes, 1, 16, 44100);
				this._socket.writeBytes(datos);
				this._socket.flush();
				this._socket.close();
				
				this._micBytes.clear();
			}
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
		
		function onEnterFrame(event:Event):void
		{
			var soundBytes:ByteArray = new ByteArray();
			SoundMixer.computeSpectrum(soundBytes, false, 0);
			var g:Graphics = this.graphics;
			g.clear();
			g.lineStyle(0, 0x6600CC);
			g.beginFill(0x6600CC);
			g.moveTo(0, PLOT_HEIGHT);
			var n:Number = 0;
			var max:Number = 0;
			
			// left channel
			for (var i:int = 0; i < CHANNEL_LENGTH; i++)
			{
				n = (soundBytes.readFloat() * PLOT_HEIGHT);
				if ( Math.abs(n) > max ) max = n;
				g.lineTo(i * 2, PLOT_HEIGHT - n);
			}
			g.lineTo(CHANNEL_LENGTH * 2, PLOT_HEIGHT);
			g.endFill();
			
			// right channel
			g.lineStyle(0, 0xCC0066);
			g.beginFill(0xCC0066, 0.5);
			g.moveTo(CHANNEL_LENGTH * 2, PLOT_HEIGHT);
			for (i = CHANNEL_LENGTH; i > 0; i--)
			{
				n = (soundBytes.readFloat() * PLOT_HEIGHT);
				g.lineTo(i * 2, PLOT_HEIGHT - n);
			}
			
			g.lineTo(0, PLOT_HEIGHT);
			g.endFill();
			
			//trace(max);
		}

	}
	
}
