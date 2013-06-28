%%
%%	General :: returns a vector with mean, variance, max and min =============
%%
function [ stats ] = getStatistics(vector)
	% extraer estadisticas
	media		= mean(vector);
	varianza	= std(vector);
	maximo		= max(vector);
	minimo		= min(vector);
	stats		= [media varianza maximo minimo];
end

%%
%%	Pitch :: Fill unvoiced frames with ceros =============
%%
function [ filledData ] = fillPitchData ( datosR )
	
	% fprintf('>> Extracting data from \"%s\" ...\n', fileIn);
	% datosR	= csvread(strcat(fileIn,".csv"));
	% datosR	= csvread(fileIn);
	
	sampleRate	= 44100;
	frameOfset	= 256;
	muestras	= size(datosR,1);
	frames		= zeros(muestras,1);

	for cont=1:muestras
		frames(cont,1)=((sampleRate*datosR(cont,1))/frameOfset);
	end
	ultimoFrame			= frames(muestras);
	datosR				= [datosR frames];
	resultados			= zeros(ultimoFrame,3);
	frameMatriz2		= 0;
	contadorMatriz2		= 1;
	for cont2=1:ultimoFrame
		frameMatriz2	= datosR(contadorMatriz2,3);
		if(cont2<frameMatriz2)
			resultados(cont2,1)	= ((frameOfset*cont2)/sampleRate);
			resultados(cont2,2)	= 0;
			resultados(cont2,3)	= cont2;
		else
			resultados(cont2,:)	= datosR(contadorMatriz2,:);
			contadorMatriz2		= contadorMatriz2+1;
		endif
	end
	
	filledData	= resultados(:,1:2);
	% csvwrite (strcat(fileIn,"_ceros.csv"), filledData);
	% csvwrite( fileIn, filledData );
end

%%
%%	Pitch :: Generate the features for pitch =============
%%
function [ features ] = pitchFeatures ( data, limit, bData, bDeriv, bStats )
	vecStat1 	= getStatistics(data(:,2) );		% data is [mean, std, max, min]
	%data		= fillPitchData(data);
	
	columna		= data(:,2);
	m			= size(columna,1);
	
	%derivadas
	vecDerivadas	= zeros(m,1);
	for cont=1:m
		if(cont < m)
			dato2					= columna(cont+1,1);
			dato1					= columna(cont  ,1);
			vecDerivadas(cont,1)	= dato2-dato1;
		endif
	end
	
	% filled data and derivative, truncate or padd to have a vector of length "limit"
	data = [data vecDerivadas];
	hardLimit 	= min( size(data,1), limit );
	data 		= data(1:hardLimit, :);
	
	paddingSize	= limit - size(data,1);
	data		= [ data ; zeros(paddingSize, size(data,2)) ];% data is [timestamp, hz, derivative]
	
	
	% statistics output
	vecStat2 	= getStatistics(vecDerivadas);	% data is [mean, std, max, min]
	stats		= [vecStat1 vecStat2];	
	
	features = [];
	if bData
		features = [features data(:,2)'];
	end
	if bDeriv
		features = [features data(:,3)'];
	end
	if bStats
		features = [features stats];
	end
end

%%
%%	MFCC:  return the mfcc data row processed
%%
function [ features ] = mfccFeatures(mfccData, limit, nCoeficients, bData, bStats)
	% limit to 50 rows, get nCoeficients
	data 		= mfccData;
	hardLimit 	= min( size(data,1), limit );
	data 		= data(1:hardLimit, :);
	
	paddingSize	= limit - size(data,1);
	data		= [ data ; zeros(paddingSize, size(data,2)) ]; % data is [ timestamp : mfcc1 : mfcc2 : mfcc3 .. ]
	
	
	features = [];
	
	if bStats
		stats = [ ];
		for t = [2:nCoeficients+1]
			stats = [ stats getStatistics( data(:,t) ) ];
		end
		features = stats;
	end
	
	if bData
		features = [ features data(:,2:1+nCoeficients)(:)' ];
	end
	
end

%%
%%	args:  
%%		outputFile 
%%		filePitch 
%%		fileMfcc 
%%		label
%%
if nargin < 4
	printf("Insificient arguments\n");
else
	arg_list 	= argv ();
	outputFile	= arg_list{1};
	filePitch	= arg_list{2};
	fileMfcc	= arg_list{3};
	label		= arg_list{4};
	label		= base2dec (label, 10)
	
	pitchData			= csvread(filePitch);
	pitchData		 	= pitchFeatures(pitchData, 150, 0, 0, 1);	% data, limit, bData, bDeriv, bStats
	
	mfccData			= csvread(fileMfcc);
	mfccData			= mfccFeatures(mfccData, 50, 20, 0, 1);		% data, limit, nCoeficients, bData, bStats
	
	%% append data
	fid = fopen(outputFile, "r");
	if fid ~= -1
		fclose(fid);
		features	= csvread(outputFile);
	else
		features	= [];
	end
	
	features	= [features; [label pitchData mfccData] ];
	csvwrite(outputFile, features);
end
