using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MathNet.Numerics.LinearAlgebra.Double;

namespace SpeechAnalyzer.Model
{
	class AudioFileFeatures
	{
		public DenseMatrix mfcc { get; set; }
		public DenseMatrix pitch { get; set; }
		public DenseMatrix featureVector { get; set; }
		public Int32 label { get; set; }
		public FileInfo fileInfo { get; set; }

		public AudioFileFeatures(FileInfo audioFile, Int32 label)
		{
			this.fileInfo = audioFile;
			this.label = label;
		}

		public AudioFileFeatures()
		{
		}
	}
}
